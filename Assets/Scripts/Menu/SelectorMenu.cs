using UnityEditor;
using UnityEngine;
using Zenject;

public class SelectorMenu : MonoBehaviour
{
    [Inject] private SaveManager _saveManager;
    [Inject] private MessageBox _messageBox;
    [Inject] private MySceneController _mySceneController;

    public async void NewGame()
    {
        string message = "Осторожно, это может сбросить предыдущий игровой процесс.";
        string yes = "Продолжить";
        string no = "Отмена";
        // если есть предыдущие сохранения
        if (_saveManager.IsPlayerStatisticExists())
            // если игрок не согласен их удалить - прерываем
            if (!await _messageBox.Question(message, yes, no))
                return;
        // иначе сбрасываем все сохранения и начинаем новую игру
        try
        {
            _saveManager.Discharge();
        }
        catch (System.IO.IOException) {
            message = "Не удается удалить предыдущие сохранения. Не хватает прав.";
            await _messageBox.Message(message);
        }
        await _mySceneController.LoadFirstScene(Vector3.zero);
    }

    public void ContinueGame()
    {
        try
        {
            _saveManager.LoadPlayerProgress();
        }
        catch (System.IO.FileNotFoundException)
        {
            _messageBox.Message("Не найдено последнее сохранение.");
            return;
        }
        _mySceneController.LoadScene(_saveManager.dataToSave.lastSceneName, Vector3.zero);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
