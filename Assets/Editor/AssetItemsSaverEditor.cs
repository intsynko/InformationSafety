using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

//[CustomEditor(typeof(AssetItemsSaver))]
public class AssetItemsSaverEditor : Editor
{
    private AssetItemsSaver saver;

    //private void OnEnable()
    //{
    //    saver = (AssetItemsSaver)target;
    //}

    //public override void OnInspectorGUI()
    //{
    //    if (saver.startAsset.Count > 0)
    //    {
    //        foreach (AssetItems items in saver.startAsset)
    //        {
    //            EditorGUILayout.BeginVertical("box");
    //            EditorGUILayout.BeginHorizontal();
    //            if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
    //            {
    //                saver.startAsset.Remove(items);
    //                break;
    //            }
    //            EditorGUILayout.EndHorizontal();
    //            items.ItemType = (AssetItem)EditorGUILayout.ObjectField("Предмет", items.ItemType, typeof(AssetItem), false);
    //            items.Count = EditorGUILayout.IntField("Количество", items.Count);
    //            EditorGUILayout.EndVertical();
    //        }
    //    }
    //    else EditorGUILayout.LabelField("Нет предметов");
    //    if (GUILayout.Button("Добавить предмет", GUILayout.Height(30))) saver.startAsset.Add(new AssetItems());
    //    if (GUI.changed) SetObjectDirty(saver.gameObject);
    //}

    //public static void SetObjectDirty(GameObject obj)
    //{
    //    EditorUtility.SetDirty(obj);
    //    EditorSceneManager.MarkSceneDirty(obj.scene);
    //}
}
