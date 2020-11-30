using UnityEngine;

namespace PickUps
{
    public class PickUpExplode : MonoBehaviour
    {
        private void ApplyEffect()
        {
            var balls = FindObjectsOfType<Ball>();
            foreach(var ball in balls)
            {
                ball.IsBallExplosive = true;
                var ballSprite = ball.GetComponent<SpriteRenderer>();
                ballSprite.color = Color.red;
                var ballRenderer = ball.GetComponent<TrailRenderer>();
                ballRenderer.material.color = Color.yellow;
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
