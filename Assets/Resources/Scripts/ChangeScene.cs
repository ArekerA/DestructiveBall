using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ChangeScene{
    public static bool menuScore = false;
    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public static void Change(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public static void Change(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public static void Exit()
    {
        Application.Quit();
    }
}
