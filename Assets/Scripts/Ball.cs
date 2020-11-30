using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Explode")]
    [SerializeField] private float explodeRadius = 2f;

    [SerializeField] private float speed = 7f;
    
    [SerializeField] private LayerMask explosiveObjects;
    [SerializeField] private AudioSource audioSource;
    
    private Platform _platform;
    private GameManager _gameManager;
    private Rigidbody2D _rigidbody;
    private Vector3 _ballOffset;
    
    public bool IsBallExplosive { get;set; }
    public bool IsBallStarted { get; private set; }

    public bool IsBallSticky { get; set; }
    private void Awake()
    {
        IsBallStarted = false;
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _platform = FindObjectOfType<Platform>();
        _ballOffset = transform.position - _platform.transform.position;
        
    }  
    private void Update()
    {
       if(!IsBallStarted) MoveWithPlatform();
    }
    private void MoveWithPlatform()
    {
        transform.position = _platform.transform.position + _ballOffset;
        if (!Input.GetMouseButtonDown(0)) return;
        if(_gameManager.pauseActive != true)
        {
            LaunchBall();
        }
    }

    private void LaunchBall()
    {
        IsBallStarted = true;
        var speedVector = new Vector2(Random.Range(-2,2),1);
        _rigidbody.velocity = speedVector * speed;
    }

    public void Duplicate()
    {
        var newBall = Instantiate(this);
        newBall.LaunchBall();
        if(IsBallSticky)
        {
            newBall.IsBallSticky = true;
        }
    }
   
    private void OnDrawGizmos()
    {
        if (_rigidbody == null) return;
        Gizmos.color = Color.red;
        var position = transform.position;
        Gizmos.DrawLine(position, position + (Vector3)_rigidbody.velocity);
        Gizmos.DrawWireSphere(position, explodeRadius);

    }
    public void ModifySpeed(float modificator)
    {
        _rigidbody.velocity *= modificator;
    }

    public void ModifyScale(float modificator)
    {
        transform.localScale *= modificator;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
        if (collision.gameObject.CompareTag("Platform"))
        {
            if(IsBallSticky)
            { 
                IsBallStarted = false;
                _rigidbody.velocity = Vector3.zero;
                _ballOffset = transform.position - _platform.transform.position;
            }
        }
        if(collision.gameObject.CompareTag("Block"))
        {
            MakeExplosive();
        }
    }
    private void MakeExplosive()
    {
        if (IsBallExplosive != true) return;
        var maxColliders = 10;
        var hitColliders = new Collider2D[maxColliders];
        var blocksInRadius = Physics2D.OverlapCircleNonAlloc(transform.position, explodeRadius,hitColliders, explosiveObjects);
        for(var i = 0;i<blocksInRadius;i++)
        {
            var blockInRadius = hitColliders[i].gameObject.GetComponent<Block>();
            if (blockInRadius != null)
            {
                blockInRadius.DestroyBlock();
            }
        }
    }

    public void ReturnBall()
    {
        IsBallStarted = false;
        _rigidbody.velocity = Vector3.zero;
        transform.position = _ballOffset;
    }
}
