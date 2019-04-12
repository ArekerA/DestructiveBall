using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public RectTransform menu;
    public RectTransform authors;
    public RectTransform scores;
    public RectTransform scoresContent;
    public GameObject scoresNames;
    public GameObject scoresValues;
    public RectTransform levels1;
    public RectTransform levels2;
    public PhysicsMaterial2D bounce;
    public float speed;
    private GameObject[] world1 = new GameObject[9];
    private bool rightA = false;
    private bool leftA = false;
    private bool rightS = false;
    private bool leftS = false;
    private bool rightL = false;
    private bool leftL = false;
    private bool upL = false;
    private bool downL = false;
    private float a;
    private string[] str = { "", "", "0" };
    public int[] lvl = { 1, 1 };
    void Start()
    {
        bounce.bounciness = 1;
        str = GameStatic.LoadScore();
        scoresContent.offsetMin = new Vector2(scoresContent.offsetMin.x, int.Parse(str[2]) * -45);
        scoresNames.GetComponent<RectTransform>().offsetMin = new Vector2(scoresNames.GetComponent<RectTransform>().offsetMin.x, int.Parse(str[2]) * -45);
        scoresValues.GetComponent<RectTransform>().offsetMin = new Vector2(scoresValues.GetComponent<RectTransform>().offsetMin.x, int.Parse(str[2]) * -45);
        scoresNames.GetComponent<Text>().text = str[0];
        scoresValues.GetComponent<Text>().text = str[1];
    }
    public void FixedUpdate()
    {
        if (leftA)
        {
            menu.offsetMin = new Vector2(menu.offsetMin.x - 0.3f, menu.offsetMin.y);
            menu.offsetMax = new Vector2(menu.offsetMax.x - 0.3f, menu.offsetMax.y);
            authors.offsetMin = new Vector2(authors.offsetMin.x - 0.3f, authors.offsetMin.y);
            authors.offsetMax = new Vector2(authors.offsetMax.x - 0.3f, authors.offsetMax.y);
            if (menu.offsetMin.x > -768)
            {
                a = speed * Mathf.Sin((menu.offsetMin.x) / 768 * Mathf.PI);
                menu.offsetMin = new Vector2(menu.offsetMin.x + a, menu.offsetMin.y);
                menu.offsetMax = new Vector2(menu.offsetMax.x + a, menu.offsetMax.y);
                authors.offsetMin = new Vector2(authors.offsetMin.x + a, authors.offsetMin.y);
                authors.offsetMax = new Vector2(authors.offsetMax.x + a, authors.offsetMax.y);
            }
            else
            {
                leftA = false;
            }
        }
        if (rightA)
        {
            menu.offsetMin = new Vector2(menu.offsetMin.x + 0.3f, menu.offsetMin.y);
            menu.offsetMax = new Vector2(menu.offsetMax.x + 0.3f, menu.offsetMax.y);
            authors.offsetMin = new Vector2(authors.offsetMin.x + 0.3f, authors.offsetMin.y);
            authors.offsetMax = new Vector2(authors.offsetMax.x + 0.3f, authors.offsetMax.y);
            if (menu.offsetMin.x < 0)
            {
                a = speed * Mathf.Sin((menu.offsetMin.x) / 768 * Mathf.PI);
                menu.offsetMin = new Vector2(menu.offsetMin.x - a, menu.offsetMin.y);
                menu.offsetMax = new Vector2(menu.offsetMax.x - a, menu.offsetMax.y);
                authors.offsetMin = new Vector2(authors.offsetMin.x - a, authors.offsetMin.y);
                authors.offsetMax = new Vector2(authors.offsetMax.x - a, authors.offsetMax.y);
            }
            else
            {
                rightA = false;
            }
        }
        if (leftL)
        {
            levels1.offsetMin = new Vector2(levels1.offsetMin.x - 0.3f, levels1.offsetMin.y);
            levels1.offsetMax = new Vector2(levels1.offsetMax.x - 0.3f, levels1.offsetMax.y);
            levels2.offsetMin = new Vector2(levels2.offsetMin.x - 0.3f, levels2.offsetMin.y);
            levels2.offsetMax = new Vector2(levels2.offsetMax.x - 0.3f, levels2.offsetMax.y);
            if (levels1.offsetMin.x > -768)
            {
                a = speed * Mathf.Sin((levels1.offsetMin.x) / 768 * Mathf.PI);
                levels1.offsetMin = new Vector2(levels1.offsetMin.x + a, levels1.offsetMin.y);
                levels1.offsetMax = new Vector2(levels1.offsetMax.x + a, levels1.offsetMax.y);
                levels2.offsetMin = new Vector2(levels2.offsetMin.x + a, levels2.offsetMin.y);
                levels2.offsetMax = new Vector2(levels2.offsetMax.x + a, levels2.offsetMax.y);
            }
            else
            {
                leftL = false;
            }
        }
        if (rightL)
        {
            levels1.offsetMin = new Vector2(levels1.offsetMin.x + 0.3f, levels1.offsetMin.y);
            levels1.offsetMax = new Vector2(levels1.offsetMax.x + 0.3f, levels1.offsetMax.y);
            levels2.offsetMin = new Vector2(levels2.offsetMin.x + 0.3f, levels2.offsetMin.y);
            levels2.offsetMax = new Vector2(levels2.offsetMax.x + 0.3f, levels2.offsetMax.y);
            if (levels1.offsetMin.x < 0)
            {
                a = speed * Mathf.Sin((levels1.offsetMin.x) / 768 * Mathf.PI);
                levels1.offsetMin = new Vector2(levels1.offsetMin.x - a, levels1.offsetMin.y);
                levels1.offsetMax = new Vector2(levels1.offsetMax.x - a, levels1.offsetMax.y);
                levels2.offsetMin = new Vector2(levels2.offsetMin.x - a, levels2.offsetMin.y);
                levels2.offsetMax = new Vector2(levels2.offsetMax.x - a, levels2.offsetMax.y);
            }
            else
            {
                rightL = false;
            }
        }
        if (leftS)
        {
            //Zabezpieczenie przed dziwnym błędem ( po wykonaniu funkcji element UI "Content" zwiększał strasznie szybko swoją wielkość aż do NaN)
            //Nie w końcowym warunku, ze względu na widoczne "skoki" elementu
            scoresContent.offsetMin = new Vector2(0, int.Parse(str[2]) * -45);
            scoresContent.offsetMax = new Vector2(0, 0);
            //=============
            menu.offsetMin = new Vector2(menu.offsetMin.x - 0.3f, menu.offsetMin.y);
            menu.offsetMax = new Vector2(menu.offsetMax.x - 0.3f, menu.offsetMax.y);
            scores.offsetMin = new Vector2(scores.offsetMin.x - 0.3f, scores.offsetMin.y);
            scores.offsetMax = new Vector2(scores.offsetMax.x - 0.3f, scores.offsetMax.y);
            if (menu.offsetMin.x > 0)
            {
                a = speed * Mathf.Sin(-(menu.offsetMin.x) / 768 * Mathf.PI);
                menu.offsetMin = new Vector2(menu.offsetMin.x + a, menu.offsetMin.y);
                menu.offsetMax = new Vector2(menu.offsetMax.x + a, menu.offsetMax.y);
                scores.offsetMin = new Vector2(scores.offsetMin.x + a, scores.offsetMin.y);
                scores.offsetMax = new Vector2(scores.offsetMax.x + a, scores.offsetMax.y);
            }
            else
            {
                leftS = false;
            }
        }
        if (rightS)
        {
            //Zabezpieczenie przed dziwnym błędem ( po wykonaniu funkcji element UI "Content" zwiększał strasznie szybko swoją wielkość aż do NaN)
            //Nie w końcowym warunku, ze względu na widoczne "skoki" elementu
            scoresContent.offsetMin = new Vector2(0, int.Parse(str[2]) * -45);
            scoresContent.offsetMax = new Vector2(0, 0);
            //=============
            menu.offsetMin = new Vector2(menu.offsetMin.x + 0.3f, menu.offsetMin.y);
            menu.offsetMax = new Vector2(menu.offsetMax.x + 0.3f, menu.offsetMax.y);
            scores.offsetMin = new Vector2(scores.offsetMin.x + 0.3f, scores.offsetMin.y);
            scores.offsetMax = new Vector2(scores.offsetMax.x + 0.3f, scores.offsetMax.y);
            if (menu.offsetMin.x < 768)
            {
                a = speed * Mathf.Sin(-(menu.offsetMin.x) / 768 * Mathf.PI);
                menu.offsetMin = new Vector2(menu.offsetMin.x - a, menu.offsetMin.y);
                menu.offsetMax = new Vector2(menu.offsetMax.x - a, menu.offsetMax.y);
                scores.offsetMin = new Vector2(scores.offsetMin.x - a, scores.offsetMin.y);
                scores.offsetMax = new Vector2(scores.offsetMax.x - a, scores.offsetMax.y);
            }
            else
            {
                rightS = false;
            }
        }
        if (downL)
        {
            menu.offsetMin = new Vector2(menu.offsetMin.x, menu.offsetMin.y - 0.3f);
            menu.offsetMax = new Vector2(menu.offsetMax.x, menu.offsetMax.y - 0.3f);
            levels1.offsetMin = new Vector2(levels1.offsetMin.x, levels1.offsetMin.y - 0.3f);
            levels1.offsetMax = new Vector2(levels1.offsetMax.x, levels1.offsetMax.y - 0.3f);
            levels2.offsetMin = new Vector2(levels2.offsetMin.x, levels2.offsetMin.y - 0.3f);
            levels2.offsetMax = new Vector2(levels2.offsetMax.x, levels2.offsetMax.y - 0.3f);
            if (menu.offsetMin.y > -1366)
            {
                a = speed * Mathf.Sin(menu.offsetMin.y / 1366 * Mathf.PI);
                menu.offsetMin = new Vector2(menu.offsetMin.x, menu.offsetMin.y + a);
                menu.offsetMax = new Vector2(menu.offsetMax.x, menu.offsetMax.y + a);
                levels1.offsetMin = new Vector2(levels1.offsetMin.x, levels1.offsetMin.y + a);
                levels1.offsetMax = new Vector2(levels1.offsetMax.x, levels1.offsetMax.y + a);
                levels2.offsetMin = new Vector2(levels2.offsetMin.x, levels2.offsetMin.y + a);
                levels2.offsetMax = new Vector2(levels2.offsetMax.x, levels2.offsetMax.y + a);
            }
            else
            {
                downL = false;
            }
        }
        if (upL)
        {
            menu.offsetMin = new Vector2(menu.offsetMin.x, menu.offsetMin.y + 0.3f);
            menu.offsetMax = new Vector2(menu.offsetMax.x, menu.offsetMax.y + 0.3f);
            levels1.offsetMin = new Vector2(levels1.offsetMin.x, levels1.offsetMin.y + 0.3f);
            levels1.offsetMax = new Vector2(levels1.offsetMax.x, levels1.offsetMax.y + 0.3f);
            levels2.offsetMin = new Vector2(levels2.offsetMin.x, levels2.offsetMin.y + 0.3f);
            levels2.offsetMax = new Vector2(levels2.offsetMax.x, levels2.offsetMax.y + 0.3f);
            if (menu.offsetMin.y < 1366)
            {
                a = speed * Mathf.Sin(menu.offsetMin.y / 1366 * Mathf.PI);
                menu.offsetMin = new Vector2(menu.offsetMin.x, menu.offsetMin.y - a);
                menu.offsetMax = new Vector2(menu.offsetMax.x, menu.offsetMax.y - a);
                levels1.offsetMin = new Vector2(levels1.offsetMin.x, levels1.offsetMin.y - a);
                levels1.offsetMax = new Vector2(levels1.offsetMax.x, levels1.offsetMax.y - a);
                levels2.offsetMin = new Vector2(levels2.offsetMin.x, levels2.offsetMin.y - a);
                levels2.offsetMax = new Vector2(levels2.offsetMax.x, levels2.offsetMax.y - a);
            }
            else
            {
                upL = false;
            }
        }

    }
    public void Change(int scene)
    {
        ChangeScene.Change(scene);
    }
    public void Change(string scene)
    {
        ChangeScene.Change(scene);
    }
    public void Exit()
    {
        ChangeScene.Exit();
    }
    public void LeftA()
    {
        leftA = true;
        rightA = false;
    }
    public void RightA()
    {
        rightA = true;
        leftA = false;
    }
    public void LeftS()
    {
        leftS = true;
        rightS = false;
    }
    public void RightS()
    {
        rightS = true;
        leftS = false;
    }
    public void LeftL()
    {
        leftL = true;
        rightL = false;
    }
    public void RightL()
    {
        rightL = true;
        leftL = false;
    }
    public void UpL()
    {
        upL = true;
        downL = false;
    }
    public void DownL()
    {
        upL = false;
        downL = true;
    }
}

