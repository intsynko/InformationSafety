using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IListAssetItems
{
    List<AssetItem> assetItems { get; set; }
}

public class AssetItemsSaver : MonoBehaviour
{
    [SerializeField] private IListAssetItems assetItems;
}
