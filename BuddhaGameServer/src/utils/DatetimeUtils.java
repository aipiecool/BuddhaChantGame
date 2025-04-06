package utils;

import java.text.SimpleDateFormat;
import java.util.Date;

public class DatetimeUtils {

    public static final SimpleDateFormat FORMAT_DATETIME = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");

    public static long getTimestampsLong(){
        return System.currentTimeMillis();
    }

    public static String getDatatime(){
        return FORMAT_DATETIME.format(new Date());
    }
}
