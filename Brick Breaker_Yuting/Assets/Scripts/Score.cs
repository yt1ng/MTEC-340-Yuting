using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]private int _score = 0;
    [SerializeField]public TMP_Text mText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    public int score
    {
        get => _score;
        
        
        set 
        {
            _score = value;
            
            this.mText.text = _score.ToString();
        }
    }
    
}