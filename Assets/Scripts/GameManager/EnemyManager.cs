using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [HideInInspector] public Transform enemyPanel;

    [HideInInspector] public bool isAttacking;
    PlayerManager player;

    public Waves waves;
    

    // Start is called before the first frame update
    void Start()
    {
        enemyPanel = GameObject.Find("Enemy").transform;
        player = GetComponent<PlayerManager>();
        SpawnWave(waves.levelWaves[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnWave(Wave wave)
    {
        for (int i =0;i < wave.units.Length;i++)
        {
            SpawnUnit(wave.units[i].prefab, enemyPanel.GetChild(i), wave.units[i].lvl);
        }
    }

    void SpawnUnit(GameObject unit, Transform slot, int lvl)
    {
        var instance = Instantiate(unit, slot);
        if (lvl > 1)
        {
            for (var i = 0; i < lvl - 1; i++)
            {
                instance.GetComponent<Unit>().LevelUp();
            }
        }
    }

    public void ClearWave()
    {
        for (int i = 0; i < 2; i++)
        {
            if (enemyPanel.GetChild(i).childCount > 0)
            {
                Destroy(enemyPanel.GetChild(i).GetChild(0).gameObject);
            }
        }
    }
}
