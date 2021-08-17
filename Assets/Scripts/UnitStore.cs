using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStore : MonoBehaviour
{
    Transform inventoryPanel;
    PlayerManager player;
    Unit unitData;
    private List<GameObject> instanceList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnMouseDown);
        inventoryPanel = GameObject.Find("Inventory").transform;
        player = GameObject.Find("GameManager").GetComponent<PlayerManager>();
        unitData = GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        int childIndex = 0;
        bool isPlaced = false;
        if (player.GetMoney() >= unitData.GetTier())
        {
            if (CountUnit(1,inventoryPanel) >= 2)
            {
                isPlaced = true;
                Destroy(instanceList[0]);
                instanceList[1].GetComponent<Unit>().LevelUp();
                instanceList.Clear();
                if (CountUnit(2, inventoryPanel) > 2)
                {
                    Destroy(instanceList[2]);
                    Destroy(instanceList[1]);
                    instanceList[0].GetComponent<Unit>().LevelUp();
                    instanceList.Clear();
                }
                Destroy(gameObject);
            }
            while (childIndex < inventoryPanel.childCount && !isPlaced)
            {
                if (inventoryPanel.GetChild(childIndex).childCount == 0)
                {
                    isPlaced = true;
                    GameObject unit = Instantiate(gameObject, inventoryPanel.GetChild(childIndex));
                    player.UpdateMoney(-unitData.GetTier());
                    unit.GetComponent<UnitStore>().enabled = false;
                    unit.GetComponent<DragHandler>().enabled = true;
                    Destroy(gameObject);
                }
                else
                {
                    childIndex++;
                }
            }
        }
    }

    private int CountUnit(int lvl,Transform boardT)
    {
        int count = 0;
        for (int i = 0; i < boardT.childCount; i++)
        {
            if (boardT.GetChild(i).childCount > 0)
            {
                GameObject sameUnit = boardT.GetChild(i).GetChild(0).gameObject;
                if (sameUnit.name.Contains(gameObject.name) && sameUnit.GetComponent<Unit>().GetLvl() == lvl)
                {
                    count++;
                    instanceList.Add(sameUnit);
                }
            }
        }

        if (count < 2 && boardT.name != "Board")
        {
            count += CountUnit(lvl, player.GetBoard());
        }
        return count;
    }
}
