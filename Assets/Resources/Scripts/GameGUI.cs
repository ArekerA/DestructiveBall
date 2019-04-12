using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour
{
    public Text score;
    public Text lives;
    void Start()
    {
    }
    void Update()
    {
        score.text = "Score: " + GameStatic.GetAllScore();
        lives.text = "Lifes: " + GameStatic.GetLifes();
    }
}
