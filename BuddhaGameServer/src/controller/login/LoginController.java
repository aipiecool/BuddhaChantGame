package controller.login;

import controller.Controller;
import controller.login.actions.Heartbeat;
import controller.login.requests.*;

public class LoginController extends Controller {

    public LoginController() {
        super();
        mPackageListener.subscribePackages(new Heartbeat());
        mPackageListener.subscribePackages(new LoginRequest());
        mPackageListener.subscribePackages(new CharacterInfoRequest());
        mPackageListener.subscribePackages(new CreateCharacterRequest());
        mPackageListener.subscribePackages(new PlayerInfoRequest());
        mPackageListener.subscribePackages(new LevelRequest());
    }
}
