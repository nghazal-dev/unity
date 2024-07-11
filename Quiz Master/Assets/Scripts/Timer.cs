using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    float timerValue;
    [SerializeField] float timeLimit = 9f;
    [SerializeField] float timeToShowCorrectAnswer = 3f;
    public bool loadNextQuestion = false;
    public bool isAnsweringQuestion = true;
    public float fillFraction;

    void Update()
    {
        UpdateTimer();
    }

    private void Start()
    {
        timerValue = timeLimit;
    }


    public void CancelTimer()
    {
        timerValue = 0;
        isAnsweringQuestion = true;
    }


    private void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (isAnsweringQuestion)
        {

            if (timerValue <= 0)
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
            else
            {
                fillFraction = timerValue / timeLimit;
            }

        }
        else
        {
            if(timerValue <= 0)
            {
                isAnsweringQuestion = true;
                timerValue = timeLimit;
                loadNextQuestion = true;
            }
            else
            {
                loadNextQuestion = false;
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
        }

    }

}
