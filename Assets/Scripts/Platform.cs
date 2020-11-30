using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float minPlatformPositionX = -6.6f;
    [SerializeField] private float maxPlatformPositionX = 6.6f;
    
    private Ball _ball;
    private GameManager _gameManager;

    private void Start()
    {
        _ball = FindObjectOfType<Ball>();
        _gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (_gameManager.autoplay && _ball.IsBallStarted)
        {
            MoveWithBall();
        }
        else
        {
            MovePlatform();
        }
    }
    private void MovePlatform()
    {
        var mousePosition = Input.mousePosition;
        if (Camera.main == null) return;
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        var mousePositionX = mouseWorldPos.x;
        var clampMousePositionX = Mathf.Clamp(mousePositionX,minPlatformPositionX,maxPlatformPositionX);
        var mousePositionY = transform.position.y;
        transform.position = new Vector3(clampMousePositionX, mousePositionY, 0);
    }
    private void MoveWithBall()
    {
        transform.position = new Vector3(_ball.transform.position.x, transform.position.y, 0);
    }

    public void ModifyScale(float modificator)
    {
        var scaleX = transform.localScale;
        scaleX.x *= modificator;
        transform.localScale = scaleX;
    }
}
