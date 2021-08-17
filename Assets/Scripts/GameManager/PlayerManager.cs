using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Text lifeText;
    public Text moneyText;
    public Text lvlText;
    public Text xpText;
    public Text xpNeedText;
    public Text waveText;
    public GameObject buyXPButton;
    public GameObject startButton;
    [HideInInspector] public int countUnit = 0;

    [HideInInspector]  public int wave = 0;
    int life = 10;
    int money = 200;
    int lvl = 1;
    int xp = 0;
    int xpNeed = 1;
    Transform board;
    Transform enemy;

    private EnemyManager enemyManager;
    [HideInInspector]  public bool isVictory = false;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLife(0);
        UpdateLvl(0);
        UpdateMoney(0);
        UpdateXpNeeded(0);
        UpdateXp(0);
        UpdateWave();
        board = GameObject.Find("Board").transform;
        enemy = GameObject.Find("Enemy").transform;
        enemyManager = GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerAttack()
    {
        startButton.SetActive(false);
        SetBoardActive(false);
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        while (!HasLose(enemy) && !HasLose(board))
        {
            StartCoroutine(AttackBoard(board, enemy));
            yield return new WaitForSeconds(1);
            if (!HasLose(enemy))
            {
                StartCoroutine(AttackBoard(enemy, board));
                yield return new WaitForSeconds(1);
            }
        }
        if (HasLose(enemy))
        {
            if (wave < enemyManager.waves.levelWaves.Length)
            {
                UpdateWave();
                enemyManager.ClearWave();
                enemyManager.SpawnWave(enemyManager.waves.levelWaves[wave-1]);
                UpdateMoney(5);
            }
            else
            {
                isVictory = true;
            }
        }
        else if (HasLose(board))
        {
            RespawnAll(enemy);
        }
        UpdateXp(1);
        GetComponent<StoreManager>().ResetUnit();
        RespawnAll(board);
        SetBoardActive(true);
        startButton.SetActive(true);
    }

    IEnumerator AttackBoard(Transform boardT, Transform boardAttacked)
    {
        var i = 0;
        while (i < boardT.childCount && !HasLose(boardAttacked))
        {
            if (boardT.GetChild(i).childCount == 1)
            {
                Unit unit = boardT.GetChild(i).GetChild(0).GetComponent<Unit>();
                if (unit.isAlive)
                {
                    unit.Attack(boardAttacked);
                    yield return new WaitForSeconds(1);
                }
            }

            i++;
        }
    }
    void RespawnAll(Transform boardT)
    {
        for(int i = 0;i < boardT.childCount;i++)
        {
            if (boardT.GetChild(i).childCount == 1)
            {
                boardT.GetChild(i).GetChild(0).GetComponent<Unit>().Respawn();
            }
        }
    }

    public bool HasLose(Transform boardT)
    {
        bool lose = true;
        int unitIndex = 0;
        while (lose && unitIndex < boardT.childCount)
        {
            if (boardT.GetChild(unitIndex).childCount == 1)
            {
                if (boardT.GetChild(unitIndex).GetChild(0).GetComponent<Unit>().isAlive)
                {
                    lose = false;
                } else
                {
                    unitIndex++;
                }
            } else
            {
                unitIndex++;
            }
        }
        return lose;
    }

    void SetBoardActive(bool state)
    {
        for (int i = 0; i < board.childCount; i++)
        {
            if (board.GetChild(i).childCount == 1)
            {
                board.GetChild(i).GetComponent<Slot>().enabled = state;
                board.GetChild(i).GetChild(0).GetComponent<DragHandler>().enabled = state;
            }
        }
    }

    public Transform GetBoard()
    {
        return board;
    }

    public int GetMoney()
    {
        return money;
    }

    public int GetLevel()
    {
        return lvl;
    }

    public void UpdateWave()
    {
        wave++;
        waveText.text = wave.ToString();
    }
    public void UpdateLife(int lifeToAdd)
    {
        life -= lifeToAdd;
        lifeText.text = life.ToString();
    }
    public void UpdateMoney(int moneyToAdd)
    {
        money += moneyToAdd;
        moneyText.text = money.ToString();
    }
    public void UpdateXp(int xpToAdd)
    {
        xp += xpToAdd;
        xpText.text = xp.ToString();
        LevelUp();
    }
    public void UpdateXpNeeded(int xpNeedToAdd)
    {
        xpNeed += xpNeedToAdd;
        xpNeedText.text = xpNeed.ToString();
    }
    public void UpdateLvl(int lvlToAdd)
    {
        lvl += lvlToAdd;
        lvlText.text = lvl.ToString();
    }
    public void BuyXP()
    {
        if (money >= 5)
        {
            UpdateMoney(-5);
            UpdateXp(4);
        }
    }

    public void LevelUp()
    {
        while (xp >= xpNeed)
        {
            UpdateXp(-xpNeed);
            UpdateLvl(1);
            switch (lvl)
            {
                case 3:
                    UpdateXpNeeded(1);
                    break;
                case 4:
                    UpdateXpNeeded(2);
                    break;
                case 5:
                    UpdateXpNeeded(4);
                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                    UpdateXpNeeded(8);
                    break;
                case 10:
                    Destroy(xpText);
                    Destroy(xpNeedText);

                    Destroy(buyXPButton);
                    break;
            }
        }
    }
}
