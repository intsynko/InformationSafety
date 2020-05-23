using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class AssetItemsSaver : StateSaver
{
    [Inject] private ItemsPool itemsPool;

    [SerializeField] public List<AssetItems> startAsset;
    private List<Tuple<string, int>> AssetItemNames;

    public List<AssetItems> GetItems()
    {
        return AssetItemNames
            .Select(name => new AssetItems() {
                ItemType = itemsPool.GetAssetItemByName(name.Item1),
                Count = name.Item2
            }).ToList();
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

    public void ConvertAssetItemsToNames(List<AssetItems> assetItems)
    {
        AssetItemNames = assetItems.Select(item => new Tuple<string, int>(item.ItemType.Name, item.Count)).ToList();
    }

    protected override string ToJson()
    {
        
        // сохранить все переменные
        string a = JsonUtility.ToJson(new AssetItemsState(this.AssetItemNames));
        return a;
    }

    protected override void Apply(string serializeObject)
    {
        AssetItemsState state = JsonUtility.FromJson<AssetItemsState>(serializeObject);
        // применить все сохраненные переменные
        this.AssetItemNames = state.NamesAndCountToList();
    }
}

public class AssetItemsState
{
    public List<string> AssetItemNames;
    public List<int> AssetItemsCount;

    public List<Tuple<string, int>> NamesAndCountToList()
    {
        return AssetItemNames.Join(
            AssetItemsCount,
            n => AssetItemNames.IndexOf(n),
            c => AssetItemsCount.IndexOf(c),
            (n, c) => new Tuple<string, int>(n, c)
            ).ToList();
    }

    public AssetItemsState(List<Tuple<string, int>> tuples)
    {
        AssetItemNames = tuples.Select(i => i.Item1).ToList();
        AssetItemsCount = tuples.Select(i => i.Item2).ToList();
    }

    public AssetItemsState(List<string> AssetItemNames, List<int> AssetItemsCount)
    {
        this.AssetItemNames = AssetItemNames;
        this.AssetItemsCount = AssetItemsCount;
    }
}
