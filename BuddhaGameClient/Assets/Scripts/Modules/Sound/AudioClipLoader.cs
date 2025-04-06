using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class AudioClipLoader 
	{
		public AudioClip[] load(string folder)
        {
			return Resources.LoadAll<AudioClip>(folder);
        }
	}
}
