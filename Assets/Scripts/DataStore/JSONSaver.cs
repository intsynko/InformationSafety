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
    /// <param name="fileName"></param>
    /// <returns></returns>
    public string GetFilePath(string fileName)
    {
        return GetMainFolder() + "/" + fileName + prefix;
    }
    /// <summary>
    /// Получить расположение корневой директории
    /// </summary>
    /// <returns></returns>
    public string GetMainFolder()
    {
        return Application.persistentDataPath;
    }

    public bool FileExists(string fileName)
    {
        return File.Exists(GetFilePath(fileName));
    }
}

