using UnityEngine;

namespace PickUps
{
    public class PickUpCatchBall : MonoBehaviour
    {
        private void ApplyEffect()
        {
            var balls = FindObjectsOfType<Ball>();
            foreach (var ball in balls)
            {
                ball.IsBallSticky = true;
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
