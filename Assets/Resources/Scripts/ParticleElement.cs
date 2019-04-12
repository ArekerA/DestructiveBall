using UnityEngine;
using System.Collections;

public class ParticleElement : MonoBehaviour {

    [Header("Element Settings")]
    [Tooltip("Element speed")]
    public float speed = 6f;
    [Tooltip("Element lifetime")]
    public float lifetime = .5f;
    [HideInInspector]
    public float x;
    [HideInInspector]
    public float y;
    [HideInInspector]
    public Color color;
    // Use this for initialization
    void Start () {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = color;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y).normalized * speed;
        Destroy(gameObject,lifetime);
    }
}
