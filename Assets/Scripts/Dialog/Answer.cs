using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Dialog/Answer")]
public class Answer : ScriptableObject
{
    /// <summary>
    /// Варианты ответа
    /// </summary>
    public string Text;
    /// <summary>
    /// Баллы за ответ
    /// </summary>
    public int PointsForQuestion;
    /// <summary>
    /// Индекс сдежующего вопроса (поставь -1)
    /// </summary>
    public int NextQuestionNumber;
    /// <summary>
    /// Использовался ли уже этот ответ
    /// </summary>
    public bool AlredyUsed;
    /// <summary>
    /// Количество очков для разблокировки
    /// </summary>
    public int PointsToUnlock;
}
