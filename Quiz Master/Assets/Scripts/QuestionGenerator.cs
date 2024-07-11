using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Web;

public class QuestionGenerator : MonoBehaviour
{
    System.Random r = new System.Random();
    string jsonString;
    int indexToInsertAt;
    List<String> answers = new List<String>();
    public List<QuestionSO> questionsGenerated = new List<QuestionSO>();

    public List<QuestionSO> GetQuestions()
    {

        int dif = SelectionScreen.difficulty;
        int cat = SelectionScreen.category;
        string baseUrl;
        String difficulty;
        String category;

        switch (dif)
        {
            case 0:
                difficulty = "&difficulty=easy";
                break;
            case 1:
                difficulty = "&difficulty=medium";
                break;
            case 2:
                difficulty = "&difficulty=hard";
                break;
            case 3:
                difficulty = "";
                break;
            default:
                difficulty = "";
                break;
        }

        switch (cat)
        {
            case 0:
                category = "";
                break;
            case 1:
                category = "&category=27"; //Animals
                break;
            case 2:
                category = "&category=25"; //Art
                break;
            case 3:
                category = "&category=26"; //Celebrities
                break;
            case 4:
                category = "&category=9"; //General Knowledge
                break;
            case 5:
                category = "&category=16"; //E:Boardgames
                break;
            case 6:
                category = "&category=10"; //E:Books
                break;
            case 7:
                category = "&category=32"; //E:Cartoons & Animations
                break;
            case 8:
                category = "&category=29"; //E:Comics
                break;
            case 9:
                category = "&category=11"; //E:Film
                break;
            case 10:
                category = "&category=12"; //E:Music
                break;
            case 11:
                category = "&category=13"; //E:Musicals
                break;
            case 12:
                category = "&category=14"; //E:Television
                break;
            case 13:
                category = "&category=15"; //E:Books
                break;
            case 14:
                category = "&category=31"; //E:Japanese Animation
                break;
            case 15:
                category = "&category=22"; //Geography
                break;
            case 16:
                category = "&category=23"; //History
                break;
            case 17:
                category = "&category=20"; //Mythology
                break;
            case 18:
                category = "&category=24"; //Politics
                break;
            case 19:
                category = "&category=17"; //Science & Nature
                break;
            case 20:
                category = "&category=18"; //Science: Computers
                break;
            case 21:
                category = "&category=30"; //Science: Gadgets
                break;
            case 22:
                category = "&category=19"; //Science: Maths
                break;
            case 23:
                category = "&category=21"; //Sports
                break;
            case 24:
                category = "&category=28"; //Vehicles
                break;
            default:
                category = "";
                break;
        }

        baseUrl = "https://opentdb.com/api.php?amount=10&type=multiple" + category + difficulty;

        try
        {
            jsonString = new WebClient().DownloadString(baseUrl);
        }
        catch (WebException e)
        {
            throw e;
        }

        if (jsonString.Length > 0)
        {

            var rootJSON = JsonConvert.DeserializeObject<Root>(jsonString);

                if (rootJSON != null)
                {
                    foreach (Result result in rootJSON.results)
                    {
                        // Random index to insert the correct answer
                        indexToInsertAt = r.Next(1, 3);

                        // Unescape each string
                        foreach (string answer in result.incorrect_answers)
                        {
                            answers.Add(System.Web.HttpUtility.HtmlDecode(answer));
                        }

                        answers.Insert(indexToInsertAt, System.Web.HttpUtility.HtmlDecode(result.correct_answer));

                        result.question = System.Web.HttpUtility.HtmlDecode(result.question);

                        QuestionSO tempData = QuestionSO.CreateInstance(result.question, answers.ToArray(), indexToInsertAt);

                        questionsGenerated.Add(tempData);
                        // Must empty between runs
                        answers.Clear();

                    }


                }

        }

        // Returns a list of 10 questions to calling method
        return questionsGenerated;

    }

}
