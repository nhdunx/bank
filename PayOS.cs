using KLFixLag;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KLFixLag
{
    ///  Zermango  https://www.facebook.com/khangpcnopro

    public record WebhookType(string code, string desc, WebhookData data, string signature);
    public record WebhookData(long orderCode, int amount, string description, string accountNumber, string reference, string transactionDateTime, string currency, string paymentLinkId, string code, string desc, string? counterAccountBankId, string? counterAccountBankName, string? counterAccountName, string? counterAccountNumber, string? virtualAccountName, string virtualAccountNumber);
    public record Transaction(string reference, int amount, string accountNumber, string description, string transactionDateTime, string? virtualAccountName, string? virtualAccountNumber, string? counterAccountBankId, string? counterAccountBankName, string? counterAccountName, string? counterAccountNumber);
    public record PaymentData(long orderCode, int amount, string description, ItemData[] items, string cancelUrl, string returnUrl, string? signature = null, string? buyerName = null, string? buyerEmail = null, string? buyerPhone = null, string? buyerAddress = null, int? expiredAt = null);
    public record PaymentLinkInformation(string id, long orderCode, int amount, int amountPaid, int amountRemaining, string status, string createdAt, Transaction[] transactions, string? canceledAt, string? cancellationReason);
    public record ItemData(string name, int quantity, int price);
    public record CreatePaymentResult(string bin, string accountNumber, int amount, string description, long orderCode, string currency, string paymentLinkId, string status, string checkoutUrl, string qrCode);

    public class PayOSError : Exception
    {
        public string Code { get; }
        public PayOSError(string code, string? message) : base(message)
        {
            Code = code;
        }
    }

    public class SignatureControl
    {
        private static string ConvertObjToQueryStr(JObject obj)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (JProperty item in obj.Properties())
            {
                string name = item.Name;
                JToken value = item.Value;
                string value2 = "";
                if (value.Type == JTokenType.Date)
                {
                    value2 = ((DateTime)value).ToString("yyyy-MM-ddTHH:mm:sszzz");
                }
                else if (value.Type == JTokenType.String)
                {
                    value2 = value.Value<string>();
                }
                else if (value.Type == JTokenType.Array)
                {
                    value2 = "[";
                    bool flag = false;
                    foreach (JObject item2 in (IEnumerable<JToken>)value)
                    {
                        if (flag)
                        {
                            value2 += ",";
                        }
                        value2 += SortObjDataByKey(item2).ToString(Formatting.None);
                        flag = true;
                    }
                    value2 += "]";
                }
                else if (value.Type != JTokenType.Null)
                {
                    value2 = value.ToString();
                }
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append('&');
                }
                stringBuilder.Append(name).Append('=').Append(value2);
            }
            return stringBuilder.ToString();
        }

        private static JObject SortObjDataByKey(JObject obj)
        {
            if (obj.Type != JTokenType.Object)
            {
                return obj;
            }
            return new JObject(from x in obj.Properties()
                               orderby x.Name
                               select x);
        }

        private static string GenerateHmacSHA256(string dataStr, string key)
        {
            using HMACSHA256 hMACSHA = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            byte[] array = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(dataStr));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in array)
            {
                stringBuilder.Append(b.ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static string CreateSignatureFromObj(JObject data, string key)
        {
            JObject obj = SortObjDataByKey(data);
            string dataStr = ConvertObjToQueryStr(obj);
            return GenerateHmacSHA256(dataStr, key);
        }

        public static string CreateSignatureOfPaymentRequest(PaymentData data, string key)
        {
            int amount = data.amount;
            string cancelUrl = data.cancelUrl;
            string description = data.description;
            string value = data.orderCode.ToString();
            string returnUrl = data.returnUrl;
            string dataStr = $"amount={amount}&cancelUrl={cancelUrl}&description={description}&orderCode={value}&returnUrl={returnUrl}";
            return GenerateHmacSHA256(dataStr, key);
        }
    }

    public class PayOS
    {
        private readonly string _clientId;
        private readonly string _apiKey;
        private readonly string _checksumKey;
        private readonly string? _partnerCode;

        private static readonly HttpClient _httpClient = new HttpClient(); // Reusing HttpClient for performance

        public PayOS(string? partnerCode = null)
        {
            _clientId = Login._clientId;
            _apiKey = Login._apiKey_;
            _checksumKey = Login._checksum;
            _partnerCode = partnerCode;
        }

        private async Task<JObject> SendRequestAsync(string url, HttpMethod method, string? body = null, Dictionary<string, string>? headers = null)
        {
            var requestMessage = new HttpRequestMessage(method, url);

            if (body != null)
            {
                requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }

            requestMessage.Headers.Add("x-client-id", _clientId);
            requestMessage.Headers.Add("x-api-key", _apiKey);

            if (_partnerCode != null)
            {
                requestMessage.Headers.Add("x-partner-code", _partnerCode);
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode(); // Throws if the HTTP request failed

            string responseContent = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseContent);
        }

        public async Task<CreatePaymentResult> createPaymentLink(PaymentData paymentData)
        {
            if ((double)paymentData.orderCode < 0.0 - (Math.Pow(2.0, 53.0) - 1.0) || (double)paymentData.orderCode > Math.Pow(2.0, 53.0) - 1.0)
            {
                throw new ArgumentOutOfRangeException("orderCode", "orderCode is out of range.");
            }

            string signature = SignatureControl.CreateSignatureOfPaymentRequest(paymentData, _checksumKey);
            paymentData = paymentData with { signature = signature };

            string jsonString = JsonConvert.SerializeObject(paymentData);
            string url = "https://api-merchant.payos.vn/v2/payment-requests";

            JObject responseBodyJson = await SendRequestAsync(url, HttpMethod.Post, jsonString);

            string code = responseBodyJson["code"]?.ToString();
            string desc = responseBodyJson["desc"]?.ToString();
            string data = responseBodyJson["data"]?.ToString();

            if (code == null || code != "00" || data == null)
            {
                throw new PayOSError(code ?? "20", desc ?? "Internal Server Error.");
            }

            JObject dataJson = JObject.Parse(data);
            string paymentLinkResSignature = SignatureControl.CreateSignatureFromObj(dataJson, _checksumKey);

            if (paymentLinkResSignature != responseBodyJson["signature"].ToString())
            {
                throw new Exception("Signature mismatch: The data is unreliable.");
            }

            return JsonConvert.DeserializeObject<CreatePaymentResult>(data) ?? throw new InvalidOperationException("Deserialization failed.");
        }

        public async Task<PaymentLinkInformation> getPaymentLinkInformation(long orderId)
        {
            string url = $"https://api-merchant.payos.vn/v2/payment-requests/{orderId}";
            JObject responseBodyJson = await SendRequestAsync(url, HttpMethod.Get);

            string code = responseBodyJson["code"]?.ToString();
            string desc = responseBodyJson["desc"]?.ToString();
            string data = responseBodyJson["data"]?.ToString();

            if (code == null || code != "00" || data == null)
            {
                throw new PayOSError(code ?? "20", desc ?? "Internal Server Error.");
            }

            JObject dataJson = JObject.Parse(data);
            string paymentLinkResSignature = SignatureControl.CreateSignatureFromObj(dataJson, _checksumKey);

            if (paymentLinkResSignature != responseBodyJson["signature"].ToString())
            {
                throw new Exception("Signature mismatch: The data is unreliable.");
            }

            return JsonConvert.DeserializeObject<PaymentLinkInformation>(data) ?? throw new InvalidOperationException("Deserialization failed.");
        }
    }
}
