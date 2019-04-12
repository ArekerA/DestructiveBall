using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {
    public float speed = 2f;
    public enum Bonuses : byte { Enlarge, Reduce, Disruption, Life, FireBall, Barrier, Guns, ExplodeBall }
    public Bonuses bonus;
    public GameObject barrier;
    void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * speed;

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if ((byte)bonus == 0)
            {
                col.gameObject.GetComponent<Player>().Enlarge(0.1F);
            }
            else if ((byte)bonus == 1)
            {
                col.gameObject.GetComponent<Player>().Reduce(0.1F);
            }
            else if ((byte)bonus == 2)
            {
                GameObject[] gos = GameObject.FindGameObjectsWithTag("Ball") as GameObject[];
                foreach (GameObject go in gos)
                {
                    go.GetComponent<Ball>().Disruption();
                }
            }
            else if ((byte)bonus == 3)
            {
                GameStatic.SetLifes(GameStatic.GetLifes() + 1);
            }
            else if ((byte)bonus == 4)
            {
                GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
                foreach (GameObject go in gos)
                {
                    if (go.CompareTag("Ball"))
                    {
                        go.GetComponent<Ball>().FireBall();
                    }
                }
            }
            else if ((byte)bonus == 5)
            {
                Instantiate(barrier, new Vector3(0,-3.08f,0.1f), new Quaternion(0,0,0,0));
            }
            else if ((byte)bonus == 6)
            {
                col.gameObject.GetComponent<Player>().Fire();
            }
            else if ((byte)bonus == 7)
            {
                GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
                foreach (GameObject go in gos)
                {
                    if (go.CompareTag("Ball"))
                    {
                        go.GetComponent<Ball>().ExplodingBall();
                    }
                }
            }
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Dead Zone")
        {
            Destroy(gameObject);
        }
    }
}
