using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitFinisher : MonoBehaviour
{

    public GameObject ArmorScript;
    public GameObject WeaponScript;
    public GameObject MountScript;
    public GameObject InputMenuScript;

    public List<string> armorlist = new List<string>();
    public List<string> weaponlist = new List<string>();

    public int unitcost;
    public int regimentcost;
    public int regimentcostnew;
    public int regimentmanpowercost;
    public int regimentmanpowercostnew;
    
    void Start()
    {
        //print(WeaponScript.GetComponent<WeaponMenuScript>().WeaponCost);
        //print(WeaponScript.GetComponent<WeaponMenuScript>().ArmorCost);
        unitcost = WeaponScript.GetComponent<WeaponMenuScript>().WeaponCost + WeaponScript.GetComponent<WeaponMenuScript>().ArmorCost + 1;
        regimentcost = unitcost * WeaponScript.GetComponent<WeaponMenuScript>().Numbers;
        regimentmanpowercost = 100 * WeaponScript.GetComponent<WeaponMenuScript>().Numbers;
    }


    public void Finisher()
    {
        GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Units") as GameObject[];
        foreach(GameObject units in theArrays)
        {
            if(units.transform.parent.GetComponent<NationHandler>().nation.tribe.ToString() == "PLAYER")
            {
                //print(units.GetComponent<UnitHandler>().units.name);
                //print(this.transform.parent.name);
                if(units.GetComponent<UnitHandler>().units.name == this.transform.parent.name)
                {
                    units.GetComponent<UnitHandler>().units.maxhealth = WeaponScript.GetComponent<WeaponMenuScript>().Health;
                    units.GetComponent<UnitHandler>().units.attackDamage = WeaponScript.GetComponent<WeaponMenuScript>().Damage;
                    units.GetComponent<UnitHandler>().units.attackRate = WeaponScript.GetComponent<WeaponMenuScript>().attackRate;
                    units.GetComponent<UnitHandler>().units.attackRange = WeaponScript.GetComponent<WeaponMenuScript>().Range;
                    units.GetComponent<UnitHandler>().units.projectileSpeed = WeaponScript.GetComponent<WeaponMenuScript>().projectileSpeed;
                    units.GetComponent<UnitHandler>().units.chargebonus = WeaponScript.GetComponent<WeaponMenuScript>().chargebonus;

                    units.GetComponent<UnitHandler>().units.MovementSpeed = WeaponScript.GetComponent<WeaponMenuScript>().Speed;
                    units.GetComponent<UnitHandler>().units.maximumspeed = WeaponScript.GetComponent<WeaponMenuScript>().MaxSpeed;

                    units.GetComponent<UnitHandler>().units.armor = (Units.Armor)(ArmorScript.GetComponent<Dropdown>().value);
                    units.GetComponent<UnitHandler>().units.weapon = (Units.Weapons)(WeaponScript.GetComponent<Dropdown>().value);
                    units.GetComponent<UnitHandler>().units.mount = (Units.Mounts)(MountScript.GetComponent<Dropdown>().value);
                    units.GetComponent<UnitHandler>().units.cost = WeaponScript.GetComponent<WeaponMenuScript>().WeaponCost + WeaponScript.GetComponent<WeaponMenuScript>().ArmorCost + WeaponScript.GetComponent<WeaponMenuScript>().MountCost + 1;
                    units.GetComponent<UnitHandler>().units.number = InputMenuScript.GetComponent<InputMenuScript>().numbers;
                    units.GetComponent<UnitHandler>().units.name = InputMenuScript.GetComponent<InputMenuScript>().names;
                    
                    if(units.GetComponent<UnitHandler>().units.attackRange >= 2)
                    {
                        units.GetComponent<UnitHandler>().units.ranged = true;
                    }
                    else
                    {
                        units.GetComponent<UnitHandler>().units.ranged = false;
                    }
                    regimentcostnew = units.GetComponent<UnitHandler>().units.cost * units.GetComponent<UnitHandler>().units.number;
                    regimentmanpowercostnew= 100 * units.GetComponent<UnitHandler>().units.number;
                    GameObject[] Nations = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
                    foreach(GameObject nation in Nations)
                    {
                        if (nation.GetComponent<NationHandler>().nation.tribe == "PLAYER")
                        {
                            nation.GetComponent<NationHandler>().nation.taxTreasury += regimentcost;
                            nation.GetComponent<NationHandler>().nation.taxTreasury -= regimentcostnew;

                            nation.GetComponent<NationHandler>().nation.totalRecruits += regimentmanpowercost;
                            nation.GetComponent<NationHandler>().nation.totalRecruits -= regimentmanpowercostnew;
                            // print(nation.GetComponent<NationHandler>().nation.taxTreasury + " Treasury");
                            // print(regimentcost + " Old Unit Cost");
                            // print(regimentcostnew + " New Unit Cost");
                            regimentcost = regimentcostnew;
                            regimentmanpowercost = regimentmanpowercostnew;
                        }
                    }
                }
            }
        }
    }
}
