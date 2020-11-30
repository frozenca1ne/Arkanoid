using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseGame : MonoBehaviour
{
    [SerializeField] private int maxHealthCount = 5;
    [SerializeField] private Image[] healthImages;
    [SerializeField] private int startHealth = 3;
    
    private Ball _ball;
    private void Start()
    {
        _ball = FindObjectOfType<Ball>();
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;
        var balls = FindObjectsOfType<Ball>();
        if (balls.Length > 1)
        {
            Destroy(collision.gameObject);
        }
        else
        {
            DoAgain();
        }
    }

    private void DoAgain()
    {
        startHealth--;
        HeartOff(startHealth);
        if (startHealth != 0)
        {
            _ball.ReturnBall();
        }
        else
        {
            var allScenes = SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(allScenes - 1);
        }
    }
   
    public void ModifyHealth(int modificator)
    {
        if (startHealth == 0) return;
        startHealth += modificator;
        HeartOn(startHealth);
    }
     private void HeartOn(int index)
    {
        if(startHealth<=maxHealthCount)
        {
            healthImages[index - 1].gameObject.SetActive(true);
        }
    }
    private void HeartOff(int index)
    {
        if (startHealth <= 5)
        {
            healthImages[index].gameObject.SetActive(false);
        }
    }
}
