using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MyDialogController : MonoBehaviour
{
    [SerializeField] private Color DefaulAnswereColor;
    [SerializeField] private GameObject Questoin;
    [SerializeField] private Image Head;
    [SerializeField] private Text CharacterName;
    [SerializeField] private Text QuestionTitle;
    [SerializeField] private GameObject[] AnswersButtons;

    [Inject] private JoysticController _joysticController;
    [Inject] private MessageBox messageBox;
    private bool haveAnswer;
    private int answereNum;
    private int curQuestionPointer = 0;
    

    public async Task<int> StartDialog(Dialog dialog, string characterName, Sprite head)
    {
        _joysticController.visible = false; // выключаем джостик
        Questoin.SetActive(true); // включаем вопросы
        CharacterName.text = characterName; // ставим имя персонажу
        Head.sprite = head; // картинку персонажа
        curQuestionPointer = 0; // сбрасываем указатель текущего вопроса на 0
        int points = 0; // считаем очки

        while (true)
        {
            if (curQuestionPointer >= dialog.QusetionSet.Length){
                await messageBox.Message("Сбой диалогой системы. Ссылка на несуществующий диалог.");
                break;
            }
            Question currentQuestion = dialog.QusetionSet[curQuestionPointer];
            QuestionTitle.text = currentQuestion.NPCQuestion;
            // цикл по всем доступным кнопкам
            for(int i=0; i < AnswersButtons.Length; i++)
            {
                // если вопроса с таким индексом нет
                if (i >= currentQuestion.Answers.Length)
                    AnswersButtons[i].SetActive(false);
                else{
                    // если не хватает баллов для разблокировки
                    if (points < currentQuestion.Answers[i].PointsToUnlock)
                        AnswersButtons[i].SetActive(false);
                    else {
                        // включаем кнопку
                        AnswersButtons[i].SetActive(true);
                        // ставим текст вопроса
                        AnswersButtons[i].transform.Find("QuestionTitle").GetComponent<Text>().text = currentQuestion.Answers[i].Text;
                        // если вопрос уже использовался
                        if (currentQuestion.Answers[i].AlredyUsed)
                            // ставим черный цвет ответа 
                            AnswersButtons[i].transform.Find("QuestionTitle").GetComponent<Text>().color = new Color(0, 0, 0, 1);
                        else
                            AnswersButtons[i].transform.Find("QuestionTitle").GetComponent<Text>().color = DefaulAnswereColor;
                    }

                }
            }
            // ждем ответа
            haveAnswer = false;
            while (!haveAnswer) await Task.Yield();
            haveAnswer = false; // защита от краша на Task.Yield();
            // проставляем, что ответ уже использовался
            Answer curAnswere = currentQuestion.Answers[answereNum];
            curAnswere.AlredyUsed = true;
            points += curAnswere.PointsForQuestion; // прибавляем баллы за ответ
            if (curAnswere.NextQuestionNumber == -1) break; // -1 - прерывание диалога
            curQuestionPointer = curAnswere.NextQuestionNumber;
        }
        // возвращаем управление
        _joysticController.visible = true;
        Questoin.SetActive(false);
        return points;
    }

    /// <summary>
    /// Метод, вызываемый кнопками, который проставляет ответ
    /// </summary>
    /// <param name="num"></param>
    public void Answer(int num)
    {
        haveAnswer = true;
        answereNum = num;
    }
}
