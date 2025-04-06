using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
    public class MainPlayerControl : MonoBehaviour
    {

        private AnimatorRender mAnimatorRender;
        private Rigidbody2D mRigbody2D;
        private float mMoveSpeed = 30f;
        private Vector2 mLastPostion;
        private int mDirection;


        // Start is called before the first frame update
        void Start()
        {
            mRigbody2D = gameObject.GetComponent<Rigidbody2D>();
            mAnimatorRender = gameObject.GetComponent<AnimatorRender>();
            mAnimatorRender.addAnimator(new Animator(new int[] { 6 }, false), PlayerAnimatorAction.ANIM_ACTION_IDLE_LEFT);
            mAnimatorRender.addAnimator(new Animator(new int[] { 3 }, false), PlayerAnimatorAction.ANIM_ACTION_IDLE_UP);
            mAnimatorRender.addAnimator(new Animator(new int[] { 6 }, true), PlayerAnimatorAction.ANIM_ACTION_IDLE_RIGHT);
            mAnimatorRender.addAnimator(new Animator(new int[] { 0 }, false), PlayerAnimatorAction.ANIM_ACTION_IDLE_DOWN);
            mAnimatorRender.addAnimator(new Animator(new int[] { 6, 7, 8, 7 }, false), PlayerAnimatorAction.ANIM_ACTION_MOVE_LEFT);
            mAnimatorRender.addAnimator(new Animator(new int[] { 3, 4, 5, 4 }, false), PlayerAnimatorAction.ANIM_ACTION_MOVE_UP);
            mAnimatorRender.addAnimator(new Animator(new int[] { 6, 7, 8, 7 }, true), PlayerAnimatorAction.ANIM_ACTION_MOVE_RIGHT);
            mAnimatorRender.addAnimator(new Animator(new int[] { 0, 1, 2, 1 }, false), PlayerAnimatorAction.ANIM_ACTION_MOVE_DOWN);
            mAnimatorRender.setCondition(PlayerAnimatorAction.ANIM_ACTION_IDLE_DOWN);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            moveCountrol();
        }       

        public int getDirection()
        {
            return mDirection;
        }

        private void moveCountrol()
        {                      
            if (MoveInput.get().IsUpPress())
            {
                mDirection = PlayerAnimatorAction.ANIM_ACTION_IDLE_UP;
                mAnimatorRender.setCondition(PlayerAnimatorAction.ANIM_ACTION_MOVE_UP);
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * mMoveSpeed);
            }
            if (MoveInput.get().IsDownPress())
            {
                mDirection = PlayerAnimatorAction.ANIM_ACTION_IDLE_DOWN;
                mAnimatorRender.setCondition(PlayerAnimatorAction.ANIM_ACTION_MOVE_DOWN);
                GetComponent<Rigidbody2D>().AddForce(Vector2.down * mMoveSpeed);
            }
            if (MoveInput.get().IsLeftPress())
            {
                mDirection = PlayerAnimatorAction.ANIM_ACTION_IDLE_LEFT;
                mAnimatorRender.setCondition(PlayerAnimatorAction.ANIM_ACTION_MOVE_LEFT);
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * mMoveSpeed);

            }
            if (MoveInput.get().IsRightPress())
            {
                mDirection = PlayerAnimatorAction.ANIM_ACTION_IDLE_RIGHT;
                mAnimatorRender.setCondition(PlayerAnimatorAction.ANIM_ACTION_MOVE_RIGHT);
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * mMoveSpeed);
            }

            if (MoveInput.get().IsUpRelease())
            {
                mAnimatorRender.setCondition(PlayerAnimatorAction.ANIM_ACTION_IDLE_UP);
            }
            if (MoveInput.get().IsDownRelease())
            {
                mAnimatorRender.setCondition(PlayerAnimatorAction.ANIM_ACTION_IDLE_DOWN);
            }
            if (MoveInput.get().IsLeftRelease())
            {
                mAnimatorRender.setCondition(PlayerAnimatorAction.ANIM_ACTION_IDLE_LEFT);
            }
            if (MoveInput.get().IsRightRelease())
            {
                mAnimatorRender.setCondition(PlayerAnimatorAction.ANIM_ACTION_IDLE_RIGHT);
            }
        }
    }
}
