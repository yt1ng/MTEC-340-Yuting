using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    
    private static MainUI _instance;

    public int score = 0;
    public int length = 0;
    public bool isBorder;
    public Button pauseButton;
    public Button homeButton;
    public TextMeshProUGUI msgText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lengthText;
    public Sprite[] sprites;
    public Image pauseButtonImage;
    public Image bgImage;
    public GameObject Wall;
    public Color tempColor;

    public bool isPause = false;

    public static MainUI Instance 
    {
        get 
        { 
            return _instance; 
        } 
    }

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseButton.onClick.AddListener(Pause);
        homeButton.onClick.AddListener(Home);

        if (PlayerPrefs.GetInt("Mode", 0) == 0)
        {
            isBorder = false;
            foreach(Transform t in Wall.gameObject.transform)
            {
                t.gameObject.GetComponent<Image>().enabled = false;
            }
        }
        else
        {
            isBorder = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (score/100)
        {
            case 0:
            case 1:
            case 2:
                break;  
            case 3 :
            case 4 :
                ColorUtility.TryParseHtmlString("#CCEEFFFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "Level" + 2;
                break;
            case 5 :
            case 6 :
                ColorUtility.TryParseHtmlString("#CCFFDBFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "Level" + 3;
                break;
            case 7 :
            case 8 :
                ColorUtility.TryParseHtmlString("#EBFFCCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "Level" + 4;
                break;
            case 9 :
            case 10 :
                ColorUtility.TryParseHtmlString("#FFF3CCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "Level" + 5;
                break;
            default :
                ColorUtility.TryParseHtmlString("#FFDACCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "Endless";
                break;
            
        }
    }

    public void UpdateUI(int s = 5,int l=1)
    {
        score = score + s;
        length = length + l;
        scoreText.text = "Score:\n" + score;
        lengthText.text = "Length:\n" + length;
    }

    public void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;
            pauseButtonImage.sprite = sprites[1];
        }
        if (isPause == false)
        {
            Time.timeScale = 1;
            pauseButtonImage.sprite = sprites[0];
        }
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }
}
