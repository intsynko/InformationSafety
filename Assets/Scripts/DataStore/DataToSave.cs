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
        new ScenePosition(){ SceneName="Room",          spawnPosition = new Vector3(-8, 2) }
        };
        AssetItemsState playerAssetItems = new AssetItemsState(new List<Tuple<string, int>>());
        playerAssetItemsNames = playerAssetItems.AssetItemNames;
        playerAssetItemsCount = playerAssetItems.AssetItemsCount;
    }
}
