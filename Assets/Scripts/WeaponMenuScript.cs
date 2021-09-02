using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenuScript : MonoBehaviour
{
    //private List<Units.Weapons> weapondropoptions = new List<Units.Weapons> { Units.Weapons.Sword, Units.Weapons.Axe, Units.Weapons.Longbow};

    public List<string> weaponslist = new List<string>();

    public Text desctext;

    public int Damage = 25;
    public int WeaponDamage;
    public int Health;// = 50;
    public int WeaponCost = 4;
    public float attackRate;
    public float projectileSpeed;
    public float chargebonus;
    public float Speed;
    public float MaxSpeed;
    public int Cost;
    public int ArmorCost = 5;
    public int Numbers;
    public float Range = 1.2f;
    public float WeaponRange;
    public string names;
    public int ArmorHealth;
    public int MountHealth;
    public int MountCost;
    public float MountSpeed;
    public float MountMaxSpeed;

    Dropdown dropdown;// = transform.GetComponent<Dropdown>();

    // Start is called before the first frame update
    void Start()
    {
        dropdown = transform.GetComponent<Dropdown>();

        GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Units") as GameObject[];
        foreach(GameObject Unitss in theArrays)
        {
            if("PLAYER" == Unitss.transform.parent.GetComponent<NationHandler>().nation.tribe.ToString())
            {
                if(this.transform.parent.name.ToString() == Unitss.GetComponent<UnitHandler>().units.name.ToString())
                {
                    string[] WeaponNames = Enum.GetNames(typeof(Units.Weapons));
                    weaponslist = new List<string>(WeaponNames);
                    foreach (string weapon in weaponslist)
                    {
                        dropdown.options.Add(new Dropdown.OptionData() {text = weapon});
                        for (int i = weaponslist.Count-1; i > -1; i--)
                        {
                            if (weaponslist[i] == Unitss.GetComponent<UnitHandler>().units.weapon.ToString())
                            {
                                dropdown.value = i;
                                //print(weaponslist[i]);
                            }
                        }
                    }

                    this.transform.Find("LabelText").GetComponent<Text>().text = Unitss.GetComponent<UnitHandler>().units.weapon.ToString();
                    Numbers = Unitss.GetComponent<UnitHandler>().units.number;
                    names = Unitss.GetComponent<UnitHandler>().units.name;
                    Health = Unitss.GetComponent<UnitHandler>().units.maxhealth;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Health = 50 + ArmorHealth + MountHealth;
        Damage = 0 + WeaponDamage;
        Range = WeaponRange;
        Cost = 1 + WeaponCost + ArmorCost + MountCost;
        Speed = MountSpeed;// / 1000;
        MaxSpeed = MountMaxSpeed;


        //desctext.text = "Damage: " + Damage + "\nHealth: " + Health + "\nRange: " + Range + "\nCost: " + (ArmorCost + WeaponCost + 1) + "\nNumber: " + Numbers;
        desctext.text = "";
        desctext.text += "Damage: " + Damage;
        desctext.text += "\nHealth: " + Health;
        desctext.text += "\nRange: " + Range;
        desctext.text += "\nCost: " + Cost;
        desctext.text += "\nSpeed: " + (Speed / 1000);
        desctext.text += "\nMaxSpeed: " + MaxSpeed;
        desctext.text += "\nNumber: " + Numbers;
    }
    public void OnWeaponValueChange()
    {
        if(dropdown.value == 0) // Axe
        {
            WeaponDamage = 40;
            attackRate = 1;
            WeaponRange = 1.0f;
            WeaponCost = 5;
            chargebonus = 0.25f;
        }
        if(dropdown.value == 1) // Sword
        {
            WeaponDamage = 25;
            attackRate = 1;
            WeaponRange = 1.2f;
            WeaponCost = 4;
            chargebonus = 0.25f;
        }
        if(dropdown.value == 2) // Pike
        {
            WeaponDamage = 20;
            attackRate = 1;
            WeaponRange = 1.6f;
            WeaponCost = 3;
            chargebonus = 0.5f;
        }
        if(dropdown.value == 3) // Longbow
        {
            WeaponDamage = 8;
            attackRate = 1;
            WeaponRange = 10f;
            projectileSpeed = 5f;
            WeaponCost = 9;
            chargebonus = 0f;
        }
        if(dropdown.value == 4) // Crossbow
        {
            WeaponDamage = 20;
            attackRate = 2;
            WeaponRange = 5f;
            projectileSpeed = 6f;
            WeaponCost = 8;
            chargebonus = 0f;
        }
        if(dropdown.value == 5) // None
        {
            WeaponDamage = 10;
            attackRate = 1;
            WeaponRange = 1.0f;
            WeaponCost = 0;
            chargebonus = 0f;
        }
    }
    
}
