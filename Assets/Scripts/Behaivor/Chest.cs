using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Chest : MonoBehaviour
{
    [Inject] private SaveManager saveManager;
    [Inject] private DialogueSystemEvents dialogueSystemEvents;
    public AssetItemsSaver AssetItemsSaver;
    public MyEvent OnOpen;
    
    [Inject] private InventorySelectorMenu inventorySelectorMenu;

    public void Open()
    {
        GetComponent<DialogueSystemTrigger>().OnUse();
        dialogueSystemEvents.conversationEvents
            .onConversationEnd.AddListener(open);
        
    }

    private async void open(Transform a)
    {
        dialogueSystemEvents.conversationEvents
            .onConversationEnd.RemoveListener(open);
        if (!DialogueLua.GetVariable("IsOpen").AsBool)
            return;
        OnOpen.Invoke();
        //AssetItemsSaver.Load(); // загружаем сохраненные изменения
        // показываем меню инвенторя
        AssetItemsSaver.ConvertAssetItemsToNames(await inventorySelectorMenu.OpenBox(AssetItemsSaver.GetItems()));
        // сохраняем остатки в сундуке
        AssetItemsSaver.Save();
    }
}
