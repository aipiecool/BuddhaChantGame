using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class Animator 
	{
        private int[] mSeq;
        private bool mFlipX;
        private int pointer = 0;

		public Animator(int[] seq, bool flipX=false)
        {
            mSeq = seq;
            mFlipX = flipX;
        }

        public int run()
        {
            if(pointer >= mSeq.Length)
            {
                pointer = 0;
            }
            return mSeq[pointer++];
        }

        internal bool isFlipX()
        {
            return mFlipX;
        }
    }
}
