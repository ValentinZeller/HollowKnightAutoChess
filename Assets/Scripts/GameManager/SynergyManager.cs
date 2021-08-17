using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SynergyManager : MonoBehaviour
{
    //List of synergies with their data
    public List<Synergy> synergiesData;
    //Player Manager
    private PlayerManager player;
    //List of units per synergy for counting unique unit name
    private Dictionary<string, Dictionary<string, int>> unitsName = new Dictionary<string, Dictionary<string, int>>();

   
    void Start()
    {
        player = GetComponent<PlayerManager>();
        foreach (var synergy in synergiesData)
        {
            unitsName.Add(synergy.name,new Dictionary<string, int>());
        }
    }

   
    void Update()
    {
        
    }

    public void UpdateSynergyCount(string synergyName,string unitName, int count)
    {
        if (!unitsName[synergyName].ContainsKey(unitName))
        {
            unitsName[synergyName].Add(unitName,count);
            Synergy synergyData = synergiesData.Find(s => s.name == synergyName);

            if (unitsName[synergyName].Count >= synergyData.minForEffect)
            {
                switch (synergyName)
                {
                    case "crossroad":
                        Debug.Log("SYNERGY");
                        crossroadEffect(unitsName[synergyName].Count);
                        break;
                }
            }
        }
        else
        {
            unitsName[synergyName][unitName] += count;
            if (unitsName[synergyName][unitName] == 0)
            {
                unitsName[synergyName].Remove(unitName);
            }
        }
    }

    void crossroadEffect(int count)
    {
        switch (count)
        {
            case 2:
                for (int i = 0; i < player.GetBoard().childCount; i++)
                {
                    if (player.GetBoard().GetChild(i).childCount > 0)
                    {
                        Unit unit = player.GetBoard().GetChild(i).GetChild(0).GetComponent<Unit>();
                        if(unit.synergyList.Find(s => s.name == "crossroad") is Synergy synergy)
                        {
                            unit.SetMaxHealth(unit.GetMaxHeath() + unit.GetMaxHeath() * synergy.effectValue);
                        }
                    }
                }
                break;
        }
    }

    void voidEffect(int count)
    {
        switch (count)
        {
            case 1:
                for (int i = 0; i < player.GetBoard().childCount; i++)
                {
                    if (player.GetBoard().GetChild(i).childCount > 0)
                    {
                        Unit unit = player.GetBoard().GetChild(i).GetChild(0).GetComponent<Unit>();
                        if(unit.synergyList.Find(s => s.name == "void") is Synergy synergy)
                        {
                            unit.SetAttack(unit.GetAttack() + unit.GetMaxHeath() * synergy.effectValue);
                        }
                    }
                }
                break;
                
        }
    }
    
}
