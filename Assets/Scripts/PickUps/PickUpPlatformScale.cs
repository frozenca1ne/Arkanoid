using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPlatformScale : MonoBehaviour
{
    Platform platform;

    public float modifyScale;

    void Start()
    {
        platform = FindObjectOfType<Platform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            platform.ModifyScale(modifyScale);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("LoseGame"))
        {
            Destroy(gameObject);
        }

    }
}
