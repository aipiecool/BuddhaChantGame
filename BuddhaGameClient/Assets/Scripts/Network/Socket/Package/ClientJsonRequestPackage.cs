using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public abstract class ClientJsonRequestPackage : ClientRequestPackage
	{
        Dictionary<string, string> mValues = new Dictionary<string, string>();		

        public void addKeyValue(string key, string value)
        {
            mValues.Add(key, value);
        }

        public void addKeyValue(string key, int value)
        {
            addKeyValue(key, value.ToString());
        }

        public void addKeyValue(string key, float value)
        {
            addKeyValue(key, value.ToString());
        }

        protected override byte[] getBody()
        {
            string json = JsonUtils.serialize(mValues);
            byte[] body = BytesUtils.string2Bytes(json);
            return body;
        }       
    }
}
