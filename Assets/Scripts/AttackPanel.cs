using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPanel : MonoBehaviour
{

    public Text descriptionText;
    public Text desctext;
    public Text descforces;
    public Text descdefence;
    public Text nationName;

    public string Descforces;

    public string descpops;

    public string defence;

    public void Update()
    {
        Descforces = "";
        descpops = "";
        defence = "";
        GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Units") as GameObject[];
        foreach(GameObject Units in theArrays)
        {
            if(GameManager.instance.country.tribe.ToString() == Units.transform.parent.GetComponent<NationHandler>().nation.tribe.ToString())
            {
                Descforces += Units.GetComponent<UnitHandler>().units.name + ": " + Units.GetComponent<UnitHandler>().units.number + "\n";
            }
        }
        descforces.text = Descforces;
        // foreach(var poptype in GameManager.instance.country.pops.poplist)
        // {
        //     descpops += "kittens\n";
        // }
        foreach(PopType poptype in GameManager.instance.country.pops.poplist)
        {
            descpops += " " + poptype.population.ToString() + " " + poptype.culture + "\n";
        }   
        desctext.text = "Population: " + GameManager.instance.country.pops.totalPopulation + "\n" + descpops;// + "Recruits: " + GameManager.instance.country.recruits + "\nTax Income: " + GameManager.instance.country.taxIncome;
        descdefence.text = "Defenses:\n Fortifications: " + GameManager.instance.country.fortifications + "\n Militia: " + GameManager.instance.country.militia;
        nationName.text = GameManager.instance.country.tribe.ToString();
    }

    public void ButtonBuildFortification()
    {
        GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
        foreach(GameObject countries in theArrays)
        {
            if(GameManager.instance.country.name == countries.name)
            {
                
                GameObject[] theArray = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
                foreach(GameObject nation in theArray)
                {
                    if(nation.name == "PLAYER" && nation.GetComponent<NationHandler>().nation.taxTreasury >= 100)
                    {
                        nation.GetComponent<NationHandler>().nation.taxTreasury -= 100;
                        countries.GetComponent<CountryHandler>().BuildFortification(1);
                    }
                }
            }
        }
    }
    public void ButtonBuildMilitia()
    {
        GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
        foreach(GameObject countries in theArrays)
        {
            if(GameManager.instance.country.name == countries.name)
            {
                
                GameObject[] theArray = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
                foreach(GameObject nation in theArray)
                {
                    if(nation.name == "PLAYER" && nation.GetComponent<NationHandler>().nation.totalRecruits >= 100)
                    {
                        nation.GetComponent<NationHandler>().nation.totalRecruits -= 100;
                        countries.GetComponent<CountryHandler>().BuildMilitia(1);
                    }
                }
            }
        }
    }
}
