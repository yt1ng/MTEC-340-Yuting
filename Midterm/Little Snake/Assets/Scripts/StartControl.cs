using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartControl : MonoBehaviour
{
    public TextMeshProUGUI  lastText;
    public TextMeshProUGUI bestText;

    public Button startButton;
    
    public Toggle border;
    public Toggle blue;
    public Toggle yellow;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        blue.onValueChanged.AddListener(BlueS);
        yellow.onValueChanged.AddListener(YellowS); 
        
        Awake();

        if (PlayerPrefs.GetString("snakeh", "sh01") == "sh01")
        {
            blue.isOn = true;
            PlayerPrefs.SetString("snakeh", "sh01");
            PlayerPrefs.SetString("snakeb01", "sb0102");
            PlayerPrefs.SetString("snakeb02", "sb0101");
        }
        else
        {
            yellow.isOn = true;
            PlayerPrefs.SetString("snakeh", "sh02");
            PlayerPrefs.SetString("snakeb01", "sb0202");
            PlayerPrefs.SetString("snakeb02", "sb0201");
        }

        if (PlayerPrefs.GetInt("Mode", 0) == 0)
        {
            PlayerPrefs.SetInt("Mode", 0);
        }
        else
        {
            border.isOn = true;
            PlayerPrefs.SetInt("Mode", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {
        lastText.text= "Last:Length"+ PlayerPrefs.GetInt("lastl",0) + ",Score" + PlayerPrefs.GetInt("lasts",0);
        bestText.text = "Best:Length" + PlayerPrefs.GetInt("bestl",0) + ",Score" + PlayerPrefs.GetInt("bests",0);
        
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void BlueS(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetString("snakeh", "sh01");
            PlayerPrefs.SetString("snakeb01", "sb0102");
            PlayerPrefs.SetString("snakeb02", "sb0101");
        }
    }

    public void YellowS(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetString("snakeh", "sh02");
            PlayerPrefs.SetString("snakeb01", "sb0202");
            PlayerPrefs.SetString("snakeb02", "sb0201");
        }
    }

    public void FreeS(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("Mode", 0);
        }
    }

    public void BorderS(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("Mode", 1);
        }
    }
}
