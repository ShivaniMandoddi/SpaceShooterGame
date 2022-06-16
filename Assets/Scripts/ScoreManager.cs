using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public int score;
    public Text scoreText;
    #endregion
    #region PRIVATE VARIABLES

    #endregion
    #region SINGLETON CLASS
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
            }
            return instance;
        }
    }
    #endregion
    #region MONOBEHAVIOUR METHODS
    private void Start()
    {
        DisplayScore();
    }

    #endregion

    #region PUBLIC METHODS
    public void ScoreIncrement(int value)
    {
        score += value;
        DisplayScore();
    }
    public void ScoreDecrement(int value)
    {
        score = Mathf.Clamp(score - value, 0, 100);
        DisplayScore();
    }
    public int GetScore()
    {
        return score;
    }
    public void DisplayScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    #endregion
    #region PRIVATE METHODS

    #endregion
}
