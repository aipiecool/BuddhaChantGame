using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class MainPlayerNetwork 
	{
		private MainPlayer mPlayer;      
		private SocketPackageNet mSocket;

		public MainPlayerNetwork(MainPlayer player)
        {
			mPlayer = player;
            mPlayer.addPositionObserver(ObservePlayerPosition);        
            mSocket = NetworkFactory.getSocketNet();
            BuddhaChantInput.get().addCallback(onBuddhaChant);
         
        }

        private void onBuddhaChant(int wordId)
        {
            mSocket.sendPackage(new PlayerChantPackage(wordId, 1));
        }

        private void ObservePlayerPosition(PlayerPosition position)
        {
            mSocket.sendPackage(new PlayerMovePackage(position.position, position.direction, WorldScene.getCurrentSceneId(), SceneSwitchParameters.sTargetEnter));
        }	
	}

    public class PlayerMovePackage : ClientPackage
    {
        private Vector2 mPosition;
        private int mDirection;
        private int mSceneId;
        private int mEnterId;        

        public PlayerMovePackage(Vector2 position, int direction, int sceneId, int enterId)
        {
            mPosition = position;
            mDirection = direction;
            mSceneId = sceneId;
            mEnterId = enterId;
        }

        protected override byte[] getBody()
        {
            return BytesUtils.bytesConcat(
                BytesUtils.bytesConcat(BytesUtils.int2Bytes(mSceneId), BytesUtils.int2Bytes(mEnterId)), 
                BytesUtils.bytesConcat(BytesUtils.vector2Bytes(mPosition), BytesUtils.int2Bytes(mDirection)));
        }

        public override string getHeaderString()
        {
            return "PlayerMove";
        }
    }

    public class PlayerChantPackage : ClientPackage
    {
        private int mWordId;
        private int mCount;

        public PlayerChantPackage(int wordId, int count)
        {
            mWordId = wordId;
            mCount = count;
        }

        protected override byte[] getBody()
        {
            return BytesUtils.bytesConcat(BytesUtils.int2Bytes(mWordId), BytesUtils.int2Bytes(mCount));
        }

        public override string getHeaderString()
        {
            return "PlayerChant";
        }        
    }
}
