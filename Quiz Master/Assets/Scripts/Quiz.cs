using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    //[SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    List<QuestionSO> questions = new List<QuestionSO>();
    QuestionGenerator generatedQuestions; // Generated Questions to replace questions for testing
    [SerializeField] TextMeshProUGUI questionText;
    QuestionSO currentQuestion;

    [Header("Answer")]
    int correctAnswerIndex;
    bool hasAnsweredEarly;
    [SerializeField] GameObject[] answerButtons;

    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    [Header("Splash Screen")]
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;

    bool splashShown = false; // isComplete
    public static bool quizInitialized = false;

    void Start()
    {
        if (SelectionScreen.isSelected && quizInitialized)
        {

            generatedQuestions = FindObjectOfType<QuestionGenerator>();
            questions = generatedQuestions.GetQuestions();

            timer = FindObjectOfType<Timer>();
            scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
            progressBar.maxValue = questions.Count;
            progressBar.value = 0;

            GetNextQuestion();
            DisplayQuestion();

        }
    }

    private void Update()
    {
        if (SelectionScreen.isSelected && quizInitialized)
        {
            if (!splashShown)
            {

                timerImage.fillAmount = timer.fillFraction;

                if (timer.loadNextQuestion)
                {
                    hasAnsweredEarly = false;
                    GetNextQuestion();
                    timer.loadNextQuestion = false;
                }
                else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
                {
                    SetButtonState(false);
                    DisplayAnswer(-1);
                }
            }
            else
            {
                // Stop updating
                this.enabled = false;
            }
        }

    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        // Display Buttons
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    private void DisplayAnswer(int index)
    {

        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        Image buttonImage;

        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            questionText.text = "Sorry, the correct answer was: \n" + correctAnswer;
        }
    }
    private void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
        else
        {
            DisplayEndSplashScreen();
        }
    }

    private void DisplayEndSplashScreen()
    {

        Image buttonImage;

        questionText.text = "You've completed the quiz! \n \n Your Final Score was: " + scoreKeeper.GetCorrectAnswers() + " of " + scoreKeeper.GetQuestionsSeen();
 
        for (int i = 0; i < answerButtons.Length; i++)
        {
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        
        timer.CancelTimer();
        timer.fillFraction = 360;
        this.enabled = false;
        timer.enabled = false;
        splashShown = true;

        // Enable quit/restart
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

    }

    private void GetRandomQuestion()
    {

        if (questions.Count > 0)
        {
            int index = Random.Range(0, questions.Count);
            currentQuestion = questions[index];

            if (questions.Contains(currentQuestion))
            {
                questions.Remove(currentQuestion);
            }
        }   

        }

        private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }

    }

    private void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    private void SetDefaultButtonSprites()
    {
        Image buttonImage;

        for (int i = 0; i< answerButtons.Length; i++)
        {
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }

    }

}
