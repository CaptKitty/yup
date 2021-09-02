using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MountMenuScript : MonoBehaviour
{
    //private List<Units.Weapons> weapondropoptions = new List<Units.Weapons> { Units.Weapons.Sword, Units.Weapons.Axe, Units.Weapons.Longbow};

    public List<string> mountlist = new List<string>();

    Dropdown dropdown;

    public int MountHealth;
    public int MountCost;
    public float MountSpeed;
    public float MountMaxSpeed;
    
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
                    string[] Mountnames = Enum.GetNames(typeof(Units.Mounts));
                    mountlist = new List<string>(Mountnames);
                    foreach (string mount in mountlist)
                    {
                        dropdown.options.Add(new Dropdown.OptionData() {text = mount});
                        for (int i = mountlist.Count-1; i > -1; i--)
                        {
                            if (mountlist[i] == units.GetComponent<UnitHandler>().units.mount.ToString())
                            {
                                dropdown.value = i;
                                //print(armorlist[i]);
                            }
                        }
                    }
                    
                    this.transform.Find("Label").GetComponent<Text>().text = units.GetComponent<UnitHandler>().units.mount.ToString();
                }
            }
        }
        if(dropdown.value == 0) // None
        {
            MountHealth = 0;
            MountCost = 0;
            MountSpeed = 2000f;
            MountMaxSpeed = 3f;
        }
        if(dropdown.value == 1) // Horse
        {
            MountHealth = 50;
            MountCost = 20;
            MountSpeed = 4000f;
            MountMaxSpeed = 5f;
        }
        // if(dropdown.value == 2) // None
        // {
        //     ArmorHealth = 0;
        //     ArmorCost = 0;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        weaponsmenuscript.GetComponent<WeaponMenuScript>().MountHealth = MountHealth;
        weaponsmenuscript.GetComponent<WeaponMenuScript>().MountCost = MountCost;
        weaponsmenuscript.GetComponent<WeaponMenuScript>().MountSpeed = MountSpeed;
        weaponsmenuscript.GetComponent<WeaponMenuScript>().MountMaxSpeed = MountMaxSpeed;
    }
    public void OnMountValueChange()
    {
        if(dropdown.value == 0) // None
        {
            MountHealth = 0;
            MountCost = 0;
            MountSpeed = 2000f;
            MountMaxSpeed = 3f;
        }
        if(dropdown.value == 1) // Horse
        {
            MountHealth = 50;
            MountCost = 20;
            MountSpeed = 4000f;
            MountMaxSpeed = 5f;
        }
    }
}
