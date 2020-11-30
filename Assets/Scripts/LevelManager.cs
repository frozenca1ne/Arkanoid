using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int loadSceneDelay = 1;
    
    private int _blocksNumber;
    public void AddBlockCount()
    {
        _blocksNumber++;
    }

    public void RemoveBlockCount()
    {
        _blocksNumber--;
        if (_blocksNumber <= 0)
        {
            StartCoroutine(LoadNextLevel(loadSceneDelay));
        }
    }

    private IEnumerator LoadNextLevel(int delay)
    {
        yield return new WaitForSeconds(delay);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
