using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public abstract class ClientRequestPackage : ClientPackage
	{
        protected byte[] mPackageId;
              

        public void setPackageId(int id)
        {
            mPackageId = BytesUtils.int2Bytes(id);
        }

        public override byte[] getBytes()
        {
            byte[] header = getHeader();
            byte[] body = getBody();
            return BytesUtils.bytesConcat(header, BytesUtils.bytesConcat(mPackageId, body));
        }
    }
}
