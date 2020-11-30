using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image panel;
    [SerializeField] private int score = 0;
    [SerializeField] private float autoplaySpeed = 1.5f;
    
    public bool autoplay;
    public bool pauseActive;

    public int Score
    {
        get => score;
        set => score = value;
    }

    private Platform _platform;

    public void AddScore(int points)
    {
        score += points;
    }

    public void ReturnToGame()
    {
        panel.gameObject.SetActive(false);
        Time.timeScale = 1;
        pauseActive = false;
        _platform.enabled = true;
    }
    private void Awake()
    {
        var gameManager = FindObjectsOfType<GameManager>();
        if (gameManager.Length <= 1) return;
        Destroy(gameObject);
        gameObject.SetActive(false);
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _platform = FindObjectOfType<Platform>();
    }
    private void Update()
    {
        GamePause();

        if (autoplay)
        {
            Time.timeScale = autoplaySpeed;
        }
    }

    private void GamePause()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (pauseActive)
        {
            if (autoplay)
            {
                Time.timeScale = autoplaySpeed;
            }
            else
            {
                Time.timeScale = 1;
            }
            pauseActive = false;
            _platform.enabled = true;
        }
        else
        {
            Time.timeScale = 0;
            pauseActive = true;
            panel.gameObject.SetActive(true);
            _platform.enabled = false;
        }
    }
}
