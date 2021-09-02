using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class NationManager : MonoBehaviour
{
    public static NationManager instance;

    public Nation nation;

    public GameObject Nation;
    
    private string Tag = "Nation";

    public List<GameObject> nationList = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // GameObject[] theArray = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
            // foreach(GameObject country in theArray)
            // {
            //     GameObject Nations = Instantiate(Nation) as GameObject;
            //     Nations.GetComponent<NationHandler>().nation.tribe = country.GetComponent<CountryHandler>().country.tribe.ToString();
            //     Nations.name = Nations.GetComponent<NationHandler> ().nation.tribe.ToString();
            //     Nations.transform.parent = GameObject.Find("NationManager").transform;
            //     Nations.tag = Tag;
            //     Nations.GetComponent<NationHandler>().nation.name = Nations.GetComponent<NationHandler> ().nation.tribe.ToString();
            //     Nations.GetComponent<NationHandler>().nation.swordsmen = country.GetComponent<CountryHandler>().country.swordsmen;
            //     Nations.GetComponent<NationHandler>().nation.archers = country.GetComponent<CountryHandler>().country.archers;
            //     Nations.GetComponent<NationHandler>().nation.taxTreasury = 100;
            //     Nations.GetComponent<NationHandler>().nation.AttackModifier = country.GetComponent<CountryHandler>().country.AttackModifier;
            //     Nations.GetComponent<NationHandler>().nation.HealthModifier = country.GetComponent<CountryHandler>().country.HealthModifier;
            //     Nations.GetComponent<NationHandler>().nation.InfantryHealthModifier = 1;
            //     Nations.GetComponent<NationHandler>().nation.SizeModifier = 1;
            //     Nations.GetComponent<NationHandler>().nation.ArcherRangeModifier = 0;

            //     if(country.GetComponent<CountryHandler>().country.tribe == Country.theTribes.FRANKS)
            //     {
            //         //Nations.GetComponent<NationHandler>().nation.color = new Color32(255,0,0,255);
            //         Nations.GetComponent<NationHandler>().nation.colorr = 255;
            //         Nations.GetComponent<NationHandler>().nation.colorg = 0;
            //         Nations.GetComponent<NationHandler>().nation.colorb = 0;
            //     }
            //     if(country.GetComponent<CountryHandler>().country.tribe == Country.theTribes.POLISH)
            //     {
            //         //Nations.GetComponent<NationHandler>().nation.color = new Color32(175,15,15,255);
            //         Nations.GetComponent<NationHandler>().nation.colorr = 175;
            //         Nations.GetComponent<NationHandler>().nation.colorg = 15;
            //         Nations.GetComponent<NationHandler>().nation.colorb = 15;
            //     }
            //     if(country.GetComponent<CountryHandler>().country.tribe == Country.theTribes.BURGUNDIANS)
            //     {
            //         //Nations.GetComponent<NationHandler>().nation.color = new Color32(0,255,0,255);
            //         Nations.GetComponent<NationHandler>().nation.colorr = 0;
            //         Nations.GetComponent<NationHandler>().nation.colorg = 255;
            //         Nations.GetComponent<NationHandler>().nation.colorb = 0;
            //     }
            //     if(country.GetComponent<CountryHandler>().country.tribe == Country.theTribes.GOTHS)
            //     {
            //         //Nations.GetComponent<NationHandler>().nation.color = new Color32(128,128,0,255);
            //         Nations.GetComponent<NationHandler>().nation.colorr = 128;
            //         Nations.GetComponent<NationHandler>().nation.colorg = 128;
            //         Nations.GetComponent<NationHandler>().nation.colorb = 0;
            //     }
            //     if(country.GetComponent<CountryHandler>().country.tribe == Country.theTribes.PLAYER)
            //     {
            //         //Nations.GetComponent<NationHandler>().nation.color = new Color32(0,0,255,255);
            //         Nations.GetComponent<NationHandler>().nation.colorr = 0;
            //         Nations.GetComponent<NationHandler>().nation.colorg = 0;
            //         Nations.GetComponent<NationHandler>().nation.colorb = 255;
            //     }
            //     foreach (GameObject nations in nationList)
            //     {
            //         if(Nations.name == nations.name)
            //         {
            //             Destroy(Nations);
            //         }
            //     }
                
            //     nationList.Add(Nations);
            // }
            LoadNations();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    
    void LoadNations()
    {
        TextAsset txt = (TextAsset)Resources.Load("nations", typeof(TextAsset));
        string path = txt.text;
        using var sr = new StringReader(path);
        int count = 0;
        string line;
        string Lines;
        while ((line = sr.ReadLine()) != null)
        {
            count++;
            try
            {
                //string Lines = line.Remove((line.Length-4),4);
                //print(line);
                Lines = sr.ReadLine();
                //if ((line.Remove((line.Length-4),4)) == this.name)
                // if ( Lines.Length == 3)
                // {
                    while(Lines != "}")
                    {
                        GameObject Nations = Instantiate(Nation) as GameObject;
                        Nations.transform.parent = GameObject.Find("NationManager").transform;
                        Nations.GetComponent<NationHandler>().nation.name = line.Remove((line.Length-4),4);
                        Nations.name = line.Remove((line.Length-4),4);
                        Nations.GetComponent<NationHandler>().nation.tribe = line.Remove((line.Length-4),4);
                        Nations.tag = Tag;
                        //print(Lines);
                        string[] split = Lines.Split('=',' ');
                        foreach (var sub in split)
                        {
                            //print(sub);
                            if (split[0] == "AttackModifier")
                            {
                                Nations.GetComponent<NationHandler>().nation.AttackModifier = float.Parse(split[3]);
                            }
                        }
                        Lines = sr.ReadLine();
                        //print(Lines);
                        split = Lines.Split('=',' ');
                        foreach (var sub in split)
                        {
                            if (split[0] == "HealthModifier")
                            {
                                Nations.GetComponent<NationHandler>().nation.HealthModifier = float.Parse(split[3]);
                            }
                        }
                        Lines = sr.ReadLine();
                        //print(Lines);
                        split = Lines.Split('=',' ');
                        foreach (var sub in split)
                        {
                            if (split[0] == "InfantryHealthModifier")
                            {
                                Nations.GetComponent<NationHandler>().nation.InfantryHealthModifier = float.Parse(split[3]);
                            }
                        }
                        Lines = sr.ReadLine();
                        //print(Lines);
                        split = Lines.Split('=',' ');
                        foreach (var sub in split)
                        {
                            if (split[0] == "ArcherRangeModifier")
                            {
                                Nations.GetComponent<NationHandler>().nation.ArcherRangeModifier = float.Parse(split[3]);
                            }
                        }
                        Lines = sr.ReadLine();
                        nationList.Add(Nations);
                    }
                // }
            }
            catch{}
        }
    }

    void Start()
    {
        if(GameObject.Find("FactionScript").GetComponent<FactionScript>().faction == "A")
        {
            GameObject.Find("PLAYER").GetComponent<NationHandler>().nation.InfantryHealthModifier += 0.25f;
        }
        if(GameObject.Find("FactionScript").GetComponent<FactionScript>().faction == "B")
        {
            GameObject.Find("PLAYER").GetComponent<NationHandler>().nation.ChargeModifier += 0.10f;
        }
        if(GameObject.Find("FactionScript").GetComponent<FactionScript>().faction == "C")
        {
            GameObject.Find("PLAYER").GetComponent<NationHandler>().nation.ArcherRangeModifier += 1f;
        }

        // GameObject[] theArray = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        // foreach(GameObject nation in theArray)
        // {
        //     GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
        //     foreach(GameObject country in theArrays)
        //     {
        //         nation.GetComponent<NationHandler>().nation.color = country.GetComponent<SpriteRenderer> ().color;
        //     }
        // }
    }
    void Update()
    {
        for (int i = nationList.Count - 1; i > -1; i--)
        {
            if (nationList[i] == null)
            {
                nationList.RemoveAt(i);
            }
        }
    }
}
