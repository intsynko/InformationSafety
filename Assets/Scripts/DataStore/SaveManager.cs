using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SaveManager
{
    
    public DataToSave dataToSave;
    public StaticSettings staticSettings;

    private JSONSaver _JSONSaver;

    private string playerDataFileName = "player_data";
    private string staticSettingsFileName = "static_settings";
    private string objStateFolderName = "objectsState";
    private string scenePostionsFolderName = "scenePositions";
    private bool started = false;

    public SaveManager(JSONSaver _JSONSaver)
    {
        this._JSONSaver = _JSONSaver;
        InitialData();
    }

    /// <summary>
    /// Загрузить сохраненную игру, если имеются сохранения
    /// </summary>
    private void InitialData()
    {
        // инициировать могут сразу несколько сущностей, 
        // но иницировать можно только один раз 
        if (!started)
        {
            started = true;
            if (dataToSave == null)
            {
                // если есть сохранения
                // (очень поможет при тестировании, если запускать не с со сцены меню)
                if (_JSONSaver.FileExists(playerDataFileName))
                    // загрузи их
                    LoadPlayerProgress();
                else Discharge();
            }
        }
    }

    public bool IsPlayerStatisticExists()
    {
        return _JSONSaver.FileExists(playerDataFileName);
    }

    /// <summary>
    /// Загрузить настройки
    /// </summary>
    public void LoadSettings()
    {
        if (_JSONSaver.FileExists(staticSettingsFileName))
            staticSettings = _JSONSaver.LoadFile<StaticSettings>(staticSettingsFileName);
        else
            staticSettings = new StaticSettings();
    }

    /// <summary>
    /// Сохранить настройки
    /// </summary>
    public void SaveSettings()
    {
        _JSONSaver.SaveFile<StaticSettings>(staticSettings, staticSettingsFileName);
    }
    
    /// <summary>
    /// Сохранить игровой процесс
    /// </summary>
    public void SavePlayerProgress()
    {
        _JSONSaver.SaveFile<DataToSave>(dataToSave, playerDataFileName);
        SaveScenePositions();
    }
    
    /// <summary>
    /// Загрузить все сохранения
    /// </summary>
    public void LoadPlayerProgress()
    {
        dataToSave = _JSONSaver.LoadFile<DataToSave>(playerDataFileName);
        LoadScenePositions();
    }
    /// <summary>
    /// Сбросить все достижения
    /// </summary>
    public void Discharge()
    {
        dataToSave = new DataToSave();
        //SaveStatic();
        string objectsStateFolder = $"{_JSONSaver.GetMainFolder()}/{objStateFolderName}";
        // удаляем все сохраненные объекты
        if (Directory.Exists(objectsStateFolder))
            Directory.Delete(objectsStateFolder, true);

        string scenePositionsFolder = $"{_JSONSaver.GetMainFolder()}/{scenePostionsFolderName}";
        // удаляем все сохраненные позиции на сценах
        if (Directory.Exists(scenePositionsFolder))
            Directory.Delete(scenePositionsFolder, true);
    }
    /// <summary>
    /// Сохранить позиции на сценах
    /// </summary>
    private void SaveScenePositions()
    {
        string scenePositionsFolder = $"{_JSONSaver.GetMainFolder()}/{scenePostionsFolderName}";
        if (!Directory.Exists(scenePositionsFolder))
            Directory.CreateDirectory(scenePositionsFolder);
        foreach (ScenePosition scenePosition in dataToSave.scenePositions)
        {
            string fullPath = $"{scenePositionsFolder}/{scenePosition.SceneName}.txt";
            _JSONSaver.WriteToFileString(fullPath, JsonUtility.ToJson(scenePosition));
        }
    }
    /// <summary>
    /// Загрузить позиции на сценах
    /// </summary>
    private void LoadScenePositions()
    {
        dataToSave.scenePositions = new List<ScenePosition>();
        string scenePositionsFolder = $"{_JSONSaver.GetMainFolder()}/{scenePostionsFolderName}";
        if (!Directory.Exists(scenePositionsFolder))
            return;

        foreach (string sceneName in Directory.EnumerateFiles(scenePositionsFolder))
        {
            string fullPath = $"{scenePositionsFolder}/{sceneName}";
            dataToSave.scenePositions.Add(JsonUtility.FromJson<ScenePosition>(_JSONSaver.ReadFromFile(sceneName)));
        }
        
    }
    /// <summary>
    /// Сохранить состояние объекта
    /// </summary>
    /// <param name="serializeObject"></param>
    /// <param name="saveName"></param>
    public void SaveObject(string serializeObject, string saveName)
    {
        Debug.Log("сохраняю объект");
        // проверяем наличие папки состояний
        string objectsStateFolder = $"{_JSONSaver.GetMainFolder()}/{objStateFolderName}";
        if (!Directory.Exists(objectsStateFolder))
            Directory.CreateDirectory(objectsStateFolder);
        // проверяем наличие папки сцены
        string thisObjectStateFolder = $"{objectsStateFolder}/{SceneManager.GetActiveScene().name}";
        if (!Directory.Exists(thisObjectStateFolder))
            Directory.CreateDirectory(thisObjectStateFolder);
        // сохраняем
        string fullPath = $"{thisObjectStateFolder}/{saveName}.txt";
        Debug.Log(fullPath);
        _JSONSaver.WriteToFileString(fullPath, serializeObject);
       
    }
    /// <summary>
    /// Получить путь до сохраненного состояния объекта
    /// </summary>
    /// <param name="saveName"></param>
    /// <returns></returns>
    public string GetObjectPath(string saveName)
    {
        string objectsStateFolder = $"{_JSONSaver.GetMainFolder()}/{objStateFolderName}";
        string thisObjectStateFolder = $"{objectsStateFolder}/{SceneManager.GetActiveScene().name}";
        return $"{thisObjectStateFolder}/{saveName}.txt";
    }
    /// <summary>
    /// Существует ли сохранения для данного объекта
    /// </summary>
    /// <param name="saveName"></param>
    /// <returns></returns>
    public bool IsObjectExists(string saveName)
    {
        return File.Exists(GetObjectPath(saveName));
    }
    /// <summary>
    /// Загрузить сохраненный объект
    /// </summary>
    /// <param name="saveName"></param>
    /// <returns></returns>
    public string LoadObject(string saveName)
    {
        return _JSONSaver.ReadFromFile(GetObjectPath(saveName));
    }
}