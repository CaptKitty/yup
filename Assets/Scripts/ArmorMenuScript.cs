using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorMenuScript : MonoBehaviour
{
    //private List<Units.Weapons> weapondropoptions = new List<Units.Weapons> { Units.Weapons.Sword, Units.Weapons.Axe, Units.Weapons.Longbow};

    public List<string> armorlist = new List<string>();

    Dropdown dropdown;

    public int ArmorHealth;
    public int ArmorCost;
    
    public GameObject weaponsmenuscript;

    // Start is called before the first frame update
    void Start()
    {
        dropdown = transform.GetComponent<Dropdown>();

        GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Units") as GameObject[];
        foreach(GameObject units in theArrays)
        {
            if("PLAYER" == units.transform.parent.GetComponent<NationHandler>().nation.tribe.ToString())
            {
                if(units.GetComponent<UnitHandler>().units.name.ToString() == this.transform.parent.name.ToString())
                {
                    string[] Armornames = Enum.GetNames(typeof(Units.Armor));
                    armorlist = new List<string>(Armornames);
                    foreach (string armor in armorlist)
                    {
                        dropdown.options.Add(new Dropdown.OptionData() {text = armor});
                        for (int i = armorlist.Count-1; i > -1; i--)
                        {
                            if (armorlist[i] == units.GetComponent<UnitHandler>().units.armor.ToString())
                            {
                                dropdown.value = i;
                                //print(armorlist[i]);
                            }
                        }
                    }
                    
                    this.transform.Find("Label").GetComponent<Text>().text = units.GetComponent<UnitHandler>().units.armor.ToString();
                }
            }
        }
        if(dropdown.value == 0) // Heavy
        {
            ArmorHealth = 100;
            ArmorCost = 10;
        }
        if(dropdown.value == 1) // Light
        {
            ArmorHealth = 50;
            ArmorCost = 5;
        }
        if(dropdown.value == 2) // None
        {
            ArmorHealth = 0;
            ArmorCost = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        weaponsmenuscript.GetComponent<WeaponMenuScript>().ArmorHealth = ArmorHealth;
        weaponsmenuscript.GetComponent<WeaponMenuScript>().ArmorCost = ArmorCost;
    }
    public void OnArmorValueChange()
    {
        if(dropdown.value == 0) // Heavy
        {
            ArmorHealth = 100;
            ArmorCost = 15;
        }
        if(dropdown.value == 1) // Light
        {
            ArmorHealth = 50;
            ArmorCost = 5;
        }
        if(dropdown.value == 2) // None
        {
            ArmorHealth = 0;
            ArmorCost = 0;
        }
    }
}
