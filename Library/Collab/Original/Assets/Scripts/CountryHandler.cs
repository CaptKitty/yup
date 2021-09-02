using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

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
        if(GameManager.instance.filesLoaded == false)
        {
            LoadBasePopulation();
        }
        sprite = GetComponent<SpriteRenderer>();
        //sprite.color = startColor;
        Adjacency();
        CountPopulation();
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
        //print(this.name);
        //print(adjacentCountries.Count);
        foreach (GameObject ac in adjacentCountries)
        {
            //print(ac.name);
            //print(ac.GetComponent<CountryHandler>().adjacentCountries.Count);
            for (int i = ac.GetComponent<CountryHandler>().adjacentCountries.Count - 1; i > -1; i--)
            {
                //print(ac.name);
                if(this.name == ac.GetComponent<CountryHandler>().adjacentCountries[i].gameObject.name)
                {
                    //print(this.name);
                    //print(ac.GetComponent<CountryHandler>().adjacentCountries[i].gameObject.name);
                    //print(ac.name);
                    if(country.tribe != Country.theTribes.PLAYER && ac.GetComponent<CountryHandler>().country.tribe.ToString() == "PLAYER")
                    {
                        //ShowGUI();
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
        CountryManager.instance.ShowAttackPanel("This country is owned by the " + country.tribe.ToString());// + ". Are you sure you want to attack them?");// country.moneyReward, country.expReward);
        GameManager.instance.attackedCountry = country.name;
        GameManager.instance.country.tribe = country.tribe;
        GameManager.instance.country.pops.totalPopulation = country.pops.totalPopulation;
        GameManager.instance.country.recruits = country.recruits;
        GameManager.instance.country.taxIncome = country.taxIncome;
        GameManager.instance.battleHasEnded = false;
        GameManager.instance.battleWon = false;
    }
    public void Adjacency()
    {
        adjacentCountries = new List<GameObject>();
        adjacentCountries.Clear();

        Collider2D[] Neighbours = Physics2D.OverlapCircleAll(transform.position, 1.5f);
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
    }
    void CountPopulation()
    {
        country.pops.totalPopulation = 0;

        foreach (PopType pop in country.pops.poplist)
        {
            country.pops.totalPopulation += pop.population;
        }
    }
    void LoadBasePopulation()
    {
        // File.Delete(Application.persistentDataPath + "/test.txt");
        // string path = Application.persistentDataPath + "/test.txt";
        // print(path);
        // //Write some text to the test.txt file
        // StreamWriter writer = new StreamWriter(path, true);
        // GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
        // foreach(GameObject array in theArrays)
        // {
        //     writer.WriteLine(array.name + " = {");
        //     writer.WriteLine("1000");
        //     writer.WriteLine("Dutch");
        //     writer.WriteLine("}");
        // }
        // writer.Close();



        // TextAsset txt = (TextAsset)Resources.Load("test", typeof(TextAsset));
        // //string content = txt.text
        // string path = txt.text;      //Application.persistentDataPath + "/.txt";
        //string path = Application.persistentDataPath + "/test.txt";
        string path = "Assets/Resources/test.txt";
        StreamReader reader = new StreamReader(path);
        
        // GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
        // foreach(GameObject array in theArrays)
        // {
            //array.GetComponent<CountryHandler>().country.pops.poplist.Add( new PopType() { culture = Polish, population = polish});
            //print(array);
        foreach (string line in File.ReadLines(path))
        { 
            
            if (line.Contains(name))
            {
                string Lines = line.Remove((line.Length-4),4);
                while (Lines != line)
                {
                    //string Lines = line.Remove((line.Length-4),4);
                    Lines = reader.ReadLine();
                }
                //Lines = reader.ReadLine();
                //print(Lines);
                Lines = reader.ReadLine();
                while(Lines != "}")
                {
                    //print(Lines); //east pommerania???
                    string popLine = Lines;
                    //print(popLine + " popLine");
                    //print(Lines); //1000
                    Lines = reader.ReadLine();
                    //print(Lines);
                    string nameLine = Lines;
                    //print(nameLine + " nameLine");
                    this.gameObject.GetComponent<CountryHandler>().country.pops.poplist.Add( new PopType() { culture = nameLine, population = int.Parse(popLine)});
                    Lines = reader.ReadLine();
                }
            }
        }
    }
}
