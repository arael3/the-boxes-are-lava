using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    [HideInInspector] public int pointsAmount;
    [HideInInspector] public bool addPointsForTime = false;

    [SerializeField] GameObject newHighScoreText;

    [SerializeField] GameObject highScoreText;

    TextMeshProUGUI pointsUI;
    Timer timer;

    bool addPointsPause = false;

    // Start is called before the first frame update
    void Start()
    {
        pointsAmount = 0;
        pointsUI = GetComponent<TextMeshProUGUI>();
        timer = GameObject.Find("Time Text").GetComponent<Timer>();
        highScoreText.GetComponent<TextMeshProUGUI>().text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
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
        //if (timer.timeInt > 0)
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

            if (pointsAmount > PlayerPrefs.GetInt("HighScore", 0))
            {
                newHighScoreText.SetActive(true);

                PlayerPrefs.SetInt("HighScore", pointsAmount);

                highScoreText.GetComponent<TextMeshProUGUI>().text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore", 0);
            }

            StartCoroutine("WaitAfterPointsForTimeAddedCoroutine");
        }

    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);

        highScoreText.GetComponent<TextMeshProUGUI>().text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    public IEnumerator WaitAfterPointsForTimeAddedCoroutine()
    {
        yield return new WaitForSeconds(2f);

        newHighScoreText.SetActive(false);

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().StartCoroutine("NextLevel");

        yield break;
    }
}
