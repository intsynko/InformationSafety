using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;
using System;

public abstract class BaseInvertory : MonoBehaviour
{
    /// <summary>
    /// Базовый контейнер
    /// </summary>
    [SerializeField] public Transform baseContainer;
    /// <summary>
    /// пул предметов
    /// </summary>
    [Inject] private ItemsPool itemsPool;
    /// <summary>
    /// Менеджер сохранений
    /// </summary>
    [Inject] private SaveManager saveManager;

    /// <summary>
    /// Коллеция преметов, которая лежат в контейнере
    /// </summary>
    protected List<AssetItems> baseAssetItems;


    /// <summary>
    /// Загрузить предметы игрока
    /// </summary>
    /// <returns></returns>
    public List<AssetItems> GetPlayerItems()
    {
        DataToSave d2s = saveManager.dataToSave;
        
        return new AssetItemsState(d2s.playerAssetItemsNames, d2s.playerAssetItemsCount).NamesAndCountToList()
            .Select(item => new AssetItems() {
                ItemType = itemsPool.GetAssetItemByName(item.Item1),
                Count = item.Item2
            }).ToList();
    }

    /// <summary>
    /// Сохранть предметы игрока
    /// </summary>
    /// <param name="assetItem"></param>
    public void SavePlayerData(List<AssetItems> assetItem)
    {
        AssetItemsState a = new AssetItemsState(assetItem.Select(
            item => new Tuple<string, int>(item.ItemType.Name, item.Count)
        ).ToList());
        saveManager.dataToSave.playerAssetItemsNames = a.AssetItemNames;
        saveManager.dataToSave.playerAssetItemsCount = a.AssetItemsCount;
        saveManager.SavePlayerProgress();
    }

    /// <summary>
    /// Отрисовать
    /// </summary>
    /// <param name="assetItems"></param>
    public virtual void Render(List<AssetItems> assetItems)
    {
        RenderContaier(assetItems, baseContainer); // отрисовываем элементы
        this.baseAssetItems = assetItems; // заполминаем стартовый набор
    }

    /// <summary>
    /// Отрисовать контейнер
    /// </summary>
    /// <param name="assetItems">Коллекция контейнера</param>
    /// <param name="container"></param>
    protected void RenderContaier(List<AssetItems> assetItems, Transform container)
    {
        foreach (Transform child in container)
            Destroy(child.gameObject);
        assetItems.ForEach(item =>
        {
            var obj = Instantiate(GetPresenter(), container);
            Initialisation(obj, item);
        });
    }


    /// <summary>
    /// Удалить предмет из контейнера
    /// </summary>
    /// <param name="itemPresenter">презентатор предмета</param>
    public virtual void RemoveItem(AbstractInvertorItemPresenter itemPresenter)
    {
        RemoveItem(itemPresenter, baseAssetItems);
    }

    /// <summary>
    /// Удалить предмет из контейнера
    /// </summary>
    /// <param name="itemPresenter">презентор предмета</param>
    /// <param name="assetItems">коллеция предметов контейнера</param>
    public virtual void RemoveItem(AbstractInvertorItemPresenter itemPresenter, List<AssetItems> assetItems)
    {
        // удаляем элемент из коллекции
        assetItems.Remove(itemPresenter.AssetItem);
    }

    /// <summary>
    /// Добавить предмет в коллекцию
    /// </summary>
    /// <param name="itemPresenter">презентор предмета</param>
    public virtual void AddNewItem(AbstractInvertorItemPresenter itemPresenter)
    {
        AddNewItem(itemPresenter, baseAssetItems, baseContainer);
    }

    /// <summary>
    /// Добавить новый предмет в контейнер
    /// </summary>
    /// <param name="itemPresenter">презентер предмета</param>
    /// <param name="assetItems">коллеция контейнера</param>
    /// <param name="container">позиция контейнера</param>
    public virtual void AddNewItem(AbstractInvertorItemPresenter itemPresenter, List<AssetItems> assetItems, Transform container)
    {
        // ищем, есть ли элемент с таким же именем в контейнере
        List<AssetItems> finded_items = assetItems.Where(item => item.ItemType.Name == itemPresenter.AssetItem.ItemType.Name).ToList();
        if (finded_items.Count > 0)
        {
            // если есть то увеличиваем его кол-во
            finded_items[0].Count += itemPresenter.AssetItem.Count;
            Destroy(itemPresenter.gameObject);
            RenderContaier(assetItems, container);
        }
        else
        {
            // иначе добавляем его в нашу коллецию элементов
            assetItems.Add(itemPresenter.AssetItem);
            // и перемещаем его внутрь контейнера
            itemPresenter.FindAndSetInClosestPosition(container);
        }

    }

    /// <summary>
    /// Создание инвертаря
    /// </summary>
    /// <param name="obj">презентор предмета</param>
    /// <param name="assetItems">коллеция предметов</param>
    public abstract void Initialisation(AbstractInvertorItemPresenter obj, AssetItems assetItems);

    /// <summary>
    /// Получить презентор для отрисовки контейреа
    /// </summary>
    /// <returns></returns>
    public abstract AbstractInvertorItemPresenter GetPresenter();
}
