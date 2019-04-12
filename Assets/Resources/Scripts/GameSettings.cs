using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour
{
    [Tooltip("How many score player needs to complite level")]
    public int maxScore = 0;
    public int world;
    public int lvl;
    void Start () {
        GameStatic.Revive();
        if(!GameStatic.IsNext())
            GameStatic.ZeroAllScore();
        GameStatic.ZeroScore();
        GameStatic.SetMaxScore(maxScore);
        GameStatic.SaveUnlockedLevels(world, lvl);
    }
    public void Restart()
    {
        ChangeScene.Restart();
    }
    public void Change(int scene)
    {
        GameStatic.SetNext(true);
        ChangeScene.Change(scene);
    }
    public void Change(string scene)
    {
        GameStatic.SetNext(true);
        ChangeScene.Change(scene);
    }
    public void Exit()
    {
        ChangeScene.Exit();
    }
}
