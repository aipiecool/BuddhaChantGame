package network.socket.actions;

import network.socket.SocketAddress;
import utils.BytesUtils;
import utils.JsonUtils;

import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;

public abstract class ServerJsonResponsePackage extends ServerResponsePackage{

    Map<String, String> mValues = new HashMap<String, String>();

    public ServerJsonResponsePackage(SocketAddress address, int packageId, int code, String message) throws UnsupportedEncodingException {
        super(address, packageId, code, message);
    }

    public void addKeyValue(String key, String value)
    {
        mValues.put(key, value);
    }

    @Override
    protected byte[] getBody() throws UnsupportedEncodingException {
        String json = JsonUtils.serialize(mValues);
        byte[] body = BytesUtils.string2Bytes(json);
        return body;
    }
}
