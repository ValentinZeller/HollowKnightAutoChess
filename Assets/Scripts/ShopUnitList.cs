using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ShopUnitList", order = 3)]
public class ShopUnitList : ScriptableObject
{
    public string description;
    public GameObject[] unitList;
}