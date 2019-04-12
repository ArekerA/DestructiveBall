using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using UnityEngine.SceneManagement;

public class Brick : MonoBehaviour {
    [Header("Brick Settings")]
    [Tooltip("How many times ball needs to hit the brick")]
    public int strength = 1;
    private int maxStrength;
    [Header("Brick Sprites")]
    public Sprite basicBrick;
    public Sprite brick11;
    public Sprite brick12;
    public Sprite brick13;
    public Sprite brick14;
    [Header("Particle Generator")]
    [Tooltip("Particle Generator prefab")]
    public GameObject particle;
    public bool scoring = true;
    public GameObject[] bonuses = new GameObject[8];

    public bool moving = false;
    public bool rotating = false;
    public float movingSpeed = 0f;
    public Vector2 movingDir = new Vector2(0, 0);

    public float movingDistance = 0f;
    public float movingRadius = 0f;
    public float movingStartAngle = 0f;
    public enum Mov
    {
        Line = 0,
        Circle = 1
    }
    public Mov type;
    private float counter = 0;
    private Vector3 position;

    private bool d = false;
    void Start () {
        maxStrength = strength;
        position = transform.position;
    }
    void FixedUpdate()
    {
        if(moving)
        {
            counter += movingSpeed;
            if ((int)type == 0)
            {
                transform.position = position + new Vector3(Mathf.Sin(counter*Mathf.PI/180)*movingDistance*movingDir.normalized.x, Mathf.Sin(counter * Mathf.PI / 180) * movingDistance * movingDir.normalized.y, 0);
            }
            else if ((int)type == 1)
            {
                transform.position = position + new Vector3(Mathf.Cos((counter + movingStartAngle) * Mathf.PI / 180) * movingRadius, Mathf.Sin((counter + movingStartAngle) * Mathf.PI / 180) * movingRadius, 0);
                if(rotating)
                    transform.RotateAround(position, movingSpeed>0?Vector3.forward:Vector3.back, Vector3.Angle(position - transform.position, transform.right));
            }
            if (counter >= 360)
                counter -= 360;
            else if (counter <= -360)
                counter += 360;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        strength--;
        if (strength <= 0)
        {
            Particle();
            SpawnBonus();

            Destroy(gameObject, .05f);
        }
        else
        {
            ChangeSprite();
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ball" && col.gameObject.GetComponent<Ball>().IsFireBall())
        {
            strength = 0;
            Particle();
            SpawnBonus();
            col.gameObject.GetComponent<Ball>().PlayAudio();
            Destroy(gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ball" && col.gameObject.GetComponent<Ball>().IsExplodeBall() && !d)
        {
            d = true;
            strength--;
            if (strength <= 0)
            {
                Particle();
                SpawnBonus();

                Destroy(gameObject);
            }
            else
            {
                ChangeSprite();
            }
        }
    }
    void OnDestroy()
    {
        if(scoring)
            GameStatic.AddScore();
        if (GameStatic.CheckScore())
        {
            GameStatic.ZeroScore();
            if (!GameObject.FindGameObjectWithTag("GameOver").GetComponent<GameOver>().change && SceneManager.GetActiveScene().buildIndex + 1 >= GameStatic.levels)
            {
                SceneManager.LoadScene(1);
            }
            else if (!GameObject.FindGameObjectWithTag("GameOver").GetComponent<GameOver>().change && SceneManager.GetActiveScene().buildIndex + 1 < GameStatic.levels)
            {
                GameStatic.SetNext(true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
                GameObject.FindGameObjectWithTag("GameOver").GetComponent<GameOver>().change = true;
        }
    }
    private void Particle()
    {
        GameObject newObject = Instantiate(particle, transform.position, transform.rotation) as GameObject;
        newObject.GetComponent<ParticleGenerator>().color = GetComponent<SpriteRenderer>().color;
    }
    private void ChangeSprite()
    {
        switch (4-4*strength/maxStrength)
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = basicBrick;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = brick11;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = brick12;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = brick13;
                break;
            case 4:
                GetComponent<SpriteRenderer>().sprite = brick14;
                break;
            default:
                break;
        }
    }
    private void SpawnBonus()
    {
        //GameObject newObject5 = Instantiate(bonuses[7], transform.position, transform.rotation) as GameObject;
        //if (Random.value > 1)
        if (Random.value < 0.12)
        {
            float x = Random.value;
            if (x < 0.2)
            {
                GameObject newObject2 = Instantiate(bonuses[0], transform.position, new Quaternion(0,0,0,1)) as GameObject;
            }
            else if (x < 0.4)
            {
                GameObject newObject2 = Instantiate(bonuses[1], transform.position, new Quaternion(0, 0, 0, 1)) as GameObject;
            }
            else if (x < 0.5)
            {
                GameObject newObject2 = Instantiate(bonuses[2], transform.position, new Quaternion(0, 0, 0, 1)) as GameObject;
            }
            else if (x < 0.55)
            {
                GameObject newObject2 = Instantiate(bonuses[3], transform.position, new Quaternion(0, 0, 0, 1)) as GameObject;
            }
            else if (x < 0.65)
            {
                GameObject newObject2 = Instantiate(bonuses[4], transform.position, new Quaternion(0, 0, 0, 1)) as GameObject;
            }
            else if (x < 0.8)
            {
                GameObject newObject2 = Instantiate(bonuses[5], transform.position, new Quaternion(0, 0, 0, 1)) as GameObject;
            }
            else if (x < 0.9)
            {
                GameObject newObject2 = Instantiate(bonuses[6], transform.position, new Quaternion(0, 0, 0, 1)) as GameObject;
            }
            else
            {
                GameObject newObject2 = Instantiate(bonuses[7], transform.position, new Quaternion(0, 0, 0, 1)) as GameObject;
            }
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(Brick))]
public class MyScriptEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var brick = target as Brick;
        EditorGUILayout.PrefixLabel("Strenght");
        brick.strength = EditorGUILayout.IntSlider(brick.strength, 0, 20);
        EditorGUILayout.PrefixLabel("Brick Sprites");
        EditorGUI.indentLevel++;
        brick.basicBrick = (Sprite)EditorGUILayout.ObjectField("Basic Brick", brick.basicBrick, typeof(Sprite), true);
        brick.brick11 = (Sprite)EditorGUILayout.ObjectField("Broken Brick 1",brick.brick11, typeof(Sprite), true);
        brick.brick12 = (Sprite)EditorGUILayout.ObjectField("Broken Brick 2",brick.brick12, typeof(Sprite), true);
        brick.brick13 = (Sprite)EditorGUILayout.ObjectField("Broken Brick 3",brick.brick13, typeof(Sprite), true);
        brick.brick14 = (Sprite)EditorGUILayout.ObjectField("Broken Brick 4",brick.brick14, typeof(Sprite), true);
        EditorGUI.indentLevel--;
        brick.particle = (GameObject)EditorGUILayout.ObjectField("Particle Object", brick.particle, typeof(GameObject), true);
        brick.scoring = EditorGUILayout.Toggle("Scoring", brick.scoring);
        EditorGUILayout.PrefixLabel("Bonus Object");
        EditorGUI.indentLevel++;
        brick.bonuses[0] = (GameObject)EditorGUILayout.ObjectField("Bonus 1", brick.bonuses[0], typeof(GameObject), true);
        brick.bonuses[1] = (GameObject)EditorGUILayout.ObjectField("Bonus 2", brick.bonuses[1], typeof(GameObject), true);
        brick.bonuses[2] = (GameObject)EditorGUILayout.ObjectField("Bonus 3", brick.bonuses[2], typeof(GameObject), true);
        brick.bonuses[3] = (GameObject)EditorGUILayout.ObjectField("Bonus 4", brick.bonuses[3], typeof(GameObject), true);
        brick.bonuses[4] = (GameObject)EditorGUILayout.ObjectField("Bonus 5", brick.bonuses[4], typeof(GameObject), true);
        brick.bonuses[5] = (GameObject)EditorGUILayout.ObjectField("Bonus 6", brick.bonuses[5], typeof(GameObject), true);
        brick.bonuses[6] = (GameObject)EditorGUILayout.ObjectField("Bonus 7", brick.bonuses[6], typeof(GameObject), true);
        brick.bonuses[7] = (GameObject)EditorGUILayout.ObjectField("Bonus 8", brick.bonuses[7], typeof(GameObject), true);
        EditorGUI.indentLevel--;
        brick.moving = EditorGUILayout.Toggle("Moving", brick.moving);

        using (var group = new EditorGUILayout.FadeGroupScope(brick.moving?1f:0f))
        {
            if (group.visible == true)
            {
                EditorGUI.indentLevel++;
                brick.type = (Brick.Mov)EditorGUILayout.EnumPopup("Type:", brick.type);
                EditorGUI.indentLevel++;
                EditorGUILayout.PrefixLabel("Speed");
                brick.movingSpeed = EditorGUILayout.Slider(brick.movingSpeed, -10f, 10f);
                using (var group2 = new EditorGUILayout.FadeGroupScope((int)brick.type == 0 ? 1f : 0f))
                {
                    if (group2.visible == true)
                    {
                        EditorGUILayout.PrefixLabel("Moving Distance");
                        brick.movingDistance = EditorGUILayout.Slider(brick.movingDistance, 0f, 10f);
                        EditorGUILayout.PrefixLabel("Moving Direction");
                        brick.movingDir = EditorGUILayout.Vector2Field("", brick.movingDir);
                    }
                }
                using (var group3 = new EditorGUILayout.FadeGroupScope((int)brick.type == 1 ? 1f : 0f))
                {
                    if (group3.visible == true)
                    {
                        EditorGUILayout.PrefixLabel("Radius");
                        brick.movingRadius = EditorGUILayout.Slider(brick.movingRadius, 0f, 10f);
                        EditorGUILayout.PrefixLabel("Start Angle");
                        brick.movingStartAngle = EditorGUILayout.Slider(brick.movingStartAngle, 0f, 360);
                        brick.rotating = EditorGUILayout.Toggle("Rotating", brick.rotating);
                    }
                }
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
    public void OnSceneGUI()
    {
        Brick brick = target as Brick;
        if (brick.moving == true)
        {
            if ((int)brick.type == 0)
            {
                Handles.DrawLine(brick.transform.position, brick.transform.position + (Vector3)brick.movingDir.normalized * brick.movingDistance);
                Handles.DrawLine(brick.transform.position, brick.transform.position - (Vector3)brick.movingDir.normalized * brick.movingDistance);
            }
            if ((int)brick.type == 1)
            {
                Handles.DrawWireDisc(brick.transform.position, Vector3.forward, brick.movingRadius);
                Handles.DrawLine(brick.transform.position, brick.transform.position + new Vector3(Mathf.Cos(brick.movingStartAngle*Mathf.PI/180), Mathf.Sin(brick.movingStartAngle * Mathf.PI / 180), 0) * brick.movingRadius);
            }
            SceneView.RepaintAll();
        }
    }
}
#endif