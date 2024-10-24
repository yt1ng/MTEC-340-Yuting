using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodSpawner : MonoBehaviour
{
    public int ylimit = 11;
    public int xoffset = 7;
    private static FoodSpawner _instance;
    public static FoodSpawner Instance { get { return _instance; } }

    public GameObject foodPre;
    public GameObject rewardPre;
    public Sprite[] foodSprites;
    public Transform foodMaker;

   

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foodMaker = GameObject.Find("FoodHolder").transform;
        //Debug.Log(foodMaker);
        MakeFood(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeFood(bool isReward)
    {
        GameObject obj = GameObject.Instantiate(foodPre);
        obj.transform.SetParent(foodMaker, false);
        int x = Random.Range(-320, 620);
        int y = Random.Range(-340, 340);
        //Debug.Log(x);
        //Debug.Log(y);
        obj.transform.localPosition = new Vector3(x, y, 0);
        int index = Random.Range(0, foodSprites.Length);
        obj.GetComponent<Image>().sprite = foodSprites[index];
        if (isReward)
        {
            GameObject objReward = GameObject.Instantiate(rewardPre);
            objReward.transform.SetParent(foodMaker, false);
            x = Random.Range(-320, 620);
            y = Random.Range(-340, 340);
            //Debug.Log(x);
            //Debug.Log(y);
            objReward.transform.localPosition = new Vector3(x, y, 0);
        }
    }

}
