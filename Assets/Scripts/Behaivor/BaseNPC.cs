using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BaseNPC : MonoBehaviour
{
    public MyEvent OnDialogEnd;
    [SerializeField] private string Name;
    [SerializeField] private Dialog dialog;
    [SerializeField] private Sprite HeadPic;
    [Inject] private MyDialogController dialogController;

    public async void StartDialog()
    {
        await dialogController.StartDialog(dialog, Name, HeadPic);
        this.OnDialogEnd.Invoke();
    }
}
