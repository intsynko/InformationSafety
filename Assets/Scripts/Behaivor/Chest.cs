using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Chest : MonoBehaviour
{
    [Inject] private SaveManager saveManager;
    public AssetItemsSaver AssetItemsSaver;
    public MyEvent OnOpen;
    
    [Inject] private InventorySelectorMenu inventorySelectorMenu;

    public async void Open()
    {
        OnOpen.Invoke();
        //AssetItemsSaver.Load(); // загружаем сохраненные изменения
        // показываем меню инвенторя
        AssetItemsSaver.ConvertAssetItemsToNames(await inventorySelectorMenu.OpenBox(AssetItemsSaver.GetItems()));
        // сохраняем остатки в сундуке
        AssetItemsSaver.Save();
    }
}
