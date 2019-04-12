using UnityEngine;
using System.Collections;

public class ParticleGenerator : MonoBehaviour {
    public Object prefab;
    [HideInInspector]
    public Color color;
    // Use this for initialization
    void Start () {
        GameObject newObject = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
        newObject.GetComponent<ParticleElement>().x = Random.value * .5f;
        newObject.GetComponent<ParticleElement>().y = 1;
        newObject.GetComponent<ParticleElement>().color = color;
        newObject = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
        newObject.GetComponent<ParticleElement>().x = -Random.value * .5f;
        newObject.GetComponent<ParticleElement>().y = 1;
        newObject.GetComponent<ParticleElement>().color = color;
        newObject = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
        newObject.GetComponent<ParticleElement>().x = -Random.value * .5f;
        newObject.GetComponent<ParticleElement>().y = -1;
        newObject.GetComponent<ParticleElement>().color = color;
        newObject = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
        newObject.GetComponent<ParticleElement>().x = Random.value * .5f;
        newObject.GetComponent<ParticleElement>().y = -1;
        newObject.GetComponent<ParticleElement>().color = color;
        newObject = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
        newObject.GetComponent<ParticleElement>().x = 1;
        newObject.GetComponent<ParticleElement>().y = Random.value * .5f;
        newObject.GetComponent<ParticleElement>().color = color;
        newObject = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
        newObject.GetComponent<ParticleElement>().x = 1;
        newObject.GetComponent<ParticleElement>().y = -Random.value * .5f;
        newObject.GetComponent<ParticleElement>().color = color;
        newObject = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
        newObject.GetComponent<ParticleElement>().x = -1;
        newObject.GetComponent<ParticleElement>().y = Random.value * .5f;
        newObject.GetComponent<ParticleElement>().color = color;
        newObject = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
        newObject.GetComponent<ParticleElement>().x = -1;
        newObject.GetComponent<ParticleElement>().y = -Random.value * .5f;
        newObject.GetComponent<ParticleElement>().color = color;
        Destroy(gameObject, 1);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
