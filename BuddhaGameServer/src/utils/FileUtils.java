package utils;

import java.io.*;

public class FileUtils {
    public static String readTxt(String filename){
        String line;
        StringBuilder stringBuilder = new StringBuilder();
        try{
            // 使用 InputStreamReader 并指定编码格式为 UTF-8
            InputStreamReader reader = new InputStreamReader(new FileInputStream(filename), "UTF-8");
            BufferedReader br = new BufferedReader(reader);
            while ((line=br.readLine())!=null){
                stringBuilder.append(line);
                // 如果需要保留行分隔符，可以在这里添加
                // stringBuilder.append(System.lineSeparator());
            }
            br.close(); // 记得关闭 BufferedReader
        } catch (IOException e){
            e.printStackTrace();
        }
        return stringBuilder.toString();
    }

    public static void writeTxt(String filename, String content){
        try{
            File writeName = new File(filename);

            FileWriter writer = new FileWriter(writeName);
            BufferedWriter out = new BufferedWriter(writer);
            out.write(content);
            out.flush();
        } catch (IOException e){
            e.printStackTrace();
        }
    }

    public static File[] listOfFolder(String path){
        File folder = new File(path);
        return folder.listFiles();
    }
}
