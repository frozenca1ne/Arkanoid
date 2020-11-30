using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    public void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public int ReturnIndex()
    {

        return SceneManager.GetActiveScene().buildIndex;
    }

    public void SelectLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
