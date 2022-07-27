using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Points : MonoBehaviour
{
    [HideInInspector] public int pointsAmount;
    [HideInInspector] public bool addPointsForTime = false;

    [SerializeField] GameObject newHighScoreText;

    [SerializeField] GameObject highScoreText;

    SoundController soundController;

    TextMeshProUGUI pointsUI;
    Timer timer;

    bool addPointsPause = false;

    void Start()
    {
        pointsAmount = 0;
        pointsUI = GetComponent<TextMeshProUGUI>();
        timer = GameObject.Find("Time Text").GetComponent<Timer>();
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        highScoreText.GetComponent<TextMeshProUGUI>().text = "LVL " + SceneManager.GetActiveScene().buildIndex + " - HIGH SCORE: " + GetHighScore();
    }

    void Update()
    {
        if (addPointsForTime)
        {
            PointsForTime();
        }

        pointsUI.text = pointsAmount.ToString();
    }

    public void PointsForTime()
    {
        if (Timer.timeInt > 0)
        {
            if (addPointsPause)
            {
                Timer.timeInt--;
                pointsAmount++;

                addPointsPause = false;
            }
            else addPointsPause = true;
        }
        else
        {
            addPointsForTime = false;
            timer.isPointsForTimeAdded = true;

            if (pointsAmount > PlayerPrefs.GetInt("Lvl" + SceneManager.GetActiveScene().buildIndex + "HighScore", 0))
            {
                newHighScoreText.SetActive(true);

                SetHighScore();

                highScoreText.GetComponent<TextMeshProUGUI>().text = "LVL " + SceneManager.GetActiveScene().buildIndex + " - HIGH SCORE: " + GetHighScore();
            }

            soundController.PlaySound("Win");

            StartCoroutine("WaitAfterPointsForTimeAddedCoroutine");
        }
    }
    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("Lvl" + SceneManager.GetActiveScene().buildIndex + "HighScore", 0);

        highScoreText.GetComponent<TextMeshProUGUI>().text = "LVL " + SceneManager.GetActiveScene().buildIndex + " - HIGH SCORE: " + GetHighScore();
    }


    void SetHighScore()
    { 
        PlayerPrefs.SetInt("Lvl" + SceneManager.GetActiveScene().buildIndex + "HighScore", pointsAmount);
    }

    string GetHighScore()
    {
        return PlayerPrefs.GetInt("Lvl" + SceneManager.GetActiveScene().buildIndex + "HighScore", 0).ToString();
    }

    public IEnumerator WaitAfterPointsForTimeAddedCoroutine()
    {
        yield return new WaitForSeconds(2f);

        newHighScoreText.SetActive(false);

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().StartCoroutine("NextLevel");

        yield break;
    }
}
