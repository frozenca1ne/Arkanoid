using UnityEngine;

namespace PickUps
{
    public class PickUpHealth : MonoBehaviour
    {
        private LoseGame _loseGame;

        public int modificator;
        private void Start()
        {
            _loseGame = FindObjectOfType<LoseGame>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                _loseGame.ModifyHealth(modificator);
            }
            Destroy(gameObject);
        }
    }
}
