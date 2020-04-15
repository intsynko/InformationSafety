using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public abstract class BaseInvertory : MonoBehaviour
{
    [SerializeField] public Transform baseContainer;
    [Inject] private ItemsPool itemsPool;
    [Inject] private SaveManager saveManager;

    public List<AssetItem> GetPlayerItems()
    {
        return saveManager.dataToSave.playerAssetItems.Select(name => itemsPool.GetAssetItemByName(name)).ToList();
    }

    public void SavePlayerData(List<AssetItem> assetItem)
    {
        saveManager.dataToSave.playerAssetItems = assetItem.Select(item => item.Name).ToList();
    }

    public virtual void Render(List<AssetItem> assetItems)
    {
        RenderContaier(assetItems, baseContainer);
    }

    protected void RenderContaier(List<AssetItem> assetItems, Transform container)
    {
        foreach (Transform child in container)
            Destroy(child.gameObject);
        assetItems.ForEach(item =>
        {
            var obj = Instantiate(GetPresenter(), container);
            Initialisation(obj, item);
        });
    }

    public abstract void Initialisation(AbstractInvertorItemPresenter obj, IItem item);

    public abstract AbstractInvertorItemPresenter GetPresenter();
}
