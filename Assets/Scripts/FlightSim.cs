using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FlightSim : MonoBehaviour
{

    public int nextUpdate=1;
    public float nextUpdates=0.25f;
    
    public Text Attacker;
    public Text Defender;
    public Text Battlefield;
    
    public GameObject BattlefieldField;
    public GameObject BattlefieldSiege;
    public GameObject Tower;
    public Owner Owner;
    private GameObject tower;
    
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.instance.battleHasEnded = false;
        GameManager.instance.battleWon = false;

        Attacker.text = GameManager.instance.attackerNation;
        Defender.text = GameManager.instance.attackedNation;
        Battlefield.text = GameManager.instance.attackedCountry;
        BattlefieldField.SetActive(false);
        BattlefieldSiege.SetActive(false);

        if(GameManager.instance.attackedCountryFortifications > 0)
        {
            BattlefieldSiege.SetActive(true);
            
            for(int towers = GameManager.instance.attackedCountryFortifications; towers > 0; towers--)
            {
                tower = Instantiate(Tower) as GameObject;
                tower.GetComponent<SpriteRenderer>().color = Owner.Color;
                int rng = Random.Range (0,2);
                if(rng == 0)
                {
                    tower.transform.position = new Vector2(Random.Range (-20,-6), 14);
                }
                if(rng == 1)
                {
                    tower.transform.position = new Vector2(Random.Range (6,20), 14);
                }
                //towers = towers-1;
            }
        }
        else
        {
            BattlefieldField.SetActive(true);
        }
        AstarPath.active.Scan();
    }

    void Update()
    {
        
        if(Time.time>=nextUpdates)
        {
            nextUpdates=Mathf.FloorToInt(Time.time)+0.25f;
            AstarPath.active.Scan();
        }
        if(Time.time>=nextUpdate)
        {
            nextUpdate=Mathf.FloorToInt(Time.time)+1;
            UpdateEverySecond();
        }
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    void UpdateEverySecond()
    {
        GameObject[] EnemyAlive;
        EnemyAlive = GameObject.FindGameObjectsWithTag("Enemy");
        
        List<GameObject> enemies = new List<GameObject>();
        enemies.Clear();
        foreach( GameObject Enemy in EnemyAlive)
        {
            if(Enemy.layer != 8)
            {
                enemies.Add(Enemy);
            }
        }

        int EnemyAliveNumber = enemies.Count;

        GameObject[] AllyAlive;
        AllyAlive = GameObject.FindGameObjectsWithTag("Ally");
        int AllyAliveNumber = AllyAlive.Length;

        if(EnemyAliveNumber == 0) {
            GameManager.instance.battleWon = true;
            GameManager.instance.battleHasEnded = true;
            //print(AllyAliveNumber + " Allies Alive");
            //print(EnemyAliveNumber + " Enemies Alive");
        }

        if(AllyAliveNumber == 0) {
            GameManager.instance.battleWon = false;
            GameManager.instance.battleHasEnded = true;
            //print(AllyAliveNumber + " Allies Alive");
            //print(EnemyAliveNumber + " Enemies Alive");
        }
        if(GameManager.instance.battleHasEnded == true)
        {
            SceneManager.LoadScene("Main");
        }
    }

    // Update is called once per frame
    IEnumerator Fight()
    {
        yield return new WaitForSeconds(1);
        int num = Random.Range(0,2);

        if(num == 0)
        {
            GameManager.instance.battleWon = false;
        }
        else
        {
            GameManager.instance.battleWon = true;
        }

        GameManager.instance.battleHasEnded = true;
        SceneManager.LoadScene("Main");
    }
}
