
using System.Runtime.InteropServices;

namespace KLFixLag
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ComWrappers.RegisterForMarshalling(WinFormsComInterop.WinFormsComWrappers.Instance);
            ApplicationConfiguration.Initialize();
            Application.Run(new Login());
            
        }
    }
}