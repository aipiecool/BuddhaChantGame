using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class OtherPlayerInfo 
	{
		public int userId;
		public string username;
		[JsonProperty("CharacterInfo")]
		public CharacterInfoEntity characterInfo;
	}
}
