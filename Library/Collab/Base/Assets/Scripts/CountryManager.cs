using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountryManager : MonoBehaviour
{    
    public static CountryManager instance;

    public GameObject AttackPanel;

    public GameObject ArmoryButton;

    public GameObject TurnButton;

    public List<GameObject> countryList = new List<GameObject>();

    void Awake()
    {
        instance = this;
    }

    public void CheckGains()
    {
        GameObject[] NationUpdates = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in NationUpdates)
        {
            nation.GetComponent<NationHandler>().nation.taxIncome = 0;
            nation.GetComponent<NationHandler>().nation.recruitsIncome = 0;
        }
        GameObject[] NextTurns = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
        foreach(GameObject country in NextTurns)
        {
            float temprecruit = country.GetComponent<CountryHandler>().country.totalPopulation * country.GetComponent<CountryHandler>().country.recruitablePopulation;
            float temptax = country.GetComponent<CountryHandler>().country.totalPopulation * country.GetComponent<CountryHandler>().country.taxRate;
           
            foreach(GameObject nation in NationUpdates)
            {
                if (country.GetComponent<CountryHandler>().country.tribe.ToString() == nation.GetComponent<NationHandler>().nation.tribe.ToString())
                {  
                    nation.GetComponent<NationHandler>().nation.taxIncome += (int) temptax;
                    nation.GetComponent<NationHandler>().nation.recruitsIncome += (int) temprecruit;
                }
            }
        }
        foreach(GameObject nation in NationUpdates)
        {
            GameObject[] unitMaintenance = GameObject.FindGameObjectsWithTag("Units") as GameObject[];
            foreach (GameObject maintenance in unitMaintenance)
            {
                if(nation.GetComponent<NationHandler>().nation.tribe == maintenance.transform.parent.GetComponent<NationHandler>().nation.tribe.ToString())
                {
                    nation.GetComponent<NationHandler>().nation.recruitsIncome -= maintenance.GetComponent<UnitHandler>().units.number;
                    nation.GetComponent<NationHandler>().nation.taxIncome -= maintenance.GetComponent<UnitHandler>().units.number;
                }
            }
            nation.GetComponent<NationHandler>().nation.recruitsIncome = nation.GetComponent<NationHandler>().nation.recruitsIncome - (nation.GetComponent<NationHandler>().nation.totalRecruits / 10);
        }
    }

    public void Armory()
    {
        SceneManager.LoadScene("Armory");
    }

    public void NextTurn()
    {
        CheckGains();
        //Aging population
        GameObject[] agingUpdate = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject aging in agingUpdate)
        {
            GameObject[] unitMaintenance = GameObject.FindGameObjectsWithTag("Units") as GameObject[];
            foreach (GameObject maintenance in unitMaintenance)
            {
                if(aging.GetComponent<NationHandler>().nation.tribe == maintenance.transform.parent.GetComponent<NationHandler>().nation.tribe.ToString())
                {
                    aging.GetComponent<NationHandler>().nation.totalRecruits -= maintenance.GetComponent<UnitHandler>().units.number;
                    aging.GetComponent<NationHandler>().nation.taxTreasury -= maintenance.GetComponent<UnitHandler>().units.number;
                }
            }
            aging.GetComponent<NationHandler>().nation.totalRecruits = aging.GetComponent<NationHandler>().nation.totalRecruits * 9 / 10;
            if ( aging.GetComponent<NationHandler>().nation.totalRecruits <= 0 )
            {
                aging.GetComponent<NationHandler>().nation.totalRecruits = 0;
            }
        }
        
        GameObject[] NextTurns = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
        foreach(GameObject country in NextTurns)
        {
            
            float totalPop = country.GetComponent<CountryHandler>().country.totalPopulation * ( 1 + country.GetComponent<CountryHandler>().country.growthPopulation);
            float recruit = country.GetComponent<CountryHandler>().country.totalPopulation * country.GetComponent<CountryHandler>().country.recruitablePopulation;
            float tax = country.GetComponent<CountryHandler>().country.totalPopulation * country.GetComponent<CountryHandler>().country.taxRate;
            country.GetComponent<CountryHandler>().country.totalPopulation = (int) totalPop;
            country.GetComponent<CountryHandler>().country.recruits = (int) recruit;
            country.GetComponent<CountryHandler>().country.totalPopulation -= (int) recruit;
            country.GetComponent<CountryHandler>().country.taxIncome = (int) tax;
            
            GameObject[] NationUpdate = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
            foreach(GameObject nation in NationUpdate)
            {
                if (country.GetComponent<CountryHandler>().country.tribe.ToString() == nation.GetComponent<NationHandler>().nation.tribe.ToString())
                {  
                    //nation.GetComponent<NationHandler>().nation.taxIncome += country.GetComponent<CountryHandler>().country.taxIncome;
                    nation.GetComponent<NationHandler>().nation.taxTreasury += country.GetComponent<CountryHandler>().country.taxIncome;
                    //nation.GetComponent<NationHandler>().nation.recruitsIncome += country.GetComponent<CountryHandler>().country.recruits;
                    nation.GetComponent<NationHandler>().nation.totalRecruits += country.GetComponent<CountryHandler>().country.recruits;
                }
            }
        }
        AIturn();
        GameManager.instance.Saving();
    }

    void AIturn()
    {
        GameObject[] NationUpdate = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in NationUpdate)
        {
            if (nation.GetComponent<NationHandler>().nation.tribe.ToString() == "PLAYER")
            {
                GameObject[] UnitUpdate = GameObject.FindGameObjectsWithTag("Units") as GameObject[];
                foreach(GameObject unit in UnitUpdate)
                {
                    if (unit.transform.parent.GetComponent<NationHandler>().nation.tribe.ToString() != "PLAYER")
                    {
                        if (unit.transform.parent.GetComponent<NationHandler>().nation.taxIncome >= 0 && unit.transform.parent.GetComponent<NationHandler>().nation.taxTreasury >= 100 && unit.transform.parent.GetComponent<NationHandler>().nation.totalRecruits >= 100 && unit.transform.parent.GetComponent<NationHandler>().nation.recruitsIncome >= 0)
                        {
                            unit.transform.parent.GetComponent<NationHandler>().nation.taxTreasury -= 10;
                            unit.transform.parent.GetComponent<NationHandler>().nation.totalRecruits -= 100;
                            unit.GetComponent<UnitHandler>().units.number += 1;
                        }
                        if (unit.GetComponent<UnitHandler>().units.number < 5 && unit.GetComponent<UnitHandler>().units.name != "Custom")
                        {
                            unit.GetComponent<UnitHandler>().units.number = 5;
                        }
                    }
                }
            }          
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ArmoryButton.SetActive(true);
        AttackPanel.SetActive(false);
        TurnButton.SetActive(true);

        AddCountryData();
        
        if(GameManager.instance.battleHasEnded && GameManager.instance.battleWon)
        {
            CountryHandler count = GameObject.Find(GameManager.instance.attackedCountry).GetComponent<CountryHandler>();
            count.country.tribe = Country.theTribes.PLAYER;
            TintCountries();
            AIturn();
        }
        GameManager.instance.Saving();
        CheckGains();
    }

    void AddCountryData(){
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
        foreach(GameObject country in theArray)
        {
            countryList.Add(country);
        }
        GameManager.instance.Loading();
        TintCountries();
        //PopulateCountries();
    }

    public void PopulateCountries()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
        foreach(GameObject country in theArray)
        {
            for(int i = 0 ; i < countryList.Count; i++)
            {
                if(country.GetComponent<CountryHandler>().country.name == countryList[i].GetComponent<CountryHandler>().name)
                {
                    // print(country.GetComponent<CountryHandler>().country.name);
                    // print(countryList[i].GetComponent<CountryHandler>().country.totalPopulation);
                    country.GetComponent<CountryHandler>().country.totalPopulation = countryList[i].GetComponent<CountryHandler>().country.totalPopulation;
                }
            }            
        }
    }

    public void TintCountries()
    {
        for(int i = 0 ; i < countryList.Count; i++)
        {
            CountryHandler countHandler = countryList[i].GetComponent<CountryHandler>();

            if(countHandler.country.tribe == Country.theTribes.FRANKS)
            {
                countHandler.TintColor(new Color32(255,0,0,128));
            }
            if(countHandler.country.tribe == Country.theTribes.POLISH)
            {
                countHandler.TintColor(new Color32(175,15,15,128));
            }
            if(countHandler.country.tribe == Country.theTribes.BURGUNDIANS)
            {
                countHandler.TintColor(new Color32(0,255,0,128));
            }
            if(countHandler.country.tribe == Country.theTribes.GOTHS)
            {
                countHandler.TintColor(new Color32(128,128,0,128));
            }
            if(countHandler.country.tribe == Country.theTribes.PLAYER)
            {
                countHandler.TintColor(new Color32(0,0,255,128));
            }
        }
    }

    public void ShowAttackPanel(string description)//, int totalPopulationReward, int expReward)
    {
        ArmoryButton.SetActive(false);
        AttackPanel.SetActive(true);
        TurnButton.SetActive(false);
        AttackPanel gui = AttackPanel.GetComponent<AttackPanel>();
        gui.descriptionText.text = description;
    }

    public void DisableAttackPanel()
    {
        ArmoryButton.SetActive(true);
        AttackPanel.SetActive(false);
        TurnButton.SetActive(true);
    }
    public void StartFight()
    {
        // if(GameManager.instance.battleHasEnded && GameManager.instance.battleWon)
        // {
            CountryHandler count = GameObject.Find(GameManager.instance.attackedCountry).GetComponent<CountryHandler>();
            GameManager.instance.archers = count.country.archers;
            GameManager.instance.swordsmen = count.country.swordsmen;
        //}

        if (count.country.tribe.ToString() != "PLAYER")
        {
            SceneManager.LoadScene("Fight");
        }
    }
}
