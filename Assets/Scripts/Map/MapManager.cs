using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MapManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int levelcomplete = 0;
    private LevelManager levelManager;
    void Start()
    {
        levelManager = GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManager"))
        {
            PlayerManager player = GameObject.Find("GameManager").GetComponent<PlayerManager>();
            if (player.isVictory)
            {
                player.isVictory = false;
                SetComplete(1);
                levelManager.LoadMap();
            }
        }

        if (SceneManager.GetActiveScene().name == "MainMap")
        {
            UpdateMap();
        }
    }

    public void SetComplete(int number)
    {
        levelcomplete = number;
        UpdateMap();
    }
    private void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("System"));
    }

    public bool IsComplete(int number)
    {
        bool comp = number >= levelcomplete;
        return comp;
    }

    public void UpdateMap()
    {
        if (SceneManager.GetActiveScene().name == "MainMap" && levelcomplete == 1)
        {
            GameObject.Find("ButtonCrossroads").GetComponent<Button>().interactable = true;
            GameObject.Find("CrossroadsButton").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        }
    }
}
