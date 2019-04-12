using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            Destroy(gameObject, 0.05f);
        }
    }
}