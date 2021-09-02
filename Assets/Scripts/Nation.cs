using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Nation
{

    public Laws laws;

    public List<PopType> popTypes = new List<PopType>();
    
    public string name;

    public string tribe;

    //public Color32 color;
    public byte colorr;
    public byte colorg;
    public byte colorb;

    public int taxIncome;
    public int taxTreasury;
    public int recruitsIncome;
    public int totalRecruits;
    
    public int archers;
    public int swordsmen;

    public float AttackModifier = 1;
    public float HealthModifier = 1;
    public float MovementSpeedModifier = 1;
    public float ManpowerModifier = 1;
    public float SizeModifier = 1;
    public float InfantryHealthModifier = 1;
    public float ArcherRangeModifier = 0;
    public float ChargeModifier = 1;
}
