using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NationHandler : MonoBehaviour
{
    public static NationHandler instance;

    public Nation nation;

    public GameObject UnitHandler;

    public List<GameObject> UnitList = new List<GameObject>();


    void Awake()
    {
        AddSwordsmen();
        AddArcher();
        AddCustomTroop();
        SetHumanRights();
    }
    void AddSwordsmen()
    {
        GameObject Unit = Instantiate(UnitHandler) as GameObject;
        Unit.transform.parent = this.transform;
        Unit.name = "Swordsmen";
        Unit.tag = "Units";
        Unit.GetComponent<UnitHandler>().units.name = "Swordsmen";
        Unit.GetComponent<UnitHandler>().units.ranged = false;
        Unit.GetComponent<UnitHandler>().units.attackRange = 1.2f;
        Unit.GetComponent<UnitHandler>().units.projectileSpeed = 5f;
        Unit.GetComponent<UnitHandler>().units.attackDamage = 25;
        Unit.GetComponent<UnitHandler>().units.attackRate = 1f;
        Unit.GetComponent<UnitHandler>().units.maxhealth = 100;
        Unit.GetComponent<UnitHandler>().units.MovementSpeed = 2000f;
        Unit.GetComponent<UnitHandler>().units.maximumspeed = 2f;
        Unit.GetComponent<UnitHandler>().units.cost = 5;
        Unit.GetComponent<UnitHandler>().units.number = 10;
        Unit.GetComponent<UnitHandler>().units.weapon = Units.Weapons.Sword;
        Unit.GetComponent<UnitHandler>().units.armor = Units.Armor.Light;
        UnitList.Add(Unit);
    }
    void AddArcher()
    {
        GameObject Unit = Instantiate(UnitHandler) as GameObject;
        Unit.transform.parent = this.transform;
        Unit.name = "Archer";
        Unit.tag = "Units";
        Unit.GetComponent<UnitHandler>().units.name = "Archer";
        Unit.GetComponent<UnitHandler>().units.ranged = true;
        Unit.GetComponent<UnitHandler>().units.attackRange = 10f;
        Unit.GetComponent<UnitHandler>().units.projectileSpeed = 5f;
        Unit.GetComponent<UnitHandler>().units.attackDamage = 8;
        Unit.GetComponent<UnitHandler>().units.attackRate = 1f;
        Unit.GetComponent<UnitHandler>().units.maxhealth = 50;
        Unit.GetComponent<UnitHandler>().units.MovementSpeed = 2000f;
        Unit.GetComponent<UnitHandler>().units.maximumspeed = 2f;
        Unit.GetComponent<UnitHandler>().units.cost = 15;
        Unit.GetComponent<UnitHandler>().units.number = 10;
        Unit.GetComponent<UnitHandler>().units.weapon = Units.Weapons.Longbow;
        Unit.GetComponent<UnitHandler>().units.armor = Units.Armor.None;
        UnitList.Add(Unit);
    }
    void AddCustomTroop()
    {
        GameObject Unit = Instantiate(UnitHandler) as GameObject;
        Unit.transform.parent = this.transform;
        Unit.name = "Custom";
        Unit.tag = "Units";
        Unit.GetComponent<UnitHandler>().units.name = "Custom";
        Unit.GetComponent<UnitHandler>().units.ranged = false;
        Unit.GetComponent<UnitHandler>().units.attackRange = 1.0f;
        Unit.GetComponent<UnitHandler>().units.projectileSpeed = 5f;
        Unit.GetComponent<UnitHandler>().units.attackDamage = 10;
        Unit.GetComponent<UnitHandler>().units.attackRate = 1f;
        Unit.GetComponent<UnitHandler>().units.maxhealth = 50;
        Unit.GetComponent<UnitHandler>().units.MovementSpeed = 2000f;
        Unit.GetComponent<UnitHandler>().units.maximumspeed = 2f;
        Unit.GetComponent<UnitHandler>().units.cost = 1;
        Unit.GetComponent<UnitHandler>().units.number = 0;
        Unit.GetComponent<UnitHandler>().units.weapon = Units.Weapons.None;
        Unit.GetComponent<UnitHandler>().units.armor = Units.Armor.None;
        UnitList.Add(Unit);
    }
    void SetHumanRights()
    {
        nation.laws.humanRights.Add( new HumanRights() { RightsName = "Life", rightsSpread = HumanRights.RightsSpread.Universal});
        nation.laws.humanRights.Add( new HumanRights() { RightsName = "Property", rightsSpread = HumanRights.RightsSpread.Exclusive});
    }
    
    // public void GetNationColor(byte alpha)
    // {
    //     Color32 Nationcolor = new Color32(nation.colorr, nation.colorg, nation.colorb, alpha);
    //     return Nationcolor;
    // }
}
