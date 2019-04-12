using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    private Image im;
    public Text text;
    public Text textName;
    public InputField inputName;
    public GameObject Button;
    public Button End;
    public bool change = false;
    private bool change2 = false;
    private bool change3 = false;
    private bool change4 = false;
    private bool change4_1 = true;
    private bool change5 = false;
    private Color c1, c2, c3, c4, c5;
	// Use this for initialization
	void Start () {
        im = GetComponent<Image>();
        c1 = new Color(im.color.r, im.color.g, im.color.b, 0);
        c2 = new Color(text.color.r, text.color.g, text.color.b, 0);
        c4 = new Color(textName.color.r, textName.color.g, textName.color.b, 0);
        c5 = new Color(inputName.GetComponent<Image>().color.r, inputName.GetComponent<Image>().color.g, inputName.GetComponent<Image>().color.b, 0);
        im.color = c1;
        text.color = c2;
        textName.color = c4;
        inputName.GetComponent<Image>().color = c5;
        End.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if(change)
        {
            GameStatic.SetNext(false);
            if (c1.a < 1)
            {
                c1.a += 0.01f;
                c2.a += 0.01f;
                im.color = c1;
                text.color = c2;
            }
            else
            {
                Destroy(Button);
                change = false;
                change2 = true;
            }
        }
        if (change2)
        {
            if (text.transform.localScale.y > 0)
            {
                text.transform.localScale = new Vector3(text.transform.localScale.x, text.transform.localScale.y - 0.05f, text.transform.localScale.z);
            }
            else
            {
                text.text = "Type your name";//alt+0160
                change2 = false;
                change3 = true;
            }
        }
        if (change3)
        {
            if (text.transform.localScale.y < 1)
            {
                text.transform.localScale = new Vector3(text.transform.localScale.x, text.transform.localScale.y + 0.05f, text.transform.localScale.z);
                c3.a += 0.05f;
                c4.a += 0.05f;
                c5.a += 0.05f;
                textName.color = c4;
                inputName.GetComponent<Image>().color = c5;
            }
            else
            {
                change3 = false;
            }
        }
        if (change4 && change4_1)
        {
            if (text.transform.localScale.y > 0)
            {
                text.transform.localScale = new Vector3(text.transform.localScale.x, text.transform.localScale.y - 0.05f, text.transform.localScale.z);
            }
            else
            {
                text.text = "Tap to save";//alt+0160
                change4_1 = false;
                change5 = true;
                End.enabled = true;
            }
        }
        if (change5)
        {
            if (text.transform.localScale.y < 1)
            {
                text.transform.localScale = new Vector3(text.transform.localScale.x, text.transform.localScale.y + 0.05f, text.transform.localScale.z);
            }
            else
            {
                change5 = false;
            }
        }
    }
    public void ChangeToSave()
    {
        change4 = true;
    }
    public void Save()
    {
        GameStatic.SaveScore(textName.text);
        ChangeScene.menuScore = true;
        ChangeScene.Change(0);
    }
    public void Exit()
    {
        ChangeScene.Change(0);
    }
}
