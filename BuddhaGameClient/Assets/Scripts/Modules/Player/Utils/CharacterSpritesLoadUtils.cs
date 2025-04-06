using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class CharacterSpritesLoadUtils 
	{
		public static Sprite[] loadCharacterSprites()
        {
			Sprite[] sprites = new Sprite[2];
			sprites[0] = Resources.LoadAll<Sprite>("Character/Player/Painting/MiniPixel/Player1/male")[0];
			sprites[1] = Resources.LoadAll<Sprite>("Character/Player/Painting/MiniPixel/Player1/female")[0];
			return sprites;
		}
		public static Sprite[][] loadAllCharacterSprites()
		{
			Sprite[][] sprites = new Sprite[2][];
			sprites[0] = Resources.LoadAll<Sprite>("Character/Player/Painting/MiniPixel/Player1/male");
			sprites[1] = Resources.LoadAll<Sprite>("Character/Player/Painting/MiniPixel/Player1/female");
			return sprites;
		}
	}
}
