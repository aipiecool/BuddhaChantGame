package network.socket;

import java.util.Objects;

public class SocketAddress {
    private String mIpAddress;
    private int mPort;

    public SocketAddress(String mIpAddress, int mPort) {
        this.mIpAddress = mIpAddress;
        this.mPort = mPort;
    }

    public String getIpAddress() {
        return mIpAddress;
    }

    public int getPort() {
        return mPort;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        SocketAddress that = (SocketAddress) o;
        return mPort == that.mPort &&
                Objects.equals(mIpAddress, that.mIpAddress);
    }

    @Override
    public int hashCode() {
        return Objects.hash(mIpAddress, mPort);
    }

    @Override
    public String toString() {
        return mIpAddress + ":" + mPort;
    }
}
