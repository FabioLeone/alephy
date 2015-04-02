using System;
using System.Security.Cryptography;
using System.Text;

namespace SIAO.SRV
{
	public class CDM
	{
		public CDM ()
		{
		}

		public static string Cript(string strText)
		{
			string c = String.Empty;
			var sha1 = SHA256Managed.Create();
			byte[] iptByte = Encoding.UTF8.GetBytes(strText);
			byte[] outByte = sha1.ComputeHash(iptByte);

			c = Convert.ToBase64String(outByte);

			return c;
		}
	}
}

