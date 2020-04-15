using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoxInvertory : BaseInvertory
{
    [Inject] private SaveManager saveManager;
    [SerializeField] protected MultipleInvertoryItemPresenter presenter;
    [SerializeField] private Transform additionalContainer;


    /// <summary>
    /// Отрисовать оба контейнера
    /// </summary>
    /// <param name="assetItems">То, что лежит в ящике</param>
    public override void Render(List<AssetItem> assetItems)
    {
        base.Render(assetItems); // рендерим коробку со всеми вещами коробки
        // в доп контейнере рендерим то, что есть и игрока
        RenderContaier(GetPlayerItems(), additionalContainer); 
    }

    public override void Initialisation(AbstractInvertorItemPresenter obj, IItem item)
    {
        var itemPresenter = (MultipleInvertoryItemPresenter)obj;
        itemPresenter.Init(transform.parent, baseContainer,  additionalContainer);
        itemPresenter.Render(item);
    }

    public override AbstractInvertorItemPresenter GetPresenter()
    {
        return presenter;
    }
    

    /// <summary>
    /// Собрать остатки в коробке
    /// </summary>
    /// <returns></returns>
    public List<AssetItem> CollectBoxRest()
    {
        return CollectRest(baseContainer);
    }
    /// <summary>
    /// Собрать остатки у игрока
    /// </summary>
    /// <returns></returns>
    public List<AssetItem> CollectPlayerRest()
    {
        return CollectRest(additionalContainer);
    }
    /// <summary>
    /// Собрать остатки в контейнере conrainer
    /// </summary>
    /// <param name="conrainer">контейнер для сбора остатков</param>
    /// <returns></returns>
    private List<AssetItem> CollectRest(Transform conrainer)
    {
        List<AssetItem> assetItems = new List<AssetItem>();
        for (int i = 0; i < conrainer.childCount; i++)
        {
            assetItems.Add((AssetItem)conrainer.GetChild(i).GetComponent<MultipleInvertoryItemPresenter>().Item);
        }
        return assetItems;
    }
}
