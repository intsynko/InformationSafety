using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class MessageBox : MonoBehaviour
{
    [SerializeField] private GameObject _question;
    [SerializeField] private GameObject _message;
    [SerializeField] private GameObject _saveBar;
    [SerializeField] private GameObject _infoTable;
    [SerializeField] private GameObject _helpBox;

    private string answer = "";

    /// <summary>
    /// Спросить что то
    /// </summary>
    /// <param name="qestion">Вопрос</param>
    /// <param name="yes"></param>
    /// <param name="no"></param>
    /// <returns></returns>
    public async Task<bool> Question(string qestion, string yes = "Да", string no = "Нет")
    {
        _question.SetActive(true);
        _question.transform.Find("Text").GetComponent<Text>().text = qestion;
        _question.transform.Find("ButtonYes").Find("Text").GetComponent<Text>().text = yes;
        _question.transform.Find("ButtonNo").Find("Text").GetComponent<Text>().text = no;
        while (answer == "") { await Task.Yield(); }
        _question.SetActive(false);
        switch (answer)
        {
            case "yes": releseAnswer();  return true;
            case "no": releseAnswer();  return false;
            default: releseAnswer();  throw new System.Exception("некорректный ответ");
        }
    }
    /// <summary>
    /// Вывести сообщение
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ok"></param>
    /// <returns></returns>
    public async Task Message(string message, string ok = "Ок")
    {
        _message.SetActive(true);
        _message.transform.Find("Text").GetComponent<Text>().text = message;
        _message.transform.Find("ButtonOk").Find("Text").GetComponent<Text>().text = ok;
        while (answer == "") { await Task.Yield(); }
        releseAnswer();
        _message.SetActive(false);
    }
    /// <summary>
    /// Запустить анимацию сохранения
    /// </summary>
    public async Task SaveAnim()
    {
        _saveBar.SetActive(true);
        Animation saveBarAnim = _saveBar.GetComponent<Animation>();
        saveBarAnim.Play("UpAndDown");
        _saveBar.transform.Find("Bar").GetComponent<Animation>().Play("Rotate");
        while (saveBarAnim.isPlaying) { await Task.Yield(); }
        _saveBar.SetActive(false);
    }

    /// <summary>
    /// Запустить анимацию подсказки
    /// </summary>
    public async void HelpBox(string message)
    {
        _helpBox.SetActive(true);
        _helpBox.transform.Find("Text").GetComponent<Text>().text = message;
        Animation helpBoxAnim = _helpBox.GetComponent<Animation>();
        helpBoxAnim.Play("UpAndDown");
        while (helpBoxAnim.isPlaying) { await Task.Yield(); }
        _helpBox.SetActive(false);
    }

    public void ShowObjectInfo(string name, string description, string content, Sprite sprite)
    {
        _infoTable.SetActive(true);
        _infoTable.transform.Find("Name").GetComponent<Text>().text = name;
        _infoTable.transform.Find("Descroption").Find("DescroptionText").GetComponent<Text>().text = description;
        _infoTable.transform.Find("ImageTable").Find("Image").GetComponent<Image>().sprite = sprite;
        _infoTable.transform.Find("Content").Find("ContentText").GetComponent<Text>().text = content;
    }

    /// <summary>
    /// Сбросить ответ
    /// </summary>
    private void releseAnswer()
    {
        answer = "";
    }
    /// <summary>
    /// Метод приема ответа от кнопок
    /// </summary>
    /// <param name="answer"></param>
    public void Answer(string answer)
    {
        this.answer = answer;
    }

    
}
