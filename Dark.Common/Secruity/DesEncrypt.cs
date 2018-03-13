using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Dark.Common.Secruity
{
    public class DesEncrypt
    {
        private static string KEY = "dark.security";
        private static byte[] rgbKey = ASCIIEncoding.ASCII.GetBytes(KEY.Substring(0, 8));
        private static byte[] rgbIV = ASCIIEncoding.ASCII.GetBytes(KEY.Insert(0, "w").Substring(0, 8));

        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string Encrypt(string strValue)
        {
            DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
            using (MemoryStream memStream = new MemoryStream())
            {
                CryptoStream crypStream = new CryptoStream(memStream, dsp.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                StreamWriter sWriter = new StreamWriter(crypStream);
                sWriter.Write(strValue);
                sWriter.Flush();
                crypStream.FlushFinalBlock();
                memStream.Flush();
                return Convert.ToBase64String(memStream.GetBuffer(), 0, (int)memStream.Length);
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="EncValue"></param>
        /// <returns></returns>
        public static string Decrypt(string EncValue)
        {
            DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
            byte[] buffer = Convert.FromBase64String(EncValue);

            using (MemoryStream memStream = new MemoryStream())
            {
                CryptoStream crypStream = new CryptoStream(memStream, dsp.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                crypStream.Write(buffer, 0, buffer.Length);
                crypStream.FlushFinalBlock();
                return ASCIIEncoding.UTF8.GetString(memStream.ToArray());
            }
        }
    }
}
