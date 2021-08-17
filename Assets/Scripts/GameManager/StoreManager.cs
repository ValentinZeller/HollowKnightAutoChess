using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    Transform storePanel;
    public ShopUnitList storeUnits;
    private PlayerManager player;
    
    // Start is called before the first frame update
    void Start()
    {
        storePanel = GameObject.Find("Store").transform;
        player = GetComponent<PlayerManager>();
        ResetUnit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StoreReset()
    {
        if (player.GetMoney() >= 2) {
            ResetUnit();
            player.UpdateMoney(-2);
        }
    }

    public void ResetUnit()
    {
        for (int i = 0; i < storePanel.childCount; i++)
        {
            if (storePanel.GetChild(i).childCount > 0)
            {
                Destroy(storePanel.GetChild(i).GetChild(0).gameObject);
            }
            SpawnUnit(storeUnits.unitList[0], storePanel.GetChild(i));
        }
    }

    void SpawnUnit(GameObject unit, Transform slot)
    {
        int randomIndex = Random.Range(0, storeUnits.unitList.Length);
        unit = storeUnits.unitList[randomIndex];
        unit = Instantiate(unit, slot);
        unit.GetComponent<DragHandler>().enabled = false;
    }
}
