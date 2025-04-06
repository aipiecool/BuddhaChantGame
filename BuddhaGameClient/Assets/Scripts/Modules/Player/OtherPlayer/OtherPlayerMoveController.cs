using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class OtherPlayerMoveController : MonoBehaviour
	{
 
        private AnimatorRender mAnimatorRender;
        private RuntimeController mRuntimeController;
        private Queue<PlayerPosition> mMovePath = new Queue<PlayerPosition>();       
        private Vector2 mLastPosition;       

        private void Start()
        {            
            mAnimatorRender = GetComponent<AnimatorRender>();
            mAnimatorRender.addAnimator(new Animator(new int[] { 6 }, false), PlayerAnimatorAction.ANIM_ACTION_IDLE_LEFT);
            mAnimatorRender.addAnimator(new Animator(new int[] { 3 }, false), PlayerAnimatorAction.ANIM_ACTION_IDLE_UP);
            mAnimatorRender.addAnimator(new Animator(new int[] { 6 }, true), PlayerAnimatorAction.ANIM_ACTION_IDLE_RIGHT);
            mAnimatorRender.addAnimator(new Animator(new int[] { 0 }, false), PlayerAnimatorAction.ANIM_ACTION_IDLE_DOWN);
            mAnimatorRender.addAnimator(new Animator(new int[] { 6, 7, 8, 7 }, false), PlayerAnimatorAction.ANIM_ACTION_MOVE_LEFT);
            mAnimatorRender.addAnimator(new Animator(new int[] { 3, 4, 5, 4 }, false), PlayerAnimatorAction.ANIM_ACTION_MOVE_UP);
            mAnimatorRender.addAnimator(new Animator(new int[] { 6, 7, 8, 7 }, true), PlayerAnimatorAction.ANIM_ACTION_MOVE_RIGHT);
            mAnimatorRender.addAnimator(new Animator(new int[] { 0, 1, 2, 1 }, false), PlayerAnimatorAction.ANIM_ACTION_MOVE_DOWN);
            mAnimatorRender.setCondition(PlayerAnimatorAction.ANIM_ACTION_IDLE_DOWN);

            mRuntimeController = new RuntimeController(0.02f);
        }

        public void setTargetPosition(Vector2 target, int direction)
        {                  
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 dPosition = target - currentPosition;
            float stepDistance = 0.006f;
            int lerpCount = (int)(dPosition.magnitude / stepDistance);       
            for (int i=0; i<lerpCount; i++)
            {
                mMovePath.Enqueue(new PlayerPosition( Vector2.Lerp(currentPosition, target, ((float)i / lerpCount)), direction));
            }           

        }

        private void Update()
        {
            mRuntimeController.run(() =>
            {
                if (mMovePath.Count > 0)
                {
                    PlayerPosition currentPlayerPos = mMovePath.Dequeue();
                    Vector2 currentPos = currentPlayerPos.position;
                    if (mLastPosition != null)
                    {                      
                        //int dir = VectorUtils.getDirectionByVector2(mLastPosition, currentPos);
                        int anim = currentPlayerPos.direction;
                        if (mMovePath.Count > 1)
                        {
                            anim += 4;
                        }
                        mAnimatorRender.setCondition(anim);
                    }
                    transform.position = currentPos;
                    mLastPosition = currentPos;
                }
            });
        }

    }
}
