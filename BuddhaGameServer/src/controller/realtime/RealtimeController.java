package controller.realtime;

import controller.Controller;
import controller.realtime.actions.client.PlayerChant;
import controller.realtime.actions.client.PlayerMove;
import controller.realtime.actions.task.ChantPostTask;
import controller.realtime.actions.task.PositionPostTask;

public class RealtimeController extends Controller {

    public RealtimeController() {
        super();
        mPackageListener.subscribePackages(new PlayerMove());
        mPackageListener.subscribePackages(new PlayerChant());
        new Thread(new PositionPostTask()).start();
        new Thread(new ChantPostTask()).start();


    }


}
