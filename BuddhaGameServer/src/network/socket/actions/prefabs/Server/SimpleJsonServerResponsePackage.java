package network.socket.actions.prefabs.Server;

import network.socket.actions.ServerResponsePackage;
import network.socket.SocketAddress;
import utils.JsonUtils;

import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;

public class SimpleJsonServerResponsePackage extends ServerResponsePackage {

    protected final String mHeaderString;
    protected Map<String, String> mValue = new HashMap<>();

    public SimpleJsonServerResponsePackage(SocketAddress address, int packageId, String headerString, int code) throws UnsupportedEncodingException {
        super(address, packageId);
        mHeaderString = headerString;
        setCode(code);
    }

    public void addKeyValue(String key, String value){
        mValue.put(key, value);
    }

    public void complete(){
        String json = JsonUtils.serialize(mValue);
        setMessage(json);
    }

    @Override
    public String getHeaderString() {
        return mHeaderString;
    }
}
