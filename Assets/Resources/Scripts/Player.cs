using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float speed = 4.0f;
    public GameObject background;
    private Vector3 position;
    private Color color;
    private int score = 0, col1 = 20, col2 = 20;
    private Rigidbody2D rigidbody;
    public GameObject bullet;
    public GameObject ball;
    private bool left = false, right = false;

    void Start () {
        position.x = position.y = 0;
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        position.x = transform.position.x / 10;
        background.transform.position = position;
        /*if (Input.GetKeyDown("left"))
            Left();
        else if (Input.GetKeyDown("right"))
            Right();
        else if (!Input.GetKey("right") && !Input.GetKey("left"))
            Stop();*/
    }
    void FixedUpdate()
    {
        if (score != GameStatic.GetScore())
        {
            col1 = 20 + (180 / GameStatic.GetMaxScore())*GameStatic.GetScore();
            score = GameStatic.GetScore();
        }
        if(col1 != col2)
        {
            col2++;
            color = new Color(col2/255f, col2/255f, col2/255f);
            background.GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void Left()
    {
        left = true;
        rigidbody.velocity = new Vector2(-1, 0) * speed;
    }
    public void Right()
    {
        right = true;
        rigidbody.velocity = new Vector2(1, 0) * speed;
    }
    public void StopL()
    {
        left = false;
        Stop();
    }
    public void StopR()
    {
        right = false;
        Stop();
    }
    public void Stop()
    {
        if(!left && !right)
            rigidbody.velocity = new Vector2(0, 0);
    }
    public void Enlarge(float i)
    {
        transform.localScale += new Vector3(i, 0, 0);
    }
    public void Reduce(float i)
    {
        transform.localScale -= new Vector3(i, 0, 0);
    }
    public void Fire()
    {
        StartCoroutine(FireB());
    }
    IEnumerator FireB()
    {
        int i = 0;
        while (i < 13)
        {
            Instantiate(bullet, new Vector3(transform.position.x + (0.5f* transform.localScale.x)-0.1f, transform.position.y + 0.32f, transform.position.z), transform.rotation);
            yield return new WaitForSeconds(0.2f);
            Instantiate(bullet, new Vector3(transform.position.x - (0.5f * transform.localScale.x) + 0.1f, transform.position.y + 0.32f, transform.position.z), transform.rotation);
            yield return new WaitForSeconds(0.2f);
            ++i;
        }
    }
    public void StartBall()
    {
        ball.GetComponent<Ball>().StartMove();
    }
}
