using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;

    public string attackedCountry;
    public string attackedNation;
    public Color32 attackedNationcolor;
    public Color32 attackerNationcolor;
    public string attackerNation;
    public int attackedCountryMilitia;
    public int attackedCountryFortifications;
    public int MilitiaDied;
    public int FortificationDied;
    public string nationsturn;
    public int nationsturnnumber = 0;
    public int year = 1000;
    public Country country;
    
    public bool battleHasEnded;
    public bool battleWon;

    public bool filesLoaded = false;
    
    public int archers;
    public int swordsmen;
    
    public int exp;
    public int totalPopulation;
    
    private int defendernumbers;
    private int attackernumbers;

    public List<GameObject> possibleTargets = new List<GameObject>();
    public List<GameObject> AINations = new List<GameObject>();

    [System.Serializable]
    public class SaveData
    {
        public List<Country> savedCountries = new List<Country>();
        public List<Nation> savedNations = new List<Nation>();
        public int cur_recruits;
        public int cur_treasury;
        public int cur_army_ratio;
    }

    void Awake()
    {
        if (instance == null)
        {
            //DeleteSaveFile();
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);

        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    void Start()
    {
        
    }

    public void Saving()
    {
        //print(CountryManager.instance.countryList[2].GetComponent<CountryHandler>().country.totalPopulation);
        SaveData data = new SaveData();
        for (int i = 0; i < CountryManager.instance.countryList.Count; i++)
        {
            data.savedCountries.Add(CountryManager.instance.countryList[i].GetComponent<CountryHandler>().country);
        }
        for (int i = 0; i < NationManager.instance.nationList.Count; i++)
        {
            data.savedNations.Add(NationManager.instance.nationList[i].GetComponent<NationHandler>().nation);
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/Savefile.jay", FileMode.Create);

        //print(Application.persistentDataPath);

        bf.Serialize(stream, data);
        stream.Close();
        //print("Saved Game");
        //print(CountryManager.instance.countryList[2].GetComponent<CountryHandler>().country.totalPopulation);
    }

    public void SaveNation()
    {
        //print(CountryManager.instance.countryList[2].GetComponent<CountryHandler>().country.totalPopulation);
        SaveData data = new SaveData();
        for (int i = 0; i < NationManager.instance.nationList.Count; i++)
        {
            data.savedNations.Add(NationManager.instance.nationList[i].GetComponent<NationHandler>().nation);
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/SaveNations.jay", FileMode.Create);

        //print(Application.persistentDataPath);
        bf.Serialize(stream, data);
        stream.Close();
        //print("Saved Game");
        //print(CountryManager.instance.countryList[2].GetComponent<CountryHandler>().country.totalPopulation);
    }
    
    public void Loading()
    {
        //print(CountryManager.instance.countryList[2].GetComponent<CountryHandler>().country.totalPopulation);
        if(File.Exists(Application.persistentDataPath + "/Savefile.jay"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/Savefile.jay", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(stream);
            stream.Close();

            for(int i = 0; i < data.savedCountries.Count; i++)
            {
                for(int j = 0; j < CountryManager.instance.countryList.Count; j++)
                {
                    if(data.savedCountries[i].name == CountryManager.instance.countryList[j].GetComponent<CountryHandler>().country.name)
                    {
                        CountryManager.instance.countryList[j].GetComponent<CountryHandler>().country = data.savedCountries[i];
                    }
                }
            }
            bf = new BinaryFormatter();
            stream = new FileStream(Application.persistentDataPath + "/SaveNations.jay", FileMode.Open);

            data = (SaveData)bf.Deserialize(stream);
            stream.Close();
            for(int i = 0; i < data.savedNations.Count; i++)
            {
                for(int j = 0; j < NationManager.instance.nationList.Count; j++)
                {
                    if(data.savedNations[i].name == NationManager.instance.nationList[j].GetComponent<NationHandler>().nation.name)
                    {
                        NationManager.instance.nationList[j].GetComponent<NationHandler>().nation = data.savedNations[i];
                    }
                }
            }

            CountryManager.instance.TintCountries();
            CountryManager.instance.PopulateCountries();
            //CountryManager.instance.CheckGains();
            //print("Game Loaded");
        }
        else
        {
            //print("No savefile found");
        }
        //print(CountryManager.instance.countryList[2].GetComponent<CountryHandler>().country.totalPopulation);
    }

    public void DeleteSaveFile()
    {
        if(File.Exists(Application.persistentDataPath + "/Savefile.jay"))
        {
            File.Delete(Application.persistentDataPath + "/Savefile.jay");
            //print("Savefile Deleted");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AIturn()
    {
        AINations.Clear();
        GameObject[] NationUpdate = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach( GameObject nation in NationUpdate)
        {
            if(nation.name != "PLAYER")
            {
                AINations.Add(nation);
            }
        }
        if(nationsturnnumber >= AINations.Count)
        {
            nationsturnnumber = 0;
        }
        nationsturn = AINations[nationsturnnumber].name;
        nationsturnnumber = nationsturnnumber + 1;
        AIRecruitment();
        AIAttack();
 
    }

    public void AIRecruitment()
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
                        if (unit.GetComponent<UnitHandler>().units.name != "Custom")
                        {
                            if (unit.transform.parent.GetComponent<NationHandler>().nation.taxIncome >= 0 && unit.transform.parent.GetComponent<NationHandler>().nation.taxTreasury >= 10 && unit.transform.parent.GetComponent<NationHandler>().nation.totalRecruits >= 100 && unit.transform.parent.GetComponent<NationHandler>().nation.recruitsIncome >= 0)
                            {
                                unit.transform.parent.GetComponent<NationHandler>().nation.taxTreasury -= 10;
                                unit.transform.parent.GetComponent<NationHandler>().nation.totalRecruits -= 100;
                                unit.GetComponent<UnitHandler>().units.number += 1;
                            }
                        }
                    }
                }
            }          
        }
    }
    public void AIRecouperate()
    {
        GameObject[] NationUpdate = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in NationUpdate)
        {
            if (nation.GetComponent<NationHandler>().nation.tribe.ToString() == "PLAYER")
            {
                GameObject[] UnitUpdate = GameObject.FindGameObjectsWithTag("Units") as GameObject[];
                foreach(GameObject unit in UnitUpdate)
                {
                    if (unit.GetComponent<UnitHandler>().units.number < 0)
                    {
                        unit.GetComponent<UnitHandler>().units.number = 0;
                    }
                    if (unit.transform.parent.GetComponent<NationHandler>().nation.tribe.ToString() != "PLAYER")
                    {
                        if (unit.GetComponent<UnitHandler>().units.number < 5 && unit.GetComponent<UnitHandler>().units.name != "Custom")
                        {
                            unit.GetComponent<UnitHandler>().units.number = 5;
                        }
                    }
                }
            }          
        }
    }

    void AIAttack()
    {
        
        possibleTargets.Clear();
        GameObject[] NationUpdate = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in NationUpdate)
        {
            if (nation.GetComponent<NationHandler>().nation.tribe.ToString() == nationsturn && nation.GetComponent<NationHandler>().nation.tribe.ToString() != "PLAYER")
            {
                GameObject[] countryA = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
                foreach(GameObject country in countryA)
                {
                    if(country.GetComponent<CountryHandler>().country.tribe.ToString() == nation.GetComponent<NationHandler>().nation.tribe.ToString())
                    {
                        foreach(GameObject ac in country.GetComponent<CountryHandler>().adjacentCountries)
                        {
                            if(ac.GetComponent<CountryHandler>().country.tribe.ToString() != country.GetComponent<CountryHandler>().country.tribe.ToString())
                            {
                                possibleTargets.Add(ac);
                            }
                        }
                    }
                }
                if(possibleTargets.Count >= 0)
                {
                    int rng = Random.Range (0, possibleTargets.Count);
                    GameObject enemycountry = possibleTargets[rng];

                    GameManager.instance.attackerNation = nation.name;
                    GameObject[] countryB = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
                    foreach(GameObject country in countryB)
                    {
                        GameManager.instance.attackerNationcolor = country.GetComponent<SpriteRenderer>().color;
                        GameManager.instance.attackerNationcolor = new Color32(GameManager.instance.attackerNationcolor.r, GameManager.instance.attackerNationcolor.g, GameManager.instance.attackerNationcolor.b, 255);
                    }
                    GameManager.instance.attackedNation = enemycountry.GetComponent<CountryHandler>().country.tribe.ToString();
                    GameObject[] countryC = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
                    foreach(GameObject nations in countryC)
                    {
                        Color32 nationcolor = enemycountry.GetComponent<SpriteRenderer>().color;
                        GameManager.instance.attackedNationcolor = new Color32(nationcolor.r, nationcolor.g, nationcolor.b, 255);
                    }
                    
                    GameManager.instance.attackedCountry = enemycountry.name;
                    GameManager.instance.attackedCountryMilitia = enemycountry.GetComponent<CountryHandler>().country.militia;
                    GameManager.instance.attackedCountryFortifications = enemycountry.GetComponent<CountryHandler>().country.fortifications;

                    GameObject[] Units = GameObject.FindGameObjectsWithTag("Units") as GameObject[];
                    foreach(GameObject unit in Units)
                    {
                        if(unit.transform.parent.GetComponent<NationHandler>().nation.name == attackerNation)
                        {
                            attackernumbers =+ unit.GetComponent<UnitHandler>().units.number;
                        }
                        if(unit.transform.parent.GetComponent<NationHandler>().nation.name == attackedNation)
                        {
                            defendernumbers =+ unit.GetComponent<UnitHandler>().units.number;
                        }
                    }
                    if (attackernumbers >= defendernumbers*1.2)
                    {
                        SceneManager.LoadScene("Fight");
                    }
                }
            } 
        }
    }
}
