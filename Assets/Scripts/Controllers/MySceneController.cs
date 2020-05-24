using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using Zenject;
using System.Threading.Tasks;

public class MySceneController
{
    [Inject] private SaveManager _saveManager;
    [Inject] private DiContainer _container;

    public MySceneController(SaveManager saveManager)
    {
        this._saveManager = saveManager;
    }

    public async Task LoadFirstScene(Vector3 player)
    {
        await LoadSceneAsyns("Room0", player);
        await CutSceneController.RunCutsceneStatic("CutScene1");
    }
    public void LoadMainMenu(Vector3 player)
    {
        LoadScene("MainMenu", player);
    }

    public async Task LoadSceneAsyns(string sceneName, Vector3 player)
    {
        SaveLastScenePosition(player);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        _saveManager.SavePlayerProgress();
        while (asyncOperation.isDone)
            await Task.Yield();
    }

    public async void LoadScene(string sceneName, Vector3 player)
    {
        SaveLastScenePosition(player);
        SceneManager.LoadScene(sceneName);
        _saveManager.SavePlayerProgress();
    }

    public void TeleportMeIfIMust(GameObject player)
    {
        ScenePosition scenePosition;
        try
        {
            scenePosition = GetScenePositionByName(SceneManager.GetActiveScene().name);
        }
        catch (ArgumentOutOfRangeException) {
            Debug.LogWarning("Для этой сцены нет заполненной");
            return;
        }
        
        if (SceneManager.GetActiveScene().name != "MainMenu")
            _saveManager.dataToSave.lastSceneName = scenePosition.SceneName;
        if (scenePosition.SpecificPosition != Vector3.zero)
        {
            Debug.Log("Имеется специальная");
            player.transform.position = scenePosition.SpecificPosition;
        }
        else if (scenePosition.LastPostion != Vector3.zero)
        {
            Debug.Log("Имеется последняя позиция");
            player.transform.position = scenePosition.LastPostion;
        }
        else if (scenePosition.spawnPosition != Vector3.zero)
        {
            Debug.Log("Имеется позиция спавна");
            player.transform.position = scenePosition.spawnPosition;
        }
        else
            Debug.LogError("Не найдена ни одна позиция для телепорта.");
    }

    private ScenePosition GetScenePositionByName(string name)
    {
        return _saveManager.dataToSave.scenePositions.Where(
            x => x.SceneName == SceneManager.GetActiveScene().name
        ).ToList()[0];
    }

    private void SaveLastScenePosition(Vector3 playerPosition)
    { 
        if (SceneManager.GetActiveScene().name != "MainMenu")
            GetScenePositionByName(SceneManager.GetActiveScene().name).LastPostion = playerPosition;
    }
}

public class ScenePosition
{
    public string SceneName;
    public Vector3 LastPostion;
    public Vector3 spawnPosition;
    public Vector3 SpecificPosition;
}
