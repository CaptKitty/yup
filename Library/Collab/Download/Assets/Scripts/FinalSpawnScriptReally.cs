using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSpawnScriptReally : MonoBehaviour
{
    public Owner Owner;
    public GameObject BasicTrooper;

    public int Spawnline;
    public int SpawnlineSupport;

    SpriteRenderer acolor;

    private string owner;
    //private string unitname = "BasicTrooper";
    private GameObject Unit;
    private GameObject units;
    private GameObject PickedProjectile;
    //private Animator ArcherAnimation;
    private Sprite unitssprite;
    private SpriteRenderer spriterenderer;

    private string unitsname;
    private bool unitsranged;
    private float unitsattackrange;
    private int unitsattackdamage;
    private float unitsattackRate;
    private Units.Weapons unitweapon;
    private Units.Armor unitarmor;
    private Units.Mounts unitmount;
    private int unitsmaxhealth;
    private float unitsmovementspeed;
    private float unitsmaximumspeed;
    private float unitschargebonus;
    private string unitsprojectile;
    private float unitsprojectileSpeed;
    private int spawnrange;

    void Start()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nations in theArray)
        {
            if(GameManager.instance.country.tribe.ToString() == nations.GetComponent<NationHandler>().nation.tribe.ToString())
            {
                owner = GameManager.instance.country.tribe.ToString();
                
            }

        }
        GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Units") as GameObject[];
        foreach(GameObject units in theArrays)
        {
            if(Owner.Nation.ToString() == units.transform.parent.GetComponent<NationHandler>().nation.tribe.ToString())
            {
                //print(units.GetComponent<UnitHandler>().units.number + " " + units.GetComponent<UnitHandler>().units.name);
                int spawnunits = units.GetComponent<UnitHandler>().units.number;
                spawnrange = 0 - units.GetComponent<UnitHandler>().units.number;
                unitsname = units.GetComponent<UnitHandler>().units.name;
                unitsranged = units.GetComponent<UnitHandler>().units.ranged;
                unitsattackrange = units.GetComponent<UnitHandler>().units.attackRange;
                unitsprojectileSpeed = units.GetComponent<UnitHandler>().units.projectileSpeed;
                unitsattackdamage = units.GetComponent<UnitHandler>().units.attackDamage;
                unitsattackRate = units.GetComponent<UnitHandler>().units.attackRate;
                unitsmaxhealth = units.GetComponent<UnitHandler>().units.maxhealth;
                unitsmovementspeed = units.GetComponent<UnitHandler>().units.MovementSpeed;
                unitsmaximumspeed = units.GetComponent<UnitHandler>().units.maximumspeed;
                unitschargebonus = units.GetComponent<UnitHandler>().units.chargebonus;
                unitsprojectile = units.GetComponent<UnitHandler>().units.projectile;
                unitweapon = units.GetComponent<UnitHandler>().units.weapon;
                unitarmor = units.GetComponent<UnitHandler>().units.armor;
                unitmount = units.GetComponent<UnitHandler>().units.mount;
                while (spawnunits > 0)
                {
                    Spawner();
                    spawnunits -= 1;
                }
            }
        }

        if( Owner.Player == false)
        {
            if(GameManager.instance.attackedCountryMilitia > 0)
            {
                int spawnunits = GameManager.instance.attackedCountryMilitia;
                spawnrange = 0 - spawnunits;
                unitsname =  "Militia";
                unitsranged = false;
                unitsattackrange = 1f;
                unitsprojectileSpeed = 0f;
                unitsattackdamage = 15;
                unitsattackRate = 1;
                unitsmaxhealth = 50;
                unitsmovementspeed = 2000f;
                //unitsprojectile = "yes";
                while (spawnunits > 0)
                {
                    Spawner();
                    spawnunits -= 1;
                }
            }
        }
    }

    void Spawner()
    {
        Unit = Instantiate(BasicTrooper) as GameObject;
        Unit.name = unitsname;
        Unit.GetComponent<Enemy> ().IsRanged = unitsranged;
        Unit.GetComponent<Enemy> ().attackRange = unitsattackrange;
        Unit.GetComponent<Enemy> ().projectileSpeed = unitsprojectileSpeed;
        Unit.GetComponent<Enemy> ().attackDamage = unitsattackdamage;
        Unit.GetComponent<Enemy> ().attackRate = unitsattackRate;
        Unit.GetComponent<Enemy> ().maxHealth = unitsmaxhealth;
        Unit.GetComponent<Enemy> ().speed = unitsmovementspeed;
        Unit.GetComponent<Enemy> ().maximumspeed = unitsmaximumspeed;
        Unit.GetComponent<Enemy> ().chargebonus = unitschargebonus;
        
        Unit.tag = Owner.Tag;
        Unit.GetComponent<Enemy> ().EnemyTag = Owner.EnemyTag;
        Unit.GetComponent<Enemy> ().Nation = Owner.Nation;
        Unit.GetComponent<Enemy> ().detectRange = Owner.detectRange;
        Unit.GetComponent<Enemy> ().weapon = unitweapon;
        Unit.GetComponent<Enemy> ().armor = unitarmor;
        Unit.GetComponent<Enemy> ().mount = unitmount;
        GetAnimator();
        GetStatsall();
        if(Unit.GetComponent<Enemy> ().IsRanged == true)
        {
            GetStatsRanged();
        }
        if(Unit.GetComponent<Enemy> ().IsRanged == false)
        {
            GetStatsMelee();
        }
        if(Owner.Player == false)
        {
            Unit.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
    }

    void GetStatsall()
    {
        float AttackModifier = Unit.GetComponent<Enemy> ().attackDamage * Owner.AttackModifier;
        int TotalAttack = (int) AttackModifier;
        Unit.GetComponent<Enemy> ().attackDamage = TotalAttack;

        float HealthModifier = Unit.GetComponent<Enemy> ().maxHealth * Owner.HealthModifier;
        int TotalHealth = (int) HealthModifier;
        Unit.GetComponent<Enemy> ().maxHealth = TotalHealth;

        float MovementSpeedModifier = Unit.GetComponent<Enemy> ().speed * Owner.MovementSpeedModifier;
        Unit.GetComponent<Enemy> ().speed = MovementSpeedModifier;
        
        float ChargeModifier = Unit.GetComponent<Enemy> ().chargebonus * Owner.ChargeModifier;
        Unit.GetComponent<Enemy> ().chargebonus = ChargeModifier;
    }
    void GetStatsRanged()
    {
        
        Unit.transform.position = new Vector2(spawnrange, Random.Range(SpawnlineSupport, SpawnlineSupport));
        
        spawnrange = spawnrange+2;

        //print(Unit.GetComponent<Enemy> ().attackRange + Owner.ArcherRangeModifier);
        float RangeModifier = Unit.GetComponent<Enemy> ().attackRange + Owner.ArcherRangeModifier;
        int TotalRange = (int) RangeModifier;
        Unit.GetComponent<Enemy> ().attackRange = TotalRange;
        GetProjectile();
        Unit.GetComponent<Enemy> ().Projectile = PickedProjectile;

        //Unit.GetComponent<Enemy> ().animator = ArcherAnimation;
        //Unit.GetComponent<SpriteRenderer>().sprite = unitssprite;
        // spriterenderer = Unit.GetComponent<SpriteRenderer>();
        // spriterenderer.sprite = unitssprite;
        // Sprite UnitSprite = Resources.Load <Sprite> ("army_block_archer");
        // SpriteRenderer renderer = Unit.AddComponent<SpriteRenderer>();
        // renderer.sprite = UnitSprite;
        Unit.GetComponent<SpriteRenderer>().color = Owner.Color;
    }
    void GetStatsMelee()
    {

        Unit.transform.position = new Vector2(spawnrange, Random.Range(Spawnline, Spawnline));
        //Unit.transform.position = new Vector2(spawnrange, Random.Range(Spawnline-1, Spawnline+1));

        spawnrange = spawnrange+2;

        float HealthModifier = Unit.GetComponent<Enemy> ().maxHealth * Owner.InfantryHealthModifier; //* Owner.HealthModifier 
        int TotalHealth = (int) HealthModifier;
        Unit.GetComponent<Enemy> ().maxHealth = TotalHealth;

        
        // //Unit.GetComponent<Enemy> ().animator = ArcherAnimation;
        // //Unit.GetComponent<SpriteRenderer>().sprite = unitssprite;
        // //spriterenderer = Unit.GetComponent<SpriteRenderer>();
        // //spriterenderer.sprite = unitssprite;
        // Sprite UnitSprite = Resources.Load <Sprite> ("army_block_infantry");
        // SpriteRenderer renderer = Unit.AddComponent<SpriteRenderer>();
        // renderer.sprite = UnitSprite;
        Unit.GetComponent<SpriteRenderer>().color = Owner.Color;

        // try 
        // // {
        //     Transform Child = this.gameObject.transform.GetChild(2);
        
        //     Child.gameObject.AddComponent<SpriteRenderer>();
        //     Sprite WeaponSprite = Resources.Load <Sprite> ("pikes 1");
        //     Child.GetComponent<SpriteRenderer>().sprite = WeaponSprite;
        //     Child.gameObject.AddComponent<BoxCollider2D>();
            //Child.GetComponent<BoxCollider2D>();
            // Child.GetComponent<SpriteRenderer>().SetActive(true);
            // Child.GetComponent<BoxCollider2D>().SetActive(true);
        // }
        // catch{}
    }
    void GetProjectile()
    {
        if(unitsprojectile == "arrow")
        {
            PickedProjectile = (GameObject) Resources.Load("arrow");
            //print(PickedProjectile);
        }
        else
        {
            PickedProjectile = (GameObject) Resources.Load("arrow");
            //print(PickedProjectile + "default");
        }
    }
    void GetAnimator()
    {
        SpriteRenderer renderer = Unit.AddComponent<SpriteRenderer>();
        Sprite CUnitSprite;
        SpriteRenderer Crenderer;
        GameObject Child;

        if(Unit.GetComponent<Enemy> ().IsRanged == true && Unit.name != "Tower")
        {
            Sprite UnitSprite = Resources.Load <Sprite> ("army_block_archer");
            renderer.sprite = UnitSprite;
        }
        if(Unit.GetComponent<Enemy> ().IsRanged == false)
        {
            Sprite UnitSprite = Resources.Load <Sprite> ("army_block_infantry");
            renderer.sprite = UnitSprite;
        }
        if(Unit.name == "Tower")
        {
            Sprite UnitSprite = Resources.Load <Sprite> ("Fortified Archer");
            renderer.sprite = UnitSprite;
        }
        if(Unit.GetComponent<Enemy> ().mount != Units.Mounts.None)
        {
            Sprite UnitSprite = Resources.Load <Sprite> ("army_block_empty");
            renderer.sprite = UnitSprite;
            if(Unit.GetComponent<Enemy> ().mount == Units.Mounts.Horse)
            {
                Child = Unit.gameObject.transform.GetChild(0).gameObject;
                Crenderer = Child.AddComponent<SpriteRenderer>();
                CUnitSprite = Resources.Load <Sprite> ("horse_icon");
                Crenderer.sprite = CUnitSprite;
                Child.transform.localScale = new Vector2(0.75f, 0.75f);
                Child.transform.position = new Vector3(0.25f, -0.10f,0);
                Crenderer.sortingOrder = 1;
                if(Unit.GetComponent<Enemy> ().IsRanged == false)//if (Unit.GetComponent<Enemy> ().weapon == Units.Weapons.Sword)
                {
                    Child = Unit.gameObject.transform.GetChild(1).gameObject;
                    Crenderer = Child.AddComponent<SpriteRenderer>();
                    CUnitSprite = Resources.Load <Sprite> ("sword_icon");
                    Crenderer.sprite = CUnitSprite;
                    Child.transform.localScale = new Vector2(0.75f, 0.75f);
                    Child.transform.position = new Vector3(-0.25f, 0.10f,0);
                    Crenderer.sortingOrder = 1;
                }
                if(Unit.GetComponent<Enemy> ().IsRanged == true)//if (Unit.GetComponent<Enemy> ().weapon == Units.Weapons.Longbow)
                {
                    Child = Unit.gameObject.transform.GetChild(1).gameObject;
                    Crenderer = Child.AddComponent<SpriteRenderer>();
                    CUnitSprite = Resources.Load <Sprite> ("longbow_icon");
                    Crenderer.sprite = CUnitSprite;
                    Child.transform.localScale = new Vector2(0.75f, 0.75f);
                    Child.transform.position = new Vector3(-0.25f, 0.10f,0);
                    Crenderer.sortingOrder = 1;
                }
            }
            // Sprite UnitSprite = Resources.Load <Sprite> ("army_block_horse");
            // renderer.sprite = UnitSprite;
        }
        // else
        // {
        //     unitssprite = Resources.Load<Sprite>("army_block_infantry");
        // }
            //ArcherAnimation = Resources.Load<Animator>("draw_animation");
    }
}
