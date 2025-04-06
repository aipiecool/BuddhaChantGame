package controller.npc;

import controller.Controller;
import controller.npc.request.NpcSpeakRequest;
import controller.npc.request.SceneNpcRequest;

public class NpcController extends Controller {

    public NpcController() {
        super();
        mPackageListener.subscribePackages(new SceneNpcRequest());
        mPackageListener.subscribePackages(new NpcSpeakRequest());
    }
}
