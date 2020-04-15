using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Invertory", menuName = "Invertory/ItemsPool")]
public class ItemsPool : ScriptableObject
{
    public List<AssetItem> AssetItems;

    public AssetItem GetAssetItemByName(string name)
    {
        return AssetItems.Where(x => x.Name == name).ToList()[0];
    }
}
