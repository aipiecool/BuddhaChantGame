using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class MoveInput : MonoBehaviour
	{
        public Joystick joystick;

        private static MoveInput sInstance;
        private bool mLastIsPress = false;
        private int mLastDirection = -1;
        public bool[] mReleaseDirection = new bool[4];

        private void Awake()
        {
            sInstance = this;
        }

        public static MoveInput get()
        {
            return sInstance;
        }

        private void Update()
        {            
            if(mLastIsPress && !joystick.isPointed)
            {
                mReleaseDirection[mLastDirection] = true;                
            }
            else 
            {
                
            }
            if(mLastDirection != joystick.direction && mLastDirection >= 0)
            {
                mReleaseDirection[mLastDirection] = true;
            }
            mLastIsPress = joystick.isPointed;
            mLastDirection = joystick.direction;
        }

        public bool IsUpPress()
        {   bool joystickInput = joystick.isPointed && joystick.direction == 1;
            return joystickInput || Input.GetKey(KeyCode.W);
        }

        public bool IsDownPress()
        {
            bool joystickInput = joystick.isPointed && joystick.direction == 3;
            return joystickInput || Input.GetKey(KeyCode.S);
        }

        public bool IsLeftPress()
        {
            bool joystickInput = joystick.isPointed && joystick.direction == 0;
            return joystickInput || Input.GetKey(KeyCode.A);
        }

        public bool IsRightPress()
        {
            bool joystickInput = joystick.isPointed && joystick.direction == 2;
            return joystickInput || Input.GetKey(KeyCode.D);
        }

        public bool IsUpRelease()
        {
            bool ret = mReleaseDirection[1];
            mReleaseDirection[1] = false;
            return ret || Input.GetKeyUp(KeyCode.W);
        }

        public bool IsDownRelease()
        {
            bool ret = mReleaseDirection[3];
            mReleaseDirection[3] = false;
            return ret || Input.GetKeyUp(KeyCode.S);
        }

        public bool IsLeftRelease()
        {
            bool ret = mReleaseDirection[0];
            mReleaseDirection[0] = false;
            return ret || Input.GetKeyUp(KeyCode.A);
        }

        public bool IsRightRelease()
        {
            bool ret = mReleaseDirection[2];
            mReleaseDirection[2] = false;
            return ret || Input.GetKeyUp(KeyCode.D);
        }
    }
}
