﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryCounter : MonoBehaviour
{
    //This script can be attached to any gameObject that has an EnemyBase or Breakable script attached to it.
    //It ensures the EnemyBase or Breakable must recover by a certain length of time before the player can attack it again.

    //[System.NonSerialized] 

    public float recoveryTime = 3f;
    [System.NonSerialized] public float counter;
    [System.NonSerialized] public bool recovering = false;

    // Update is called once per frame
    void Update()
    {
        if(counter < recoveryTime)
        {
            counter += Time.deltaTime;
            recovering = true;
        }
        else
        {
            counter = recoveryTime;
            recovering = false;
        }
    }

    public void Recover()
    {
        counter = 0;
    }
}
