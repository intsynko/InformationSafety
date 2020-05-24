using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public enum EntityType { Wolf, Doctor};

public class DataToSave
{
    public DateTime lastSession; // дата последней сессии
    public DateTime gameStarted; // дата игра начата
    public DateTime dateTimeInGame;
    public string lastSceneName;
    public Vector3 lastPostion;
    public List<ScenePosition> scenePositions;
    // предметы игрока
    public List<string> playerAssetItemsNames;
    public List<int> playerAssetItemsCount;



    /// <summary>
    /// Создание нового файла статистики
    /// </summary>
    public DataToSave()
    {
        // default values
        Debug.Log("DischargeStatic");
        lastSession = DateTime.Now;
        gameStarted = DateTime.Now;
        dateTimeInGame = DateTime.Now;
        lastSceneName = SceneManager.GetActiveScene().name;
        scenePositions = new List<ScenePosition>() {
        new ScenePosition(){ SceneName="MainMenu" },
        new ScenePosition(){ SceneName="FirstScene",    spawnPosition = new Vector3(0, 4) },
        new ScenePosition(){ SceneName="Room",          spawnPosition = new Vector3(-8, 2) },
        new ScenePosition(){ SceneName="MainScene",     spawnPosition = new Vector3(21, 6) },
        new ScenePosition(){ SceneName="Room0",         spawnPosition = new Vector3(0, 7) },

        new ScenePosition(){ SceneName="Room1",         spawnPosition = new Vector3(-6, 6) },
        new ScenePosition(){ SceneName="Room2",         spawnPosition = new Vector3(-6, 7) },
        new ScenePosition(){ SceneName="Room3",         spawnPosition = new Vector3(-6, 6) }
        };
        AssetItemsState playerAssetItems = new AssetItemsState(new List<Tuple<string, int>>());
        playerAssetItemsNames = playerAssetItems.AssetItemNames;
        playerAssetItemsCount = playerAssetItems.AssetItemsCount;
    }


    /// <summary>
    /// Загрузить предметы игрока
    /// </summary>
    /// <returns></returns>
    public List<AssetItems> GetPlayerItems(ItemsPool itemsPool)
    {
        return new AssetItemsState(this.playerAssetItemsNames, this.playerAssetItemsCount).NamesAndCountToList()
            .Select(item => new AssetItems()
            {
                ItemType = itemsPool.GetAssetItemByName(item.Item1),
                Count = item.Item2
            }).ToList();
    }

    /// <summary>
    /// Сохранть предметы игрока
    /// </summary>
    /// <param name="assetItem"></param>
    public void SetPlayerItems(List<AssetItems> assetItem)
    {
        AssetItemsState a = new AssetItemsState(assetItem.Select(
            item => new Tuple<string, int>(item.ItemType.Name, item.Count)
        ).ToList());
        this.playerAssetItemsNames = a.AssetItemNames;
        this.playerAssetItemsCount = a.AssetItemsCount;
    }
}
