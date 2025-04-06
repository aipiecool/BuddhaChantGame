using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class SoundsManager : MonoBehaviour
	{
		private static SoundsManager sInstance;

		public AudioSource audioSource;
		private AudioClip[] mAudioClips;
		private Hashtable mName2AudioClips = new Hashtable();

		private void Awake()
        {
			sInstance = this;
			mAudioClips = new AudioClipLoader().load("Sounds/");
			foreach (AudioClip clip in mAudioClips)
            {
				mName2AudioClips.Add(clip.name, clip);
			}
		}

		public static SoundsManager get()
        {
			return sInstance;
        }

		public void playSound(string name)
        {
            if (!audioSource.isPlaying && name != null)
            {
				if (mName2AudioClips.ContainsKey(name))
				{
					audioSource.clip = mName2AudioClips[name] as AudioClip;
					audioSource.Play();
				}
				else
				{
					Log.input().warn("playSound false: no clip name " + name);
				}
			}           
		}
     
    }
}
