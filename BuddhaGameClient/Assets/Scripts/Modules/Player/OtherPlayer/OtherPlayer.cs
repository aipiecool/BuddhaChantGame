using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class OtherPlayer : MonoBehaviour
	{
        private OtherPlayerMoveController mMoveController;
        private AnimatorRender mAnimatorRender;
        private FlowSpriteLabel mChantLabel;

        private int mPlayerId;
        private SpriteLabel mPlayerNameLabel;
        private Runable mInitializedCallback;
        private int mLifetime = 1;
        private SocketPackageNet mSocket;
        private Queue<int> mChantQueue = new Queue<int>();
        private RuntimeController mPlayChantController = new RuntimeController(0.5f);

        static OtherPlayer()
        {
            ServerPackageManager.get().registerServerPackage(new SimpleServerResponsePackage("PlayerInfoResponse"));
        }

        private void Start()
        {
            mSocket = NetworkFactory.getSocketNet();            

            mMoveController = gameObject.GetComponent<OtherPlayerMoveController>();
            mAnimatorRender = GetComponent<AnimatorRender>();
            mChantLabel = GetComponent<FlowSpriteLabel>();
            mPlayerNameLabel = gameObject.AddComponent<SpriteLabel>();
            mPlayerNameLabel.setInitialzedCallback(() =>{
                mPlayerNameLabel.setSize(500, 1);
                mPlayerNameLabel.setOffset(0, 0.1f);
                mPlayerNameLabel.setText("玩家");                
            });
            
            mInitializedCallback();
        }

        public void whenInitialized(Runable callback)
        {
            mInitializedCallback = callback;
        }

        public void initialize(int id, Vector2 position)
        {
            mPlayerId = id;
            transform.position = position;
            mSocket.sendRequest(new SocketRequest(new SimpleClientRequestPackage("PlayerInfoRequest", id), (response) =>
            {
                if (response.isSuccess() && response.getCode() == 1)
                {
                    string jsonString = response.getMessage();
                    ThreadLooper.get().runMainThread(() =>
                    {
                        try
                        {
                            OtherPlayerInfo entity = JsonUtils.unserialize<OtherPlayerInfo>(jsonString);
                            mPlayerNameLabel.setText(entity.username);
                            mAnimatorRender.initialze(new CharacterInfo(entity.characterInfo));

                        }
                        catch (Exception e)
                        {                            
                            Log.input().warn("请求其他玩家信息错误:\n" + jsonString + "\n" +e.ToString());                        
                        }
                    });                   
                }
                else
                {
                    Log.input().warn("请求其他玩家信息错误 超时:" + response.ToString());
                    Toast.get().show("网络不稳定", 2);
                }
            }));
        }

        public void addChant(int wordId, int count)
        {
            for(int i=0; i<count; i++)
            {
                mChantQueue.Enqueue(wordId);
            }
        }

        private void Update()
        {
            if (mChantQueue.Count > 0)
            {
                float runSpeed = 5f / mChantQueue.Count;
                mPlayChantController.setRunSpped(runSpeed);
                mPlayChantController.run(() =>
                {
                    int wordId = mChantQueue.Dequeue();
                    mChantLabel.addText(BuddhaNames.getNameById(wordId));
                });
            }
        }

        public void setPosition(Vector2 position, int direction)
        {
            mMoveController.setTargetPosition(position, direction);
            mLifetime = 1;
        }

        public int getPlayerId()
        {
            return mPlayerId;
        }

        public void setLifetime(int lifetime)
        {
            mLifetime = lifetime;
        }

        public int getLifetime()
        {
            return mLifetime;
        }

        public void destory()
        {
            mChantLabel.destory();
            mPlayerNameLabel.destory();
            Destroy(gameObject);            
        }
    }
}
