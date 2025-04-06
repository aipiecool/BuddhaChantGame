package utils;

import java.io.UnsupportedEncodingException;

public class BytesUtils {

    public static byte[] bytesConcat(byte[] b1, byte[] b2)
    {
        byte[] b3 = new byte[b1.length + b2.length];
        System.arraycopy(b1, 0, b3, 0, b1.length);
        System.arraycopy(b2, 0, b3, b1.length, b2.length);
        return b3;
    }

    public static byte[] subbytes(byte[] bytes , int start, int end) throws Exception {
        if (end > 0)
        {
            if(start > end)
            {
                throw new Exception("start > end");
            }
            byte[] b1 = new byte[end - start];
            if (b1.length > bytes.length - start)
            {
                throw new Exception("b1.Length > bytes.Length - start");
            }
            System.arraycopy(bytes, start, b1, 0, b1.length);
            return b1;
        }
        else
        {
            int length = bytes.length - start;
            byte[] b1 = new byte[length];
            System.arraycopy(bytes, start, b1, 0, b1.length);
            return b1;
        }
    }

    public static int bytes2Int(byte[] bytes) {
        int result = 0;
        //将每个byte依次搬运到int相应的位置
        result = bytes[3] & 0xff;
        result = result << 8 | bytes[2] & 0xff;
        result = result << 8 | bytes[1] & 0xff;
        result = result << 8 | bytes[0] & 0xff;
        return result;
    }

    public static byte[] int2Bytes(int num) {
        byte[] bytes = new byte[4];
        //通过移位运算，截取低8位的方式，将int保存到byte数组
        bytes[3] = (byte)(num >>> 24);
        bytes[2] = (byte)(num >>> 16);
        bytes[1] = (byte)(num >>> 8);
        bytes[0] = (byte)num;
        return bytes;
    }

    public static byte[] string2Bytes(String text) throws UnsupportedEncodingException {
        return text.getBytes("UTF-8");
    }

    public static String bytes2String(byte[] bytes) throws UnsupportedEncodingException {
        return new String(bytes, "UTF-8");
    }

    public static float bytes2Float(byte[] bytes){
        return Float.intBitsToFloat(bytes2Int(bytes));
    }

    public static byte[] float2Bytes(float num){
        return int2Bytes(Float.floatToIntBits(num));
    }

    public static void fillBytesWithInt(byte[] bytes, int num, int pos) {
        bytes[pos + 3] = (byte)(num >>> 24);
        bytes[pos + 2] = (byte)(num >>> 16);
        bytes[pos + 1] = (byte)(num >>> 8);
        bytes[pos + 0] = (byte)num;
    }

    public static void fillBytesWithFloat(byte[] bytes, float num, int pos) {
        fillBytesWithInt(bytes, Float.floatToIntBits(num), pos);
    }
}
