using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Chest : MonoBehaviour
{
    public string MySpecificName;
    [Inject] private SaveManager saveManager;
    public List<AssetItem> AssetItems;
    public MyEvent OnOpen;

    private GameObject cloud;
    [Inject] private InventorySelectorMenu inventorySelectorMenu;

    private void Start()
    {
        cloud = transform.Find("Cloud").gameObject;
        //cloud.OnClick += Cloud_OnClick;
    }

    public  async void Cloud_OnClick()
    {
        cloud.SetActive(false);
        OnOpen.Invoke();
        AssetItems = await inventorySelectorMenu.OpenBox(AssetItems);
    }

    private void OnDestroy()
    {
        saveManager.SaveObject(JsonUtility.ToJson(AssetItems), "MySpecificName");
    }
}
