using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    private float maxHealth;
    [SerializeField] float health = 0;
    [SerializeField] float attack = 0;
    [SerializeField] int tier = 0;
    [SerializeField] private int lvl = 1;
    
    [HideInInspector] public bool isAlive = true;
    public Slider healthSlider;
    public List<Synergy> synergyList;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetAttack()
    {
        return attack;
    }

    public float GetMaxHeath()
    {
        return maxHealth;
    }

    public void SetMaxHealth(float maxhealth)
    {
        maxHealth = maxhealth;
        health = maxhealth;
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetAttack(float newAttack)
    {
        attack = newAttack;
    }

    public void AddHealth(float healthToRemove)
    {
        health += healthToRemove;
        healthSlider.value = health;
    }

    public int GetTier()
    {
        return tier;
    }

    public int GetLvl()
    {
        return lvl;
    }

    public void LevelUp()
    { 
        SetMaxHealth(health + Mathf.Round(maxHealth/2));
        SetAttack(attack + (Mathf.Round(attack / 2)));
        float colorOffset = 0.2f;
        lvl++;
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r - colorOffset ,GetComponent<Image>().color.g - colorOffset,GetComponent<Image>().color.b - colorOffset);
    }

    void IsDead()
    {
        if (health <= 0)
        {
            isAlive = false;
            GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    public void Respawn()
    {
        health = maxHealth;
        healthSlider.value = health;
        if (!isAlive)
        {
            GetComponent<CanvasGroup>().alpha = 1;
            isAlive = true;
        }
    }

    public void Attack(Transform boardEnemy)
    {
        
        bool hasFound = false;
        int childIndex = 0;
        while (!hasFound && (childIndex < boardEnemy.childCount))
        {
            if (boardEnemy.GetChild(childIndex).childCount == 1)
            {
                if (boardEnemy.GetChild(childIndex).GetChild(0).GetComponent<Unit>().isAlive)
                {
                    boardEnemy.GetChild(childIndex).GetChild(0).GetComponent<Unit>().AddHealth(-attack);
                    boardEnemy.GetChild(childIndex).GetChild(0).GetComponent<Unit>().IsDead();
                    hasFound = true;
                }
                else
                {
                    childIndex++;
                }
            }
            else
            {
                childIndex++;
            }
        }
    }
}

