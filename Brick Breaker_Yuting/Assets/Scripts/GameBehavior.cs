using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class  GameBehavior : MonoBehaviour
{
    public static GameBehavior _instance;
    public static GameBehavior Instance {
        get
        {
            if (!_instance)
            {
                GameObject go = new GameObject("GameBehavior");
                _instance = go.AddComponent<GameBehavior>();
                DontDestroyOnLoad(go);
            }

            return _instance;
        }
    }

    public float InitBallSpeed = 5.0f;
    public float BallSpeedIncrement = 1.2f;

    [SerializeField] private int _scoreToVictory = 3;

    public Utilities.GameplayState State = Utilities.GameplayState.Play;

    [SerializeField] private TextMeshProUGUI _messages;
    
    public int level = 1;
    public int score = 0;
    public int lives = 3;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private AudioSource _source;
    void Start()
    {
        NewGame();
        _source = GetComponent<AudioSource>();
        State = Utilities.GameplayState.Play;
        // _messages = GetComponent<TextMeshProUGUI>();
        // _messages.enabled = false;
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;

        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        SceneManager.LoadScene("Level" + level);
    }
   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchState();
        }
    }

    private void SwitchState()
    {
        if (State == Utilities.GameplayState.Play)
        {
            State = Utilities.GameplayState.Pause;
            Time.timeScale = 0;
            print("zan ting la!");
            // _messages.text = "Pause";
            // _messages.enabled = true;
        }
        else
        {
            State = Utilities.GameplayState.Play;
            Time.timeScale = 1;
            // _messages.enabled = false;
        }
    }
}
    