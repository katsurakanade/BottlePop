using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public Text scoreText;
    public GameObject excellent;
    public float score = 1000.0f;
    public float animationTime = 2;

    private void Start()
    {
        GameManager.instance.scoring = this;
    }

    private string _countAndTime;
    private float _clearTime;
    private float _score;
    public void StartScore()
    {
        _clearTime = GameManager.instance.sceneClearTime - GameManager.instance.sceneStartTime;
        
        //Score calculation
        _score = GameManager.instance.barController.collectedCount * score *
                 (GameManager.instance.medianTime / _clearTime);
        
        _countAndTime = "アメダマ: " + GameManager.instance.barController.collectedCount + "/" + 
                                      GameManager.instance.barController.maxCount + " 時間: " + Mathf.FloorToInt(_clearTime)
                                      + " スコア: ";
        StartCoroutine(ScoreAnimation());
    }

    private IEnumerator ScoreAnimation()
    {
        float g_score = 0;
        while (_score > g_score)
        {
            g_score += _score * Time.deltaTime / animationTime;
            scoreText.text = $"{_countAndTime}{Mathf.FloorToInt(g_score)}";
            yield return null;
        }

        int scoreFloored = Mathf.FloorToInt(_score);
        scoreText.text = $"{_countAndTime}{scoreFloored}";

        string highScoreName = $"{SceneManager.GetActiveScene().name}HighScore";
        int highScore = PlayerPrefs.GetInt(highScoreName);

        if (scoreFloored > highScore)
        {
            //Display Best Score visual
            PlayerPrefs.SetInt(highScoreName, scoreFloored);
            excellent.SetActive(true);
        }
    }
}
