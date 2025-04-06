package network.socket.actions.unpackage;

import network.socket.SocketAddress;
import utils.BytesUtils;

public class ClientUnpackage implements Unpackageable{

    protected int mHeader;
    protected byte[] mBody;
    protected SocketAddress mAddress;

    public ClientUnpackage(byte[] srcBytes, SocketAddress address) throws Exception  {
        mAddress = address;
        mHeader = BytesUtils.bytes2Int(BytesUtils.subbytes(srcBytes, 0, 4));
        mBody = BytesUtils.subbytes(srcBytes, 4, -1);
    }

    @Override
    public int getHeader()
    {
        return mHeader;
    }

    @Override
    public byte[] getBody()
    {
        return mBody;
    }

    @Override
    public SocketAddress getAddress() {
        return mAddress;
    }
}
