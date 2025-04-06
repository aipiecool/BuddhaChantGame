package network.socket.actions;

import network.socket.SocketAddress;
import utils.BytesUtils;

import java.io.UnsupportedEncodingException;

public abstract class ServerResponsePackage extends ServerPackage {

    protected int mPackageId;
    protected int mCode;
    protected String mMessage;
    private int mLifetime;

    public ServerResponsePackage(SocketAddress address, int packageId, int code, String message) throws UnsupportedEncodingException {
        super(address);
        setCode(code);
        setMessage(message);
        setPackageId(packageId);
    }

    public ServerResponsePackage(SocketAddress address, int packageId) throws UnsupportedEncodingException {
        super(address);
        setPackageId(packageId);
    }

    public void setCode(int code){
        mCode = code;
    }

    public void setMessage(String message){
        mMessage = message;
    }

    public void setPackageId(int id){
        mPackageId = id;
    }

    public int getPackageId() {
        return mPackageId;
    }

    @Override
    protected byte[] getBody() throws UnsupportedEncodingException {
        return BytesUtils.bytesConcat(BytesUtils.int2Bytes(mCode), BytesUtils.string2Bytes(mMessage));
    }

    @Override
    public byte[] getBytes() throws UnsupportedEncodingException {
        byte[] header = getHeader();
        byte[] body = getBody();
        return BytesUtils.bytesConcat(header, BytesUtils.bytesConcat(BytesUtils.int2Bytes(mPackageId), body));
    }

    @Override
    public String toString() {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(getHeaderString());
        stringBuilder.append("(addr:");
        stringBuilder.append(mAddress.toString());
        stringBuilder.append(",packageId:");
        stringBuilder.append(mPackageId);
        stringBuilder.append(",code:");
        stringBuilder.append(mCode);
        stringBuilder.append(",message:");
        stringBuilder.append(mMessage);
        stringBuilder.append(")");
        return stringBuilder.toString();
    }

    @Override
    public boolean equals(Object obj) {
        if(obj instanceof ServerResponsePackage){
            ServerResponsePackage other = (ServerResponsePackage) obj;
            return other.mPackageId == mPackageId && other.getAddress().equals(mAddress);
        }
        return false;
    }

    public void setLifeTime(int lifeTime){
        mLifetime = lifeTime;
    }

    public int getLifetime() {
        return mLifetime;
    }
}
