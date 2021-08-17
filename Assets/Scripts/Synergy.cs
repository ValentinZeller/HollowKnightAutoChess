using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Synergy", order = 2)] 
public class Synergy : ScriptableObject
{
    public string description;
    public float effectValue;
    public float minForEffect;
}