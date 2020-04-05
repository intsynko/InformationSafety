using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Invertory", menuName = "Invertory/SpritePool")]
public class SpritePool : ScriptableObject
{
    public List<SpriteItem> SpriteItems;

    public Sprite GetSpriteByName(string name)
    {
        return SpriteItems.Where(x => x.SpriteName == name).ToList()[0].Sprite;
    }
}
