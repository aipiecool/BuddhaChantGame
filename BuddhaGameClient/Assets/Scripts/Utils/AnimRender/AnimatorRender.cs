using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class AnimatorRender : MonoBehaviour
	{
		private SpriteRenderer mSpriteRenderer;
		private Sprite[] mSprites;
		private Hashtable mAnimatorMap = new Hashtable();
		private object mCurrentKey;
		private RuntimeController mRuntimeController;
	

		private void Start()
        {
			mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();			
			mRuntimeController = new RuntimeController(0.1f);	
		}	

		public void initialze(CharacterInfo characterInfo)
        {
			characterInfo.render(mSpriteRenderer);
			mSprites = characterInfo.getAllSprites();
		}

		public void addAnimator(Animator animator, object key)
        {
			mAnimatorMap.Add(key, animator);
		}

		public void setCondition(object key)
        {
			mCurrentKey = key;

		}

        public void FixedUpdate()
        {
			if (mCurrentKey != null && mSprites != null)
			{
				mRuntimeController.run(()=>{
					Animator animator = mAnimatorMap[mCurrentKey] as Animator;
					if (animator != null)
					{
						int index = animator.run();
						mSpriteRenderer.sprite = mSprites[index];
						mSpriteRenderer.flipX = animator.isFlipX();

					}
				});			
			}
			
		}
    }
}
