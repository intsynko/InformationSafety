using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
{
    [SerializeField] private ItemsPool inventoryItemsPool;

    public override void InstallBindings()
    {
        Container.BindInstance(inventoryItemsPool);
    }
}