using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Dialog/Question")]
public class Question : ScriptableObject
{
    [Header("Test 1")]
    [Tooltip("Test 2")]
    /// <summary>
    /// Текст вопроса
    /// </summary>
    public string NPCQuestion;
    /// <summary>
    /// Варианты ответа
    /// </summary>
    public Answer[] Answers;
}
