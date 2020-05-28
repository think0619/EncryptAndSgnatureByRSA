using EncryptSign.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptSign
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frm_Login());
        }
    }
    public static class LoginUser
    {
        public static int ID { get; set; }
        public static string Uid { get; set; }
        public static string Name { get; set; }
        public static string PublicKey { get; set; }
        public static string PrivateKey { get; set; }
    }
}
