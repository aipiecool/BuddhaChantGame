package utils;

import com.google.gson.Gson;

import java.lang.reflect.Type;

public class JsonUtils {

    private final static Gson sGson = new Gson();

    public static String serialize(Object o){
        return sGson.toJson(o);
    }

    public static <T> T unserialize(String json, Class<T> tClass){
        try {
        return sGson.fromJson(json, tClass);
        }catch (Exception e){
            System.out.println("Json转换错误:\n" + json);
            throw e;
        }
    }

    public static <T> T unserialize(String json, Type typeOfT){
        try {
            return sGson.fromJson(json, typeOfT);
        }catch (Exception e){
            System.err.println("Json转换错误:\n" + json);
            throw e;
        }
    }

}
