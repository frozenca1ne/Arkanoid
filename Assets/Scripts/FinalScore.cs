using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    [SerializeField] private Text finalScore;
    
    private GameManager _gameManager;
    private ScenesLoader _scenesLoader;
    
    private void Start()
    {
       _gameManager = FindObjectOfType<GameManager>();
       _scenesLoader = FindObjectOfType<ScenesLoader>();
    }
    private void Update()
    {
        SetScore();
    }
   
    private void SetScore()
    {
        var index = _scenesLoader.ReturnIndex();
        if (index != 0)
        {
            GetFinalScore(_gameManager.Score);
        }
        else
        {
            ModifyBestScore(_gameManager.Score);
            SetBestScore();
            _gameManager.Score = 0;          
        }
    }
    private void ModifyBestScore(int score)
    {
        var currentScore = PlayerPrefs.GetInt("BestScore", 0);
        if(score>currentScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
    }
    private void GetFinalScore(int score)
    {
        finalScore.text = "SCORE : " + score;
    }
    private void SetBestScore()
    {
        var score = PlayerPrefs.GetInt("BestScore",0);
        finalScore.text = "BEST SCORE : " + score;
    }
}
