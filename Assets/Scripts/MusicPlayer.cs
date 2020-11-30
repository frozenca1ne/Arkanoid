using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        var musicPlayers = FindObjectsOfType<MusicPlayer>();
        if (musicPlayers.Length <= 1) return;
        Destroy(gameObject);
        gameObject.SetActive(false);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
