using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class Wave
{
    public UnitData[] units;
}

[Serializable] public class UnitData
{
    public GameObject prefab;
    [Range(1,3)]
    public int lvl;
}

