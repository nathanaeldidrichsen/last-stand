using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Wave", order = 1)]

public class Wave : ScriptableObject
{

    public GameObject[] enemiesToSpawn;
}
