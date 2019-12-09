using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Core.Global
{

    /// <summary>
    /// 加密接口
    /// </summary>
    public interface IEncryptionService
    {
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        string MD5(string strValue, int bit = 16);
    }

    /// <summary>
    /// 加密服务
    /// </summary>
    public class EncryptionService : CommonService<EncryptionService>, IEncryptionService
    {
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public string MD5(string strValue, int bit = 16)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(strValue));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            if (bit == 16)
                return tmp.ToString().Substring(8, 16);
            else
            if (bit == 32) return tmp.ToString();//默认情况
            else return string.Empty;
        }
    }
}
