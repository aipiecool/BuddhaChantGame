using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace BuddhaGame
{
	public class EncodeUtils 
	{
		public static int string2HashCode(string value)
		{
			int h = 0;
			if (h == 0 && value.Length > 0)
			{
				char[] val = value.ToCharArray();

				for (int i = 0; i < value.Length; i++)
				{
					h = 31 * h + val[i];
				}				
			}
			
			return h;
		}

		public static string MD5(string source)
		{
			byte[] sor = Encoding.UTF8.GetBytes(source);
			MD5 md5 = System.Security.Cryptography.MD5.Create();
			byte[] result = md5.ComputeHash(sor);
			StringBuilder strbul = new StringBuilder(40);
			for (int i = 0; i < result.Length; i++)
			{
				strbul.Append(result[i].ToString("x2"));
			}
			string md5Result = strbul.ToString();
			return md5Result;
		}
	}
}
