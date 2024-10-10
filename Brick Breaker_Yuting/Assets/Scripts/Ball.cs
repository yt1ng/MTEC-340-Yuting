using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 500f;

    private float _speed;

    [SerializeField] float _yLimit = 4.85f;
    [SerializeField] float _xLimit = 10.0f;

    private Vector2 _direction;

    private AudioSource _source;

    [SerializeField] private AudioClip _wallHit;
    [SerializeField] private AudioClip _paddleHit;
    [SerializeField] private AudioClip _scorePoint;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Invoke(nameof(SetRandomTrajectory), 1f);
        _source = GetComponent<AudioSource>();
        _source.Play();
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;
        this.rigidbody.AddForce(force.normalized * this.speed);
    }

    void Update()
    {
        if (GameBehavior.Instance.State == Utilities.GameplayState.Play)
        {
            
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Paddle>() != null)
        {
            _speed *= GameBehavior.Instance.BallSpeedIncrement;
            _direction.x *= -1;
            
            print ("Paddle Hit");
            
            _source.pitch = Random.Range(0.75f, 1.25f);
            _source.clip = _paddleHit;
            _source.PlayOneShot(_paddleHit);
        }
    }

     void ResetBall()
        {
            transform.position = Vector3.zero;

            _direction = new Vector2(
                // Ternary operator
                // condition ? passing : failing
                Random.value > 0.5f ? 1 : -1,
                Random.value > 0.5f ? 1 : -1
            );

            _speed = GameBehavior.Instance.InitBallSpeed;
        }

        public void hitBricks()
        {
            print("HitBricks");
            _source.clip = _wallHit;
            _source.PlayOneShot(_wallHit);
        }
}