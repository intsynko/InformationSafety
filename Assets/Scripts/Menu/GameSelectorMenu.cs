using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSelectorMenu : MonoBehaviour
{
    [Inject] private MySceneController _mySceneController;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject settings;
    [Inject] private MessageBox _messageBox;
    [Inject] private JoysticController _joysticController;
    [Inject] private Move move;
    [Inject] private SaveManager saveManager;

    public void Settings()
    {
        settings.SetActive(true);
    }

    public void Save()
    {
        _messageBox.SaveAnim();
        saveManager.Save();
    }

    public void OpenMenu()
    {
        menuPanel.SetActive(true);
        button.SetActive(false);
        if (_joysticController)
            _joysticController.visible = false;
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        button.SetActive(true);
        _joysticController.visible = true;
    }


    public async void Exit()
    {
        if (await _messageBox.Question("Вы уверены что хотите выйти в главное меню?"))
            _mySceneController.LoadScene("MainMenu", move.transform.position);
    }
}
