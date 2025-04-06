using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class MessagesManager 
	{
		private static MessagesManager sInstance;
		private static object sSingletonLock = new object();

        private MessageModel mMessageModel = new MessageModel();

        static MessagesManager()
        {
            ServerPackageManager.get().registerServerPackage(new NeedRequestMessagePackage());           
        }

        private MessagesManager()
        {
            MessagePackageListener messagePackageListener = new MessagePackageListener(this);
            messagePackageListener.subscribePackages(new NeedRequestMessagePackage());
        }

        public void requestFlowText()
        {
            mMessageModel.requestFlowText((flowTexts)=>
            {
                foreach(FlowTextResponseInfo info in flowTexts)
                {
                    ScreenFlowText.get().addText(info.message, info.sound);
                }
                PortraitView.get()?.requestUpdate();
            });
        }

        public static MessagesManager get()
        {
            if (sInstance == null)
            {
                lock (sSingletonLock)
                {
                    if (sInstance == null)
                    {
                        sInstance = new MessagesManager();
                    }
                }
            }
            return sInstance;
        }

        private class MessagePackageListener : PackageListener
        {
            MessagesManager mOuter;
            public MessagePackageListener(MessagesManager outer)
            {
                mOuter = outer;
            }

            public override void onReceive(ServerPackage package)
            {
                package.process(mOuter);
            }
        }

        private class NeedRequestMessagePackage : ServerPackage
        {
            private string messageType;
            
            public NeedRequestMessagePackage(ServerUnpackage unpackage)
            {
                byte[] body = unpackage.getBody();
                messageType = BytesUtils.bytes2String(body);              
            }

            public NeedRequestMessagePackage()
            {

            }

            public override ServerPackage create(ServerUnpackage unpackage)
            {
                return new NeedRequestMessagePackage(unpackage);
            }

            public override string getHeaderString()
            {
                return "NeedRequestMessage";
            }

            public override void process(object outer2)
            {
                switch (messageType)
                {
                    case "FlowText":
                        get().requestFlowText();
                        break;
                }
            }
        }
    }
}
