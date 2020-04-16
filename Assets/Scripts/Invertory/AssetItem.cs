using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Invertory", menuName = "Invertory/AssetItem")] 
public class AssetItem : ScriptableObject, IItem
{
    public string Name => name;
    public Sprite Sprite => sprite;

    [SerializeField] private string name;
    [SerializeField] private Sprite sprite;
}
