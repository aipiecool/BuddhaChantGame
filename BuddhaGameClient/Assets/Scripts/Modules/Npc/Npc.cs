using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class Npc : MonoBehaviour, Clickable
	{
        public SpriteLabel spriteLabel;

        private string npcName;
        private string localName;
        private string firstSpeak;
        private string secondSpeak;
       
        private NpcModel mModel = new NpcModel();

        private void Start()
        {
            spriteLabel.setInitialzedCallback(() =>
            {
                spriteLabel.setText(localName);
                spriteLabel.setSize(500, 1);
                spriteLabel.setOffset(0, 0.1f);
                spriteLabel.setColor(Color.green);
            });
        }

        public void initializeByResponseSceneNpcInfo(SceneNpcInfoResponse info)
        {
            npcName = info.name;
            localName = info.localName;
            firstSpeak = info.firstSpeak;
            secondSpeak = info.secondSpeak;       
            transform.position = new Vector3(info.positionX, info.positionY, -0.5f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name.Equals("MainPlayer"))
            {
                Log.input().debug("玩家触发NPC:" + npcName);
                ActionButtonsManager.get().show(this, "与" + localName + "对话", ()=>{
                    showNpcDialog();
                });
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name.Equals("MainPlayer"))
            {
                Log.input().debug("玩家离开NPC:" + npcName);
                ActionButtonsManager.get().hide(this);
            }
        }

        public void onClick(RaycastHit2D hit)
        {
            float distance = Vector3.Distance(transform.position, MainPlayer.get().transform.position);
            if(distance < 0.52f)
            {
                showNpcDialog();
            }            
        }

        private void showNpcDialog()
        {
            Log.input().debug("请求npc对话框:" + npcName);
            NpcDialog.get().show(this);
            mModel.requestNpcSpeak(this, (response)=>
            {
                if (!string.IsNullOrEmpty(response.firstSpeak))
                {
                    //使用脚本话术
                    if (!response.firstSpeak.Equals(firstSpeak))
                    {
                        //第一次对话
                        NpcDialog.get().show(new NpcSpeakInfo(this, response.firstSpeak));
                    }
                    else
                    {
                        //第二次对话
                        NpcDialog.get().show(new NpcSpeakInfo(this, response.secondSpeak));
                    }
                    firstSpeak = response.firstSpeak;
                    secondSpeak = response.secondSpeak;
                }
                else
                {
                    //使用默认话术
                    NpcDialog.get().show(new NpcSpeakInfo(this, firstSpeak));
                }                
            });
        }       

        public string getName()
        {
            return npcName;
        }

        public string getLocalName()
        {
            return localName;
        }
    }
}
