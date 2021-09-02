using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Country
{
    public string name;

    public enum theTribes{
        BURGUNDIANS,    
        FRANKS,
        GOTHS,
        POLISH,
        PLAYER
    }

    public theTribes tribe;

    public Pops pops;

    //public int totalPopulation;
    public float growthPopulation = 0.1f;
    public float recruitablePopulation = 0.1f;
    public int recruits = 100;
    public float taxRate = 0.01f;
    public int taxIncome = 100;
    
    public int archers = 10;
    public int swordsmen = 10;

    public int militia = 0;
    public int fortifications = 0;

    public float AttackModifier = 1;
    public float HealthModifier = 1;
}
