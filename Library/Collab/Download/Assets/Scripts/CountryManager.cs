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

    public GameObject GovernmentButton;

    public GameObject TurnButton;
    
    public GameObject Header;

    public List<GameObject> countryList = new List<GameObject>();



    private float temprecruit;
    private float temptax;

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
            country.GetComponent<CountryHandler>().country.recruits = 0;
            
            temprecruit = 0;
            foreach(PopType poptype in country.GetComponent<CountryHandler>().country.pops.poplist)
            {
                float tempPopDraft = poptype.population * poptype.draftrate;
                temprecruit += (int) tempPopDraft;
            }
            //float temprecruit = country.GetComponent<CountryHandler>().country.pops.totalPopulation * country.GetComponent<CountryHandler>().country.recruitablePopulation;
            //float temptax = country.GetComponent<CountryHandler>().country.pops.totalPopulation * country.GetComponent<CountryHandler>().country.popstaxRate;
           
            temptax = 0;
            foreach(PopType poptype in country.GetComponent<CountryHandler>().country.pops.poplist)
            {
                float tempPopTax = poptype.population * poptype.taxrate;
                temptax += (int) tempPopTax;
            }

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
        GameManager.instance.Saving();
        GameManager.instance.SaveNation();
        SceneManager.LoadScene("Armory");
    }

    public void Government()
    {
        GameManager.instance.Saving();
        GameManager.instance.SaveNation();
        SceneManager.LoadScene("Gubernment");
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
            aging.GetComponent<NationHandler>().nation.totalRecruits = aging.GetComponent<NationHandler>().nation.totalRecruits - (aging.GetComponent<NationHandler>().nation.totalRecruits / 10);
            if ( aging.GetComponent<NationHandler>().nation.totalRecruits <= 0 )
            {
                aging.GetComponent<NationHandler>().nation.totalRecruits = 0;
            }
        }
        
        GameObject[] NextTurns = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
        foreach(GameObject country in NextTurns)
        {
            country.GetComponent<CountryHandler>().country.recruits = 0;
            foreach(PopType poptype in country.GetComponent<CountryHandler>().country.pops.poplist)
            {
                float tempPopgrowth = poptype.population * poptype.growthrate;
                float tempPopDraft = poptype.population * poptype.draftrate;
                // if(country.GetComponent<CountryHandler>().country.tribe.ToString() == "PLAYER")
                // {
                //     print(poptype.culture + poptype.population + poptype.draftrate);
                // }
                poptype.population = (int) poptype.population + (int) tempPopgrowth - (int) tempPopDraft;
                country.GetComponent<CountryHandler>().country.recruits += (int) tempPopDraft;
            }
            //float totalPop = country.GetComponent<CountryHandler>().country.pops.totalPopulation * ( 1 + country.GetComponent<CountryHandler>().country.growthPopulation);
            //float recruit = country.GetComponent<CountryHandler>().country.pops.totalPopulation * country.GetComponent<CountryHandler>().country.recruitablePopulation;
            // = country.GetComponent<CountryHandler>().country.pops.totalPopulation * country.GetComponent<CountryHandler>().country.taxRate;
            country.GetComponent<CountryHandler>().country.taxIncome = 0;
            foreach(PopType poptype in country.GetComponent<CountryHandler>().country.pops.poplist)
            {
                float tempPopTax = poptype.population * poptype.taxrate;
                // if(country.GetComponent<CountryHandler>().country.tribe.ToString() == "PLAYER")
                // {
                //     print(poptype.culture + poptype.population + poptype.draftrate);
                // }
                country.GetComponent<CountryHandler>().country.taxIncome += (int) tempPopTax;
            }
            //country.GetComponent<CountryHandler>().country.pops.totalPopulation = (int) totalPop;
            //country.GetComponent<CountryHandler>().country.recruits = (int) recruit;
            //country.GetComponent<CountryHandler>().country.pops.totalPopulation -= (int) recruit;
            //country.GetComponent<CountryHandler>().country.taxIncome = (int) tax;
            
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
            country.GetComponent<CountryHandler>().CountPopulation();
        }
        AddPopTypes();
        GameManager.instance.Saving();
        GameManager.instance.SaveNation();
        GameManager.instance.AIturn();
    }


    public void AddPopTypes()
    {
        GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in theArrays)
        {
            foreach(PopType poptyp in nation.GetComponent<NationHandler>().nation.popTypes)
            {
                poptyp.population = 0;
            }
            GameObject[] theArray = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
            foreach(GameObject country in theArray)
            {
                if(nation.GetComponent<NationHandler>().nation.name == country.GetComponent<CountryHandler>().country.tribe.ToString())
                {
                    foreach(PopType poptype in country.GetComponent<CountryHandler>().country.pops.poplist)
                    {
                        bool addEstate = true;
                        foreach(PopType poptypes in nation.GetComponent<NationHandler>().nation.popTypes)
                        {
                            if(poptypes.culture == poptype.culture)
                            {
                                poptypes.population += poptype.population;
                                addEstate = false;
                            }
                        }
                        if(addEstate == true)
                        {
                            nation.GetComponent<NationHandler>().nation.popTypes.Add( new PopType() { culture = poptype.culture, population = poptype.population});
                        }
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Header.SetActive(true);
        // ArmoryButton.SetActive(true);
        // GovernmentButton.SetActive(true);
        // AttackPanel.SetActive(false);
        // TurnButton.SetActive(true);

        AddCountryData();
        
        if(GameManager.instance.battleHasEnded && GameManager.instance.battleWon)
        {
            CountryHandler count = GameObject.Find(GameManager.instance.attackedCountry).GetComponent<CountryHandler>();
            if(GameManager.instance.attackerNation == Country.theTribes.PLAYER.ToString())
            {
                count.country.tribe = Country.theTribes.PLAYER;
            }
            if(GameManager.instance.attackerNation == Country.theTribes.FRANKS.ToString())
            {
                count.country.tribe = Country.theTribes.FRANKS;
            }
            if(GameManager.instance.attackerNation == Country.theTribes.GOTHS.ToString())
            {
                count.country.tribe = Country.theTribes.GOTHS;
            }
            if(GameManager.instance.attackerNation == Country.theTribes.BURGUNDIANS.ToString())
            {
                count.country.tribe = Country.theTribes.BURGUNDIANS;
            }
            if(GameManager.instance.attackerNation == Country.theTribes.POLISH.ToString())
            {
                count.country.tribe = Country.theTribes.POLISH;
            }
            if (GameManager.instance.MilitiaDied > 0)
            {
                count.country.militia = count.country.militia - GameManager.instance.MilitiaDied;
                GameManager.instance.MilitiaDied = 0;
            }
            if (GameManager.instance.FortificationDied > 0)
            {
                count.country.fortifications = count.country.fortifications -  GameManager.instance.FortificationDied;
                GameManager.instance.FortificationDied = 0;
            }
            TintCountries();
        }
        GameManager.instance.AIRecouperate();
        GameManager.instance.Saving();
        GameManager.instance.SaveNation();
        GameManager.instance.filesLoaded = true;
        CheckGains();
        AddPopTypes();
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
                    // print(countryList[i].GetComponent<CountryHandler>().country.pops.totalPopulation);
                    country.GetComponent<CountryHandler>().country.pops.totalPopulation = countryList[i].GetComponent<CountryHandler>().country.pops.totalPopulation;
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

    public void ShowAttackPanel(string description)//, int pops.totalPopulationReward, int expReward)
    {
        // Header.SetActive(false);
        // ArmoryButton.SetActive(false);
        // GovernmentButton.SetActive(false);
        // AttackPanel.SetActive(true);
        // TurnButton.SetActive(false);
        AttackPanel gui = AttackPanel.GetComponent<AttackPanel>();
        gui.descriptionText.text = description;
    }

    public void StartFight()
    {
        // if(GameManager.instance.battleHasEnded && GameManager.instance.battleWon)
        // {
            CountryHandler count = GameObject.Find(GameManager.instance.attackedCountry).GetComponent<CountryHandler>();
            GameManager.instance.archers = count.country.archers;
            GameManager.instance.swordsmen = count.country.swordsmen;
        //}
            GameManager.instance.attackerNation = "PLAYER";
            GameManager.instance.attackerNationcolor = new Color32(0,0,255,255);
            // GameObject[] theArray = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
            // foreach(GameObject nation in theArray)
            // {
            //     if(nation.name == attackerNation)
            //     {
            //         GameManager.instance.attackerNationcolor = nation.GetComponent<NationHandler>().GetNationColor(255);
            //     }    
            // }
            


            GameManager.instance.attackedNation = count.country.tribe.ToString();
            GameManager.instance.attackedNationcolor = count.gameObject.GetComponent<SpriteRenderer>().color;
            GameManager.instance.attackedNationcolor = new Color32(GameManager.instance.attackedNationcolor.r, GameManager.instance.attackedNationcolor.g, GameManager.instance.attackedNationcolor.b, 255);
            GameManager.instance.attackedCountry = count.name;
        
        
        //GameObject player = GameObject.Find("PLAYER");
        foreach( GameObject ac in count.adjacentCountries)
        {
            if(ac.GetComponent<CountryHandler>().country.tribe == Country.theTribes.PLAYER)
            {
                if (count.country.tribe.ToString() != "PLAYER")
                {
                    SceneManager.LoadScene("Fight");
                }
            }
        }
        
    }
}
