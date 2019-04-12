using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    private static int number = 0;
    [Header("Ball Settings")]
    [Tooltip("Ball speed")]
    public float speed = 4.5f;
    public Rigidbody2D player;
    public GameObject gameOver;
    [Header("Ball Sprites")]
    public Sprite basicBall;
    public Sprite fireBall;
    public Sprite explodingBall;
    private bool fire = false;
    private bool exploding = false;
    private bool explode = false;
    private static float transformY;
    private bool move = false;
    private new AudioSource audio;
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Ball").Length == 1)
            number = 0;
        if (number == 0)
            transformY = transform.position.y;
        ++number;
        Debug.Log(number);
        audio = GetComponent<AudioSource>();
    }
    void OnDestroy()
    {
        --number;
        GameStatic.SetBalls(GameStatic.GetBalls() - 1);
    }
    void Update()
    {
        Debug.Log("x:"+ GetComponent<Rigidbody2D>().velocity.x+" y:"+ GetComponent<Rigidbody2D>().velocity.y+" magnitude:"+ GetComponent<Rigidbody2D>().velocity.magnitude);
        if (!move)
            transform.localPosition = new Vector3(player.position.x, transformY, 0);
        else
        {
            if (GetComponent<Rigidbody2D>().velocity.magnitude < 2f)
            {
                GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * speed;
            }
            if (GetComponent<Rigidbody2D>().velocity.magnitude == 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.2f) * speed;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        audio.Play();
        if (col.gameObject.tag == "Brick" && exploding)
        {
            explode = true;
            StartCoroutine(OffExplodeBall());
        }
        if (col.gameObject.tag == "Player")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2((transform.position.x - col.gameObject.transform.position.x) / col.gameObject.transform.lossyScale.x, 1).normalized * speed;
        }
        else if(col.gameObject.tag == "Dead Zone")
        {
            Debug.Log(number + ", " + GameStatic.GetBalls());
            if (GameStatic.GetLifes()-1 <= 0 && number <= 1)
            {
                Debug.Log("koniec");
                GameStatic.SetLifes(GameStatic.GetLifes() - 1);
                gameOver.GetComponent<GameOver>().change = true;
                Destroy(gameObject);
                return;
            }
            else
            {
                if (number > 1)
                {
                    Destroy(gameObject);
                    return;
                }
                else
                {
                    GameStatic.SetLifes(GameStatic.GetLifes() - 1);
                    transform.localPosition = new Vector3(player.position.x, transformY, 0);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    GameObject go = GameObject.FindGameObjectWithTag("Player");
                    go.GetComponent<Player>().ball = gameObject;
                    move = false;
                }
            }
        }
        if (GetComponent<Rigidbody2D>().velocity.y < 0.2f && GetComponent<Rigidbody2D>().velocity.y >= 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.2f) * speed;
        }
        else if (GetComponent<Rigidbody2D>().velocity.y > -0.2f && GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.2f) * speed;
        }
        if (GetComponent<Rigidbody2D>().velocity.magnitude < 4f && move)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * speed;
        }
        if (!move)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
    public void PlayAudio()
    {
        audio.Play();
    }
    public void StartMove()
    {
        if (!move)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.value - .5f, 1).normalized * speed;
            move = true;
        }
    }
    public void Disruption()
    {
        if (number < 7)
        {
            GameObject newObject = Instantiate(gameObject, transform.position, transform.rotation) as GameObject;
            GameObject newObject2 = Instantiate(gameObject, transform.position, transform.rotation) as GameObject;
            newObject.GetComponent<Ball>().StartMove();
            newObject2.GetComponent<Ball>().StartMove();
            GameStatic.SetBalls((byte)(GameStatic.GetBalls() + 2));
        }
    }
    public void FireBall()
    {
        fire = true;
        exploding = false;
        GetComponent<TrailRenderer>().startColor = new Color(1f,.3f,0f);
        GetComponent<SpriteRenderer>().sprite = fireBall;
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(OffFireBall());
    }
    public void ExplodingBall()
    {
        fire = false;
        exploding = true;
        GetComponent<TrailRenderer>().startColor = new Color(.3f, .3f, .3f);
        GetComponent<SpriteRenderer>().sprite = explodingBall;
        GetComponent<CircleCollider2D>().enabled = true; 
        StartCoroutine(OffExplodingBall());
    }
    public bool IsFireBall()
    {
        return fire;
    }
    public bool IsExplodingBall()
    {
        return exploding;
    }
    public bool IsExplodeBall()
    {
        return explode;
    }
    IEnumerator OffFireBall()
    {
        yield return new WaitForSeconds(3);
        if (exploding == false)
        {
            GetComponent<SpriteRenderer>().sprite = basicBall;
            GetComponent<TrailRenderer>().startColor = new Color(1f, 1f, 1f);
        }
        fire = false;
    }
    IEnumerator OffExplodingBall()
    {
        yield return new WaitForSeconds(10);
        if (fire == false)
        {
            GetComponent<SpriteRenderer>().sprite = basicBall;
            GetComponent<TrailRenderer>().startColor = new Color(1f, 1f, 1f);
        }
        GetComponent<CircleCollider2D>().enabled = false;
        exploding = false;
    }
    IEnumerator OffExplodeBall()
    {
        yield return new WaitForSeconds(0.02f);
        explode = false;
    }
}
