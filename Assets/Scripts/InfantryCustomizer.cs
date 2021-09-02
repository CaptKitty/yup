using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfantryCustomizer : MonoBehaviour
{

    public GameObject swordsmen;
    public GameObject archer;

    void Awake()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Units") as GameObject[];
        foreach(GameObject unit in theArray)
        {
            if (unit.transform.parent.name == "PLAYER")
            {
                if (unit.name == "Swordsmen")
                {
                    swordsmen = unit.gameObject;
                }
                if (unit.name == "Archer")
                {
                    archer = unit.gameObject;
                }
            }
        }
    }

    public void Play()
    {
        GameObject[] Nations = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in Nations)
        {
            if (nation.GetComponent<NationHandler>().nation.tribe == "PLAYER")
            {
                if (nation.GetComponent<NationHandler>().nation.taxTreasury >= 0 && nation.GetComponent<NationHandler>().nation.totalRecruits >= 0)
                {
                    GameManager.instance.SaveNation();
                    SceneManager.LoadScene("Main");
                }
            }
        }
    }

    public void OptA()//axe
    {
        swordsmen.GetComponent<UnitHandler>().units.attackRange = 1.2f;
        swordsmen.GetComponent<UnitHandler>().units.attackDamage = 50;
        swordsmen.GetComponent<UnitHandler>().units.maxhealth = 50;
        swordsmen.GetComponent<UnitHandler>().units.chargebonus = 0.25f;
    }
    public void OptB()//sword
    {
        swordsmen.GetComponent<UnitHandler>().units.attackRange = 1.2f;
        swordsmen.GetComponent<UnitHandler>().units.attackDamage = 25;
        swordsmen.GetComponent<UnitHandler>().units.maxhealth = 100;
        swordsmen.GetComponent<UnitHandler>().units.chargebonus = 0.25f;
    }
    public void OptC()//pike
    {
        swordsmen.GetComponent<UnitHandler>().units.attackRange = 1.6f;
        swordsmen.GetComponent<UnitHandler>().units.attackDamage = 25;
        swordsmen.GetComponent<UnitHandler>().units.maxhealth = 75;
        swordsmen.GetComponent<UnitHandler>().units.chargebonus = 1f;
    }
    public void OptD()//bow
    {
        archer.GetComponent<UnitHandler>().units.attackRange = 10f;
        archer.GetComponent<UnitHandler>().units.attackDamage = 8;
        archer.GetComponent<UnitHandler>().units.maxhealth = 50;
        archer.GetComponent<UnitHandler>().units.chargebonus = 0f;
    }
    public void OptE()//crossbow
    {
        archer.GetComponent<UnitHandler>().units.attackRange = 6f;
        archer.GetComponent<UnitHandler>().units.attackDamage = 10;
        archer.GetComponent<UnitHandler>().units.maxhealth = 75;
        archer.GetComponent<UnitHandler>().units.chargebonus = 0f;
    }
}
