package network.socket.actions;

import network.socket.SocketAddress;
import utils.BytesUtils;
import utils.EncodeUtils;

import java.io.UnsupportedEncodingException;

public abstract class ServerPackage {

    protected byte[] mHeader;
    protected SocketAddress mAddress;

    public ServerPackage(SocketAddress address) {
        mAddress = address;
    }

    public SocketAddress getAddress() {
        return mAddress;
    }

    public abstract  String getHeaderString();

    protected abstract  byte[] getBody() throws UnsupportedEncodingException;

    public byte[] getHeader()
    {
        if(mHeader == null)
        {
            mHeader = BytesUtils.int2Bytes(EncodeUtils.string2HashCode(getHeaderString()));
        }
        return mHeader;
    }

    public byte[] getBytes() throws UnsupportedEncodingException {
        byte[] header = getHeader();
        byte[] body = getBody();
        return BytesUtils.bytesConcat(header, body);
    }

    @Override
    public String toString() {
        return getHeaderString() + "(" + mHeader + ")=>" + mAddress.toString();
    }
}
