using UnityEngine;

public class Brick : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] states;
    public int health { get; private set; }
    public bool unbreakable;
    
    public Score score ;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (!this.unbreakable)
        {
            this.health = this.states.Length;
            this.spriteRenderer.sprite = this.states[this.health - 1];
        }
        this.score = FindObjectOfType<Score>();
    }

    private void Hit()
    { 
        if (this.unbreakable) {
            return;
        }

        this.health--;

        if (this.health <= 0) {
            this.gameObject.SetActive(false);
            this.score.score += 1;
        } else {
            this.spriteRenderer.sprite = this.states[this.health - 1];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball") {
            Hit();
        }
    }

}