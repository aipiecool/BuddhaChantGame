using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class CharacterInfo
	{
		public CharacterInfoEntity mEntity;

		private static Sprite[][] sCharacterSprites;

		public CharacterInfo(CharacterInfoEntity entity)
        {
			mEntity = entity;
		}

		public void render(SpriteRenderer player)
        {
			Sprite[][] characterSprites = getCharacterSprites();
			player.sprite = characterSprites[getGender()][0];
			player.color = getColor();
		}

		public Sprite[] getAllSprites()
        {
			 return getCharacterSprites()[getGender()];
		}

		public int getGender()
        {
			return mEntity.gender;

		}

		public Color getColor()
        {
			return new Color(mEntity.colorR, mEntity.colorG, mEntity.colorB);
		}

		private Sprite[][] getCharacterSprites()
        {
			if(sCharacterSprites == null)
            {				
				sCharacterSprites = CharacterSpritesLoadUtils.loadAllCharacterSprites();
			}
			return sCharacterSprites;		
        }
	}

	public class CharacterInfoEntity
	{
		public bool isCreated;
		public int gender;
		public float colorR;
		public float colorG;
		public float colorB;
	}

	public class CharacterInfoEntityWithScene : CharacterInfoEntity
	{
		public int sceneId;
		public int enterId;
	}
}
