using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AssetItemsSaver : StateSaver
{
    [Inject] private ItemsPool itemsPool;

    [SerializeField] private List<AssetItem> startAsset;
    private List<string> AssetItemNames;

    public List<AssetItem> GetItems()
    {
        return AssetItemNames.Select(name => itemsPool.GetAssetItemByName(name)).ToList();
    }

    protected override string GetSpecificName()
    {
        return base.GetSpecificName() + "_assetItems";
    }

    protected override void FirstRun()
    {
        base.FirstRun();
        ConvertAssetItemsToNames(startAsset);
    }

    public void ConvertAssetItemsToNames(List<AssetItem> assetItems)
    {
        AssetItemNames = assetItems.Select(item => item.Name).ToList();
    }

    protected override string ToJson()
    {
        // сохранить все переменные
        return JsonUtility.ToJson(new AssetItemsState()
        {
            AssetItemNames = this.AssetItemNames
        });
    }

    protected override void Apply(string serializeObject)
    {
        AssetItemsState state = JsonUtility.FromJson<AssetItemsState>(serializeObject);
        // применить все сохраненные переменные
        this.AssetItemNames = state.AssetItemNames;
    }
}

public struct AssetItemsState
{
    public List<string> AssetItemNames;
}
