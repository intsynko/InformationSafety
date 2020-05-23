using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Invertory", menuName = "Invertory/AssetItemWithIbfo")]
public class AssetItemWithInfo : AssetItem
{
    public string Content;
    public string Description;
}
