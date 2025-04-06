using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BuddhaGame
{
    public class Enter : MonoBehaviour
    {
        public int enterId;
        public string targetScene;
        public int targetEnterId;
        public int direction;

        private void Start()
        {
            
        }

        void OnTriggerEnter2D(Collider2D collidedObject)
        {
            if (collidedObject.gameObject.name.Equals("MainPlayer"))
            {
                Log.input().debug("玩家触发入口:" + enterId + "(" + WorldScene.getCurrentSceneName() + ")");
                SceneSwitchParameters.sTargetEnter = targetEnterId;
                SceneSwitch.get().switchScene(()=>
                {
                    SceneManager.LoadScene(targetScene);
                });              
            }
        }

        public Vector3 getSpawnPoint()
        {
            Vector3 pos = transform.GetChild(0).position;
            return new Vector3(pos.x, pos.y, -1);
        }

        public int getDirection()
        {
            return direction;
        }
    }
}
