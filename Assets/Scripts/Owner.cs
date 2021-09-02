using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner : MonoBehaviour
{
    public float AttackModifier = 1;
    public float HealthModifier = 1;
    public float MovementSpeedModifier = 1;
    public float ManpowerModifier = 1;
    public float SizeModifier = 1;
    public float InfantryHealthModifier = 1;
    public float ArcherRangeModifier = 1;
    public float ChargeModifier = 1;
    //public int layer;
    //public int enemyLayer;
    public int Angles;
    public string Tag;
    public string EnemyTag;
    public Color32 Color;
    public int Swordsmen;
    public int Archers;
    public string Nation;
    public bool Player;
    public float detectRange = 100f;

    void Awake()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        if (Player == false)
        {
            foreach(GameObject nations in theArray)
            {
                if(GameManager.instance.attackedNation == nations.GetComponent<NationHandler>().nation.tribe.ToString())
                {
                    Swordsmen = nations.GetComponent<NationHandler>().nation.swordsmen;
                    Archers = nations.GetComponent<NationHandler>().nation.archers;
                    AttackModifier = nations.GetComponent<NationHandler>().nation.AttackModifier;
                    HealthModifier = nations.GetComponent<NationHandler>().nation.HealthModifier;
                    SizeModifier = nations.GetComponent<NationHandler>().nation.SizeModifier;
                    InfantryHealthModifier = nations.GetComponent<NationHandler>().nation.InfantryHealthModifier;
                    ArcherRangeModifier = nations.GetComponent<NationHandler>().nation.ArcherRangeModifier;
                    ChargeModifier = nations.GetComponent<NationHandler>().nation.ChargeModifier;
                    Nation = nations.GetComponent<NationHandler>().nation.name;
                    Color = GameManager.instance.attackedNationcolor; 
                    // if(GameManager.instance.attackedCountryFortifications > 0)
                    // {
                    //     detectRange = 13f;
                    // }
                    print(nations.GetComponent<NationHandler>().nation.name + " defends");
                }
                
            }
        }
        else if (Player == true)
        {
            foreach(GameObject nations in theArray)
            {
                if(GameManager.instance.attackerNation == nations.GetComponent<NationHandler>().nation.tribe.ToString())
                {
                    Swordsmen = nations.GetComponent<NationHandler>().nation.swordsmen;
                    Archers = nations.GetComponent<NationHandler>().nation.archers;
                    AttackModifier = nations.GetComponent<NationHandler>().nation.AttackModifier;
                    HealthModifier = nations.GetComponent<NationHandler>().nation.HealthModifier;
                    SizeModifier = nations.GetComponent<NationHandler>().nation.SizeModifier;
                    InfantryHealthModifier = nations.GetComponent<NationHandler>().nation.InfantryHealthModifier;
                    ArcherRangeModifier = nations.GetComponent<NationHandler>().nation.ArcherRangeModifier;
                    ChargeModifier = nations.GetComponent<NationHandler>().nation.ChargeModifier;
                    Nation = nations.GetComponent<NationHandler>().nation.name;
                    Color = GameManager.instance.attackerNationcolor; 
                    print(nations.GetComponent<NationHandler>().nation.name + " attacks");
                }
            }
        }
    }
}
