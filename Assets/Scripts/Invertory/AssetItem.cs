using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Invertory", menuName = "Invertory/AssetItem")] 
public class AssetItem : ScriptableObject, IItem
{
    public string Name => name;
    public string SpriteName => spriteName;

    [SerializeField] private string name;
    [SerializeField] private string spriteName;
}
