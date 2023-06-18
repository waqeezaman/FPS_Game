using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFunctions : CharacterFunctions
{
    [SerializeField]
    private float SpawnChance;


    public bool WillSpawn()
    {
        float r = Random.value;
        if (r < SpawnChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
