using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [Tooltip("Bullet speed")]
    public float speed = 10f;
    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
