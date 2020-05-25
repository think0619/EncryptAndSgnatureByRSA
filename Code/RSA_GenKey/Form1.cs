using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace RSA_GenKey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

            string str_Private_Key = Convert.ToBase64String(RSAalg.ExportCspBlob(true));
            string str_Public_Key = Convert.ToBase64String(RSAalg.ExportCspBlob(false));

            textBox1.Text = str_Public_Key;
            textBox2.Text = str_Private_Key;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

            string str_Private_Key = RSAalg.ToXmlString(true);
            string str_Public_Key = RSAalg.ToXmlString(false);

            textBox1.Text = str_Public_Key;
            textBox2.Text = str_Private_Key;
        }
    }
}