using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class FlowSpriteLabelChild : MonoBehaviour
	{
        private FlowSpriteLabel.FlowLable mFlowLable;
        private FlowSpriteLabel mFlowSpriteLabel;
        private Transform mOuterTransform;

        private void Start()
        {
            
        }

        public void initialize(FlowSpriteLabel.FlowLable flowLable, FlowSpriteLabel flowSpriteLabel, Transform transform)
        {
            mFlowLable = flowLable;
            mFlowSpriteLabel = flowSpriteLabel;
            mOuterTransform = transform;
        }

        private void Update()
        {
            if (mFlowSpriteLabel == null || mOuterTransform == null)
            {
                mFlowLable.destory();
            }
        }
    }
}
