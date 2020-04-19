using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEditor;
using Zenject;

public class JSONSaver
{
    private string prefix = ".txt";

    /// <summary>
    /// Сохранить ститистику
    /// </summary>
    public void SaveFile<T>(T dataToSave, string relativePath)
    {
        string data = JsonUtility.ToJson(dataToSave);
        Debug.Log("Save: " + data);
        string path = $"{GetMainFolder()}/{relativePath}.txt";
        WriteToFileString(path, data);
    }
    /// <summary>
    /// Загрузить файл
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T LoadFile<T>(string relativePath)
    {
        string path = GetFilePath(relativePath);
        string data = ReadFromFile(path);
        Debug.Log("Load: " + data);
        return JsonUtility.FromJson<T>(data);
    }

    /// <summary>
    /// Записать в файл (путь, данные) 
    /// </summary>
    /// <param name="fullPath"></param>
    /// <param name="data"></param>
    public void WriteToFileString(string fullPath, string data)
    {
        //string path =  GetFilePath(fileName);
        FileStream fileStream = new FileStream(fullPath, FileMode.Create);
        using (StreamWriter streamWriter = new StreamWriter(fileStream))
        {
            streamWriter.Write(data);
        }

    }
    /// <summary>
    /// Прочитать файл
    /// </summary>
    /// <param name="fullPath"></param>
    /// <returns></returns>
    public string ReadFromFile(string fullPath)
    {
        //string path = GetFilePath(fileName);
        Debug.Log(fullPath);
        if (File.Exists(fullPath))
        {
            using (StreamReader reader = new StreamReader(fullPath))
            {
                return reader.ReadToEnd();
            }
        }
        Debug.Log("file not found");
        throw new System.IO.FileNotFoundException();
    }
    /// <summary>
    /// Получить абсолютный путь файла
    /// </summary>
    /// <param name="fileRelativePath">Относительный путь файла</param>
    /// <returns></returns>
    public string GetFilePath(string fileRelativePath)
    {
        return GetMainFolder() + "/" + fileRelativePath + prefix;
    }
    /// <summary>
    /// Получить расположение корневой директории
    /// </summary>
    /// <returns></returns>
    public string GetMainFolder()
    {
        return Application.persistentDataPath;
    }

    /// <summary>
    /// Существует ли файл
    /// </summary>
    /// <param name="fileRelativePath">Отностиельный путь к файлу</param>
    /// <returns></returns>
    public bool FileExists(string fileRelativePath)
    {
        return File.Exists(GetFilePath(fileRelativePath));
    }
}

