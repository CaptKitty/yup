using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

[RequireComponent(typeof(PolygonCollider2D))]

public class CountryHandler : MonoBehaviour
{

    public Country country;

    private SpriteRenderer sprite;


    private Color32 oldColor;
    private Color32 hoverColor;
    //public Color32 startColor;

    public List<GameObject> adjacentCountries = new List<GameObject>();

    private int i;

    void Awake()
    {
        // if(GameManager.instance.filesLoaded == false)
        // {
            LoadBasePopulation();
        // }
        sprite = GetComponent<SpriteRenderer>();
        //sprite.color = startColor;
        Adjacency();
        CountPopulation();
    }
    void Start()
    {
        LoadLaws();
    }
    void OnMouseEnter()
    {
        oldColor = sprite.color;
        hoverColor = new Color32(oldColor.r, oldColor.g, oldColor.b, 190);
        sprite.color = hoverColor;
    }
    void OnMouseExit()
    {
        sprite.color = oldColor;
    }
    void OnMouseUpAsButton()
    {
        GameObject attackbutton = GameObject.Find("AttackButton");
        attackbutton.GetComponent<Button>().interactable = false;
        attackbutton.GetComponent<Button>().enabled = true;

        foreach (GameObject ac in adjacentCountries)
        {
            for (int i = ac.GetComponent<CountryHandler>().adjacentCountries.Count - 1; i > -1; i--)
            {
                if(this.name == ac.GetComponent<CountryHandler>().adjacentCountries[i].gameObject.name)
                {
                    if(country.tribe != Country.theTribes.PLAYER && ac.GetComponent<CountryHandler>().country.tribe.ToString() == "PLAYER")
                    {
                        attackbutton.GetComponent<Button>().interactable = true;
                    }
                    if(country.tribe == Country.theTribes.PLAYER)
                    {
                        attackbutton.GetComponent<Button>().enabled = false;
                    }
                    ShowGUI();
                }
            }
        }
    }
    void OnDrawGizmos()
    {
        country.name = name;
        this.tag = "Country";
    }
    public void TintColor(Color32 color)
    {
        sprite.color = color;
    }
    void ShowGUI()
    {
        CountryManager.instance.ShowAttackPanel( country.name );// + " is owned by the " + country.tribe.ToString());// + ". Are you sure you want to attack them?");// country.moneyReward, country.expReward);
        GameManager.instance.attackedCountry = country.name;
        GameManager.instance.attackedCountryMilitia = country.militia;
        GameManager.instance.attackedCountryFortifications = country.fortifications;
        GameManager.instance.country.name = country.name;
        GameManager.instance.country.tribe = country.tribe;
        GameManager.instance.country.pops.totalPopulation = country.pops.totalPopulation;
        GameManager.instance.country.pops.poplist = country.pops.poplist;
        GameManager.instance.country.recruits = country.recruits;
        GameManager.instance.country.taxIncome = country.taxIncome;
        GameManager.instance.country.militia = country.militia;
        GameManager.instance.country.fortifications = country.fortifications;
        GameManager.instance.battleHasEnded = false;
        GameManager.instance.battleWon = false;
    }
    public void Adjacency()
    {
        float adjacency = 0.75f;
        adjacentCountries = new List<GameObject>();
        adjacentCountries.Clear();

        Collider2D[] Neighbours = Physics2D.OverlapCircleAll(transform.position, adjacency);
        foreach (Collider2D countries in Neighbours)
        {
            
            //print(countries.gameObject.name);
            // print(countries.GetComponent<CountryHandler>().country.tribe.ToString());
            // print(countries.gameObject);
            if (this.transform != countries.transform)
            {
                adjacentCountries.Add(countries.gameObject);
            }
            // print(adjacentCountries.Count);
        }
        while(adjacentCountries.Count <= 2)
        {
            adjacentCountries.Clear();
            adjacency = adjacency * 1.1f;
            foreach (Collider2D countries in Neighbours)
            {
                Neighbours = Physics2D.OverlapCircleAll(transform.position, adjacency);
                //print(countries.gameObject.name);
                // print(countries.GetComponent<CountryHandler>().country.tribe.ToString());
                // print(countries.gameObject);
                if (this.transform != countries.transform)
                {
                    adjacentCountries.Add(countries.gameObject);
                }
                // print(adjacentCountries.Count);
            }
        }
    }
    public void CountPopulation()
    {
        country.pops.totalPopulation = 0;

        foreach (PopType pop in country.pops.poplist)
        {
            country.pops.totalPopulation += pop.population;
        }
    }
    void LoadBasePopulation()
    {
        TextAsset txt = (TextAsset)Resources.Load("test", typeof(TextAsset));
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
                Lines = line.Remove((line.Length-4),4);        
                Lines = sr.ReadLine();
                if ((line.Remove((line.Length-4),4)) == this.name)
                {
                    while(Lines != "}")
                    {
                        string popLine = Lines;
                        Lines = sr.ReadLine();
                        string nameLine = Lines;
                        this.gameObject.GetComponent<CountryHandler>().country.pops.poplist.Add( new PopType() { culture = nameLine, population = int.Parse(popLine)});
                        Lines = sr.ReadLine();
                    }
                }
            }
            catch{}
        }
    }

    void LoadLaws()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in theArray)
        {
            foreach (PopType poptype in country.pops.poplist)
            {
                if(nation.name == country.tribe.ToString())
                {
                    foreach( PopType poptypes in nation.GetComponent<NationHandler>().nation.popTypes)
                    {
                        if(poptypes.culture == poptype.culture)
                        {
                            poptype.growthrate = poptypes.growthrate;
                            poptype.draftrate = poptypes.draftrate;
                            poptype.taxrate = poptypes.taxrate;
                        }
                    }
                }
            }
        }
        GameObject.Find("Countrymanager").GetComponent<CountryManager>().CheckGains();
    }

    public void BuildMilitia(int number)
    {
        country.militia += number;
        ShowGUI();
    }

    public void BuildFortification(int number)
    {
        country.fortifications += number;   
        ShowGUI();
    }
}
