using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class SnakeHead : MonoBehaviour
{
    private Rigidbody2D sankeRigid;

    List<Transform> bodyList = new List<Transform>();
    
    public float velocity = 0.4f;
    public int step = 50;
    private int x;
    private int y;
    private Vector3 headPos;
    private Transform canvas;
    public GameObject Canvas;
    
    public Sprite[] bodysprites = new Sprite[2];

    public GameObject effect;
    public GameObject snakeBodyPre;
    public AudioClip[] eatClip;
    public AudioClip dieClip;
    public AudioClip levelupClip;
    
    public Sprite head;
    
    private bool isDie = false;

    private Vector2 sankeV;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("F","sh01"));
        bodysprites[0] = Resources.Load<Sprite>(PlayerPrefs.GetString("snakeb01", "sb0102"));
        bodysprites[1] = Resources.Load<Sprite>(PlayerPrefs.GetString("snakeb02", "sb0101"));

        sankeRigid = GetComponent<Rigidbody2D>();
        //Debug.Log(sankeRigid);
        InvokeRepeating("Move", 1, 0.3f);
        x = 0;y = step;
        lastPosition.Add(transform.localPosition);
        
        InvokeRepeating("Move",1,velocity);
    }

    // Update is called once per frame
    
    
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Space) && MainUI.Instance.isPause == false)
        {
                velocity -= 0.1f;
                if (velocity <= 0.0f)
                {
                    velocity = 0.4f;
                }
                CancelInvoke();
                InvokeRepeating("Move",0,velocity);
            
        }
        if (Input.GetKeyUp(KeyCode.Space) && MainUI.Instance.isPause == false)
        {
            
        }
        if (Input.GetKey(KeyCode.W) && y != -step && MainUI.Instance.isPause == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            x = 0;
            y = step;
        }
        if (Input.GetKey(KeyCode.S)&& y != step && MainUI.Instance.isPause == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            x = 0;
            y = -step;
        }
        if (Input.GetKey(KeyCode.A) && x != step && MainUI.Instance.isPause == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            x = -step;
            y = 0;
        }
        if (Input.GetKey(KeyCode.D)&& x != -step && MainUI.Instance.isPause == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
            x = step;
            y = 0;
        }
    }

    private void GetInput()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.W) && y != -step && MainUI.Instance.isPause == false && isDie == false)
        {
            x = 0;y = step;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.S) && y != step && MainUI.Instance.isPause == false && isDie == false)
        {
            x = 0; y = -step;
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKey(KeyCode.A) && x != step && MainUI.Instance.isPause == false && isDie == false)
        {
            x = -step; y = 0;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKey(KeyCode.D) && x != -step && MainUI.Instance.isPause == false && isDie == false)
        {
            x = step; y = 0;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }


        //sankeV = new Vector2(h, v);

        Debug.Log(sankeV);
    }

    private List<Vector3> lastPosition = new List<Vector3>();
    
    private void Move()
    {
        //sankeRigid.velocity = sankeV;
        headPos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(headPos.x + x, headPos.y + y, headPos.z);
        if (bodyList.Count > 0)
        {
            for (int i = lastPosition.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    lastPosition[i]= gameObject.transform.localPosition;
                }
                else
                {
                    lastPosition[i] = lastPosition[i - 1];
                }
            }

            for (int i = 0; i < bodyList.Count; i++)
            {
                bodyList[i].localPosition = lastPosition[i];
            }
            bodyList[0].localPosition = headPos;
        }
    }
    private void Grow()
    {
        Debug.Log(bodyList.Count);
        int index = bodyList.Count % 2;
        GameObject Body = Instantiate(snakeBodyPre);
        
        Body.transform.SetParent(Canvas.transform, false);
        Body.GetComponent<Image>().sprite = bodysprites[index];
        Body.transform.transform.localPosition = lastPosition[lastPosition.Count - 1];
        lastPosition.Add(Body.transform.transform.localPosition);
        bodyList.Add(Body.transform);
    }

    private void Die()
    {
        CancelInvoke();
        isDie = true;
        Debug.Log(isDie);
        Instantiate(effect);
        AudioSource.PlayClipAtPoint(dieClip, Vector3.zero);
        StartCoroutine(GameOver(1.5f));
        PlayerPrefs.SetInt("lasts", MainUI.Instance.score);
        PlayerPrefs.SetInt("lastl", MainUI.Instance.length);
        if (PlayerPrefs.GetInt("bests", 0) < MainUI.Instance.score)
        {
            PlayerPrefs.SetInt("bests", MainUI.Instance.score);
            PlayerPrefs.SetInt("bestl", MainUI.Instance.length);
        }
    }

    IEnumerator GameOver(float t)
    {
        yield return new WaitForSeconds(t);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == ("Food"))
        {
            
            int v = Random.Range(0, eatClip.Length);
            
            AudioSource.PlayClipAtPoint(eatClip[v], Vector3.zero);
            Destroy(collision.gameObject);

            
            MainUI.Instance.UpdateUI();
            Grow();
            FoodSpawner.Instance.MakeFood((Random.Range(0, 100) < 20) ? true : false);
        }else if (collision.tag == "Reward")
        {
            
            int v = Random.Range(0, eatClip.Length);
            AudioSource.PlayClipAtPoint(eatClip[v], Vector3.zero);
            Destroy(collision.gameObject);
            MainUI.Instance.UpdateUI(Random.Range(10,20)*10);
            Grow();
        }
        else if(collision.tag == "Body")
        {
            Die();
        }
        else if (collision.tag == "board")
        {
            Die();
        }



    }
}
