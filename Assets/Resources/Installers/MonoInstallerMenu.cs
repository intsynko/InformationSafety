using Zenject;

public class MonoInstallerMenu : MonoInstaller
{
    public override void InstallBindings()
    {
        JSONSaver jSONSaver = new JSONSaver();
        Container.BindInstance<JSONSaver>(jSONSaver);

        SaveManager saveManager = new SaveManager(jSONSaver);
        Container.BindInstance<SaveManager>(saveManager);

        MySceneController mySceneController = new MySceneController(saveManager);
        Container.BindInstance<MySceneController>(mySceneController);
    }
}