using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Block : MonoBehaviour
{
    [SerializeField] private GameObject[] pickUps;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private GameObject destroyFx;

    [SerializeField] private bool visibility = true;

    [Header("Explode")]
    [SerializeField] private bool isExploding;
    [SerializeField] private float explodeRadius = 2f;


    [Header("Points")]
    [SerializeField] private int pointsToBreak = 1;
    [SerializeField] private int pointsPerBlock = 10;
    
    private LevelManager _levelManager;
    private GameManager _gameManager;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _levelManager = FindObjectOfType<LevelManager>();
        _levelManager.AddBlockCount();
        _gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        VisibilityBlock();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (visibility == false)
        {
            visibility = true;
        }
        DestroyBlock();
    }
    public void DestroyBlock()
    {
        pointsToBreak -= 1;
        if (pointsToBreak != 0) return;
        audioSource.PlayOneShot(destroySound);
        Destroy(gameObject);
        _gameManager.AddScore(pointsPerBlock);
        
        if (pickUps.Length != 0)
        {
            var randomChance = Random.Range(0, 10);
            if (randomChance < 3)
            {
                var pickupPosition = transform.position;
                var pickUpIndex = Random.Range(0, pickUps.Length - 1);
                var pickUp = pickUps[pickUpIndex];
                Instantiate(pickUp, pickupPosition, Quaternion.identity);
            }
        }
        if (destroyFx != null)
        {
            var destroyFxVector = transform.position;
            var newObject = Instantiate(destroyFx, destroyFxVector, Quaternion.identity);
            Destroy(newObject, 1f);
        }
        if (isExploding)
        {
            LayerMask layerMask = LayerMask.GetMask("Block");
            var objectsInRadius = Physics2D.OverlapCircleAll(transform.position, explodeRadius, layerMask);

            foreach (var objectsI in objectsInRadius)
            {
                if (objectsI.gameObject == gameObject)
                {
                    continue;
                }

                var block = objectsI.gameObject.GetComponent<Block>();
                if (block == null)
                {
                    Destroy(objectsI.gameObject);
                }
                else
                {
                    block.DestroyBlock();
                }

            }
        }
        _levelManager.RemoveBlockCount();
    }

    private void VisibilityBlock()
    {
        _sprite.enabled = visibility != false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }

}
