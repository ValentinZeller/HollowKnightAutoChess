using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Waves", order = 1)]
public class Waves : ScriptableObject
{
    public Wave[] levelWaves;
}
