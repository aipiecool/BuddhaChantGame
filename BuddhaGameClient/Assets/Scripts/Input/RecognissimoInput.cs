using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Recognissimo;
using Recognissimo.Components;
using UnityEngine;

namespace BuddhaGame
{
    public class RecognissimoInput : MonoBehaviour
    {
        [SerializeField]
        private SpeechRecognizer recognizer;
    
        [SerializeField]
        private StreamingAssetsLanguageModelProvider languageModelProvider;
        
        private static BuddhaChantInput sInstance;
        
        void OnEnable()
        {
            recognizer.Started.AddListener(() =>
            {
                Debug.Log("Started Recognissimo");
            });
        
            recognizer.ResultReady.AddListener(OnResult);
            recognizer.InitializationFailed.AddListener(OnError);
            recognizer.RuntimeFailed.AddListener(OnError);
            recognizer.PartialResultReady.AddListener(OnPartialResult);
        }

        private void OnResult(Result result)
        {
            /*Debug.Log("OnResult:" + result.text);
            string[] texts = result.text.Split(' ');
            foreach (var text in texts)
            {
                TextToNotify(text);
            }*/
        }

        private void OnPartialResult(PartialResult partial)
        {
            Debug.Log("OnPartialResult:" + partial.partial);
            string[] texts = partial.partial.Split(' ');
            TextToNotify(texts.First());
        }
        
        private void TextToNotify(string text)
        {
            if (text.Contains("阿弥陀佛"))
            {
                BuddhaChantInput.get().notify(0);
            }
            if (text.Contains("观世音菩萨") || text.Contains("观音菩萨"))
            {
                BuddhaChantInput.get().notify(1);
            }
            if (text.Contains("地藏王菩萨"))
            {
                BuddhaChantInput.get().notify(2);
            }
        }

        private void OnError(SpeechProcessorException exception)
        {
            Debug.LogError(exception.Message);
        }
        
        public void setOpenRecord(bool open)
        {
            if (open)
            {
                recognizer.StartProcessing();
            }
            else
            {
                recognizer.StopProcessing();
            }
        }
    }

}

