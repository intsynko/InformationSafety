using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
{
    [SerializeField] private SpritePool inventorySpritePool;

    public override void InstallBindings()
    {
        Container.BindInstance(inventorySpritePool);
    }
}