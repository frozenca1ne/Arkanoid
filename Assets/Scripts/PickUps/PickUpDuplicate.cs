using UnityEngine;

namespace PickUps
{
    public class PickUpDuplicate : MonoBehaviour
    {
        [SerializeField] private int newBallNumber = 2;

        private void ApplyEffect()
        {
            var ball = FindObjectOfType<Ball>();
            for (var i = 0; i < newBallNumber; i++)
            {
                ball.Duplicate();
            }
        
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                ApplyEffect();
            }
            Destroy(gameObject);
        }
    }
}
