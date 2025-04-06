using Assets.Scripts.Utils.NotifyData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{    public class MainPlayer : MonoBehaviour 
    {
        private static MainPlayer sInstance;

        public AnimatorRender animatorRender;
        public SpriteLabel usernameLabel;
        public FlowSpriteLabel chantLabel;

        private MainPlayerNetwork mPlayerNetwork;
        private MainPlayerControl mMainPlayerControl;
        private NotifyData<PlayerPosition> mPosition = new NotifyData<PlayerPosition>();        


        void Start()
        {
            sInstance = this;
            mPosition.setNotifyIntervalTime(0.1f);
            mPosition.setSynchronizeTime(1f);
            mPlayerNetwork = new MainPlayerNetwork(this);

            mMainPlayerControl = GetComponent<MainPlayerControl>();


            animatorRender.initialze(UserRecorder.get().GetCharacterInfo());
            usernameLabel.setText(UserRecorder.get().getUsername());
            usernameLabel.setSize(500, 1);
            usernameLabel.setOffset(0, 0.1f);
            usernameLabel.setColor(Color.yellow);

            List<Enter> enters = WorldScene.getCurrentWordScene().enters;
            foreach(Enter enter in enters)
            {
                if(enter.enterId == SceneSwitchParameters.sTargetEnter)
                {
                    transform.position = enter.getSpawnPoint();
                    animatorRender.setCondition(enter.getDirection());
                }
            }

            BuddhaChantInput.get().addCallback(onBuddhaChant);
        }

        public static MainPlayer get()
        {
            return sInstance;
        }

        private void onBuddhaChant(int wordId)
        {
            chantLabel.addText(BuddhaNames.getNameById(wordId));
        }

        public void addPositionObserver(NotifyDataObserver<PlayerPosition> observer)
        {
            mPosition.addObserver(observer);
        }     

        public void removePositionObserver(NotifyDataObserver<PlayerPosition> observer)
        {
            mPosition.removeObserver(observer);
        }         
       
        void FixedUpdate()
        {
            mPosition.setValue(new PlayerPosition(transform.position, mMainPlayerControl.getDirection()));
           
        }

        
    }

    public class PlayerPosition
    {
        public Vector2 position;
        public int direction;

        public PlayerPosition(Vector3 position, int direction)
        {
            this.position = position;
            this.direction = direction;
        }
    }
}

