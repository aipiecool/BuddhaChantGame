using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BuddhaGame
{
    public class BytesUtils
    {
        public static byte[] bytesConcat(byte[] b1, byte[] b2)
        {
            byte[] b3 = new byte[b1.Length + b2.Length];
            Buffer.BlockCopy(b1, 0, b3, 0, b1.Length);
            Buffer.BlockCopy(b2, 0, b3, b1.Length, b2.Length);
            return b3;
        }    

        //end为-1时拷贝从start到最后一位
        public static byte[] subbytes(byte[] bytes , int start, int end)
        {            
            if (end > 0)
            {
                if(start > end)
                {
                    throw new Exception("start > end");
                }               
                byte[] b1 = new byte[end - start];
                if (b1.Length > bytes.Length - start)
                {
                    throw new Exception("b1.Length > bytes.Length - start");
                }
                Buffer.BlockCopy(bytes, start, b1, 0, b1.Length);
                return b1;
            }
            else
            {
                int length = bytes.Length - start;
                byte[] b1 = new byte[length];
                Buffer.BlockCopy(bytes, start, b1, 0, b1.Length);
                return b1;
            }
        }

        public static int bytes2Int(byte[] bytes)
        {
            int result = 0;
            //将每个byte依次搬运到int相应的位置
            result = bytes[3] & 0xff;
            result = result << 8 | bytes[2] & 0xff;
            result = result << 8 | bytes[1] & 0xff;
            result = result << 8 | bytes[0] & 0xff;
            return result;
        }

        public static byte[] int2Bytes(int num)
        {
            byte[] bytes = new byte[4];
            //通过移位运算，截取低8位的方式，将int保存到byte数组
            bytes[3] = (byte)(num >> 24);
            bytes[2] = (byte)(num >> 16);
            bytes[1] = (byte)(num >> 8);
            bytes[0] = (byte)num;
            return bytes;
        }

        public static byte[] string2Bytes(String text)
        {
            return Encoding.Default.GetBytes(text);
        }

        public static string bytes2String(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }       

        public static byte[] float2Bytes(float value)
        {
            return BitConverter.GetBytes(value);
        }

        public static float bytes2Float(byte[] bytes)
        {
            return BitConverter.ToSingle(bytes, 0);
        }

        public static byte[] vector2Bytes(Vector2 vector2)
        {
            return BytesUtils.bytesConcat(float2Bytes(vector2.x), float2Bytes(vector2.y));
        }

        public static Vector2 bytes2Vector(byte[] bytes)
        {
            byte[] xBytes = subbytes(bytes, 0, 4);
            byte[] yBytes = subbytes(bytes, 4, 8);
            return new Vector2(bytes2Float(xBytes), bytes2Float(yBytes));
        }
    }
}
