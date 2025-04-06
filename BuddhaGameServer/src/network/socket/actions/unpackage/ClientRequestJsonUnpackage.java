package network.socket.actions.unpackage;

import network.socket.SocketAddress;
import utils.BytesUtils;
import utils.JsonUtils;
import com.google.gson.reflect.TypeToken;

import java.util.Map;

public class ClientRequestJsonUnpackage implements Unpackageable {

    protected int mHeader;
    protected byte[] mBody;
    protected int mPackageId;
    Map<String, String> mValues;
    protected SocketAddress mAddress;

    public ClientRequestJsonUnpackage(ClientRequestUnpackage clientRequestUnpackage) throws Exception {
        mHeader = clientRequestUnpackage.getHeader();
        mBody = clientRequestUnpackage.getBody();
        mAddress = clientRequestUnpackage.getAddress();
        mPackageId = clientRequestUnpackage.getPackageId();
        mValues = JsonUtils.unserialize(BytesUtils.bytes2String(mBody), new TypeToken<Map<String, String>>() {}.getType());
    }

    public Map<String, String> getValues(){
        return mValues;
    }

    public String getValue(String key){
        return mValues.get(key);
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

    @Override
    public String toString() {
        return "ClientRequestJsonUnpackage(" + mValues.toString() + ")";
    }
}
