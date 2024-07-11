using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question" )]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter new question text here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;
    public string GetQuestion()
    {
        return question;
    }
    public string GetAnswer(int index)
    {
        return answers[index];
    }
    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
    public void Init(string question, string[] answers, int correctAnswerIndex)
    {
        this.question = question;
        this.answers = answers;
        this.correctAnswerIndex = correctAnswerIndex;
    }
    public static QuestionSO CreateInstance(string question, string[] answers, int correctAnswerIndex)
    {
        var data = ScriptableObject.CreateInstance<QuestionSO>();
        data.Init(question, answers, correctAnswerIndex);
        return data;
    }
}
