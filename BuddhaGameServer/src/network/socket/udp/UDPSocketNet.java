package network.socket.udp;

import log.Log;
import network.socket.SocketAddress;
import network.socket.SocketNet;
import utils.BytesUtils;

import java.io.IOException;
import java.net.*;

public class UDPSocketNet extends SocketNet {

    protected Thread mReceiveThread;
    protected DatagramSocket mSocket;
    protected byte[] mReceiveBuff;

    public UDPSocketNet(int port) {
        super(port);
        try {
            mReceiveBuff = new byte[512];
            mSocket = new DatagramSocket(mPort);
            mReceiveThread = new RealReceiveThread();
            mReceiveThread.start();
            Log.input().info("UDP服务器已启动(端口号:" + mPort + ")");
        } catch (SocketException e) {
            Log.input().warn("UDP服务器启动失败");
            System.err.println(e.toString());
        }
    }

    @Override
    public void sendBytes(byte[] data, SocketAddress socketAddress)  {
        try {
            DatagramPacket packet = new DatagramPacket(data,data.length);
            InetSocketAddress address = new InetSocketAddress(socketAddress.getIpAddress(), socketAddress.getPort());
            InetAddress inetAddress = address.getAddress();
            packet.setAddress(inetAddress);
            packet.setPort(socketAddress.getPort());
            mSocket.send(packet);
        } catch (IOException e) {
            notifyError(e);
        }
    }

    @Override
    public void release() {
        mSocket.close();
        mReceiveThread.interrupt();
    }

    class RealReceiveThread extends Thread{

        @Override
        public void run() {
            while(!Thread.currentThread().isInterrupted()){
                DatagramPacket packet = new DatagramPacket(mReceiveBuff, mReceiveBuff.length);
                try {
                    mSocket.receive(packet);
                    UDPSocketNet.this.notify(new SocketAddress(packet.getAddress().getCanonicalHostName(), packet.getPort()) , BytesUtils.subbytes(packet.getData(), 0, packet.getLength()));
                } catch (Exception e) {
                    notifyError(e);
                }
            }
        }
    }
}
