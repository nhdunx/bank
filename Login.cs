using System.Text;

namespace KLFixLag
{ ///  Zermango  https://www.facebook.com/khangpcnopro
    public partial class Login : Form
    {
        public static string _clientId = "";
        public static string _apiKey_ = "";
        public static string _checksum = "";

        PayOS payOS;
        public Login()
        {
            InitializeComponent();
            PerformLoginAndSetupAsync();
        }
    
        private async void PerformLoginAndSetupAsync()
        {
            await Task.Run(() =>
            {
                payOS = new PayOS();
                pay();
            });

        }
     
        private void Login_Load(object sender, EventArgs e)
        {
         /////// Dùng timer1 để check bank 5 giây 1 lần
            timer1.Interval = 5000; // 5000ms = 5 giây
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();

        }

        int orderID = 0;
        private static Random random = new Random();
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }
        int code = 0;
        public async void pay()
        {
            var soluong = Convert.ToInt32("1"); ///// số lượng
            int tongGiaTien = 0;

            tongGiaTien = Convert.ToInt32("99000");  ////// giá tiền

            int thanhtien = 0;
            ItemData item = new ItemData("", soluong, thanhtien);
            ItemData[] items = new ItemData[1];
            items[0] = item;

            var noidung = "Thanh Toan Fixlag" + " " + GenerateRandomString(2);

            orderID = random.Next(100000, 999999);

            PaymentData paymentData = new PaymentData(orderID, tongGiaTien, noidung,
                  items, "https://localhost:3002", "https://localhost:3002");

            var createPayment = await payOS.createPaymentLink(paymentData);
            var linkCheckOut = createPayment.checkoutUrl;
            var a = createPayment.qrCode;
            code = (int)createPayment.orderCode;
            var imageUrl = $"https://img.vietqr.io/image/{createPayment.bin}-{createPayment.accountNumber}-qr_only.jpg?addInfo={createPayment.description}&amount={createPayment.amount}";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(imageUrl))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (Stream stream = await response.Content.ReadAsStreamAsync())
                            {
                                using (MemoryStream memoryStream = new MemoryStream())
                                {
                                    await stream.CopyToAsync(memoryStream);
                                    memoryStream.Seek(0, SeekOrigin.Begin);

                                    Bitmap bitmap = new Bitmap(memoryStream);

                                    pictureBox2.Image = bitmap;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Failed to download image. Status code: {response.StatusCode}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading image: {ex.Message}");
            }
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                var createPayment1 = await payOS.getPaymentLinkInformation((int)code);
                if (createPayment1.status == "PENDING")
                {
                    MessageBox.Show("Chưa thanh toán");

                }
                else if (createPayment1.status == "PAID")
                {
                    MessageBox.Show("Đã thanh toán");
                    timer1.Stop();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
