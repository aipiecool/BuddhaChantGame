using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace BuddhaGame
{
	public delegate void HttpCallback(HttpResponse response);
	public class HttpNet 
	{
        private string mBaseUrl;

        public HttpNet(string baseUrl)
        {
            mBaseUrl = baseUrl;
        }

		public void post(HttpRequest request, HttpCallback callback)
        {
            ThreadLooper.get().startCoroutine(postRequest(request, callback));
        }

        private IEnumerator postRequest(HttpRequest request, HttpCallback callback)
        {
            string httpLog = "发送post请求:\n" + mBaseUrl + request.getUrl() + "\n";
            var www = UnityWebRequest.Post(mBaseUrl + request.getUrl(), request.getFormDatas());
            yield return www.SendWebRequest();            
            if (www.isNetworkError || www.isHttpError)
            {
                httpLog += "\n请求返回:\ncode:" + www.responseCode + "\nerror:" + www.error;
                Log.input().info(httpLog);
                callback(new HttpResponse(www.responseCode, www.error));               
            }
            else
            {
                httpLog += "请求返回:\ncode:" + www.responseCode + "\body:" + www.downloadHandler.text;
                Log.input().info(httpLog);
                callback(new HttpResponse(www.responseCode, www.downloadHandler.text));
            }
        }
    }
}
