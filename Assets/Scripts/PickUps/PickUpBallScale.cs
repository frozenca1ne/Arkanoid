using UnityEngine;

namespace PickUps
{
    public class PickUpBallScale : MonoBehaviour
    {
        [SerializeField] private float scale;
        private Ball _ball;
        private void Start()
        {
            _ball = FindObjectOfType<Ball>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                _ball.ModifyScale(scale);
            }
            Destroy(gameObject);
        }
    }
}
