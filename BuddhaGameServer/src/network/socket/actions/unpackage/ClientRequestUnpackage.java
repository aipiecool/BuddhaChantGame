package network.socket.actions.unpackage;

import network.socket.SocketAddress;
import utils.BytesUtils;

public class ClientRequestUnpackage implements Unpackageable {

    protected int mHeader;
    protected byte[] mBody;
    protected int mPackageId;
    protected SocketAddress mAddress;

    public ClientRequestUnpackage(ClientUnpackage clientUnpackage) throws Exception {
        mHeader = clientUnpackage.getHeader();
        mPackageId = BytesUtils.bytes2Int(BytesUtils.subbytes(clientUnpackage.mBody, 0, 4));
        mBody = BytesUtils.subbytes(clientUnpackage.mBody, 4, -1);
        mAddress = clientUnpackage.getAddress();
    }

    public int getPackageId()
    {
        return mPackageId;
    }

    @Override
    public int getHeader() {
        return mHeader;
    }

    @Override
    public byte[] getBody() {
        return mBody;
    }

    @Override
    public SocketAddress getAddress() {
        return mAddress;
    }
}
