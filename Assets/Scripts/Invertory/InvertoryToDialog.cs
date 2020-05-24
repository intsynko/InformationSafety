using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InvertoryToDialog : MonoBehaviour
{
    [Inject] SaveManager saveManager;
    [Inject] private DialogueSystemEvents dialogueSystemEvents;
    [Inject] private ItemsPool itemsPool;

    public void Start()
    {
        dialogueSystemEvents.conversationEvents.
            onConversationStart.AddListener((Transform a) => { DialogStart(); });
        dialogueSystemEvents.conversationEvents.
            onConversationEnd.AddListener((Transform a) => { DialofEnd(); });
    }
    
    
    /// <summary>
    /// Загружаем в диалог предметы
    /// </summary>
    private void DialogStart()
    {
        DataToSave d2s = saveManager.dataToSave;
        // проходимся по всем предметам в пуле предметов
        for (int i=0; i < itemsPool.AssetItems.Count; i++)
        {
            string name = itemsPool.AssetItems[i].Name; // берем его имя
            int count = 0; // изначально кол-во = 0
            // если предмет присутствует в инвертаре игрока
            if (d2s.playerAssetItemsNames.Contains(name))
                // беерм его количество
                count = d2s.playerAssetItemsCount[d2s.playerAssetItemsNames.IndexOf(name)];
            // заливаем в lua
            DialogueLua.SetVariable(name, count);
        }
    }

    /// <summary>
    /// Выгружаем из диалога все предметы в инвертарь игрока
    /// </summary>
    private void DialofEnd()
    {
        List<AssetItems> result = new List<AssetItems>();
        DataToSave d2s = saveManager.dataToSave;
        // проходимся по всем предметам в пуле предметов
        for (int i = 0; i < itemsPool.AssetItems.Count; i++)
        {
            string name = itemsPool.AssetItems[i].Name;
            int count = DialogueLua.GetVariable(name).asInt;
            Debug.Log($"{name}: {count}");
            if (count > 0)
                result.Add(new AssetItems()
                {
                    ItemType = itemsPool.GetAssetItemByName(name),
                    Count = count
                });
        }
        saveManager.dataToSave.SetPlayerItems(result);
        saveManager.SavePlayerProgress();
    }
}
