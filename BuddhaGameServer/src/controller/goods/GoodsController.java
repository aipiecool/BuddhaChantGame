package controller.goods;

import controller.Controller;
import controller.goods.requests.GoodsRequest;

public class GoodsController extends Controller {

    public GoodsController() {
        super();
        mPackageListener.subscribePackages(new GoodsRequest());
    }
}
