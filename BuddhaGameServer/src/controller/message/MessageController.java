package controller.message;

import controller.Controller;
import controller.message.actions.server.NeedRequestMessage;
import controller.message.requests.FlowTextRequest;
import event.server.ServerEventListener;
import event.server.ServerEventsMamager;
import event.server.events.impl.ClientNeedFlowTextServerEvent;
import model.player.Player;

import java.io.UnsupportedEncodingException;

public class MessageController extends Controller {

    public MessageController() {
        super();

        mPackageListener.subscribePackages(new FlowTextRequest());

        //监听屏幕浮动文字消息
        ServerEventsMamager.get().addListener(new ServerEventListener<ClientNeedFlowTextServerEvent>(
                ClientNeedFlowTextServerEvent.class
        ) {
            @Override
            protected void onReceive(ClientNeedFlowTextServerEvent event) {
                Player player = event.getPlayer();
                player.addFlowTextEvent(event);
                try {
                    mSocket.sendPackage(new NeedRequestMessage(player.getAddress(), "FlowText"));
                } catch (UnsupportedEncodingException e) {
                    e.printStackTrace();
                }
            }
        });
    }
}
