using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionJSON : MonoBehaviour
{
    public string question { get; set; }
    public string correctAnswer { get; set; }
    public List<string> incorrectAnswers { get; set; }
    public List<string> results { get; set; }

}
