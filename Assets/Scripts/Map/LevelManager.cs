using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private string currentLevel;
    public string[] mapList;
    
    public void LoadLevel(string levelName)
    {
        currentLevel = levelName;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Assets/Scenes/"+levelName+".unity");
    }
    
    public string GetMap()
    {
        string map = "Main";
        for (int i = 0; i < mapList.Length; i++)
        {
            if (currentLevel.Contains(mapList[i]))
            {
                map = mapList[i];
            }
        }
        return map+"Map";
    }

    public void LoadMap()
    {
        LoadLevel(GetMap());
    }
}
