using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public int nextUpdate=1;

    public int maxHealth = 100;
    public int currentHealth;
    public float attackRange = 1.2f;
    public float projectileSpeed = 5f;
    public Transform attackPoint;
    public int attackDamage = 25;
    private float chargespeed = 0;
    public float chargebonus = 0.25f;
    public float attackRate = 0.25f;
    //public LayerMask enemyLayers;
    public Units.Weapons weapon;
    public Units.Armor armor;
    public Units.Mounts mount;


    private float nextStrike;

    float Base_moveSpeed;
    public Rigidbody2D rb;
    Vector2 movement;

    public float detectRange = 100f;


    public string EnemyTag;
    public bool IsRanged;
    public string Nation;
    private bool dead = false;

    public Animator animator;
    public GameObject Projectile;

    public List<Collider2D> target = new List<Collider2D>();

    private Collider2D enemy;

    public Transform AItarget;
    public float speed = 2000f;
    public float maximumspeed = 2f;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Base_moveSpeed = speed;
        //moveSpeed = 0;
        seeker = GetComponent<Seeker>();
        rb = this.GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        Targeter();
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0 && dead == false)
        {
            dead = true;
            Die();
        }
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, AItarget.position, OnPathComplete);
        }
    }

    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        List<Collider2D> fighting = new List<Collider2D>();
        fighting.Clear();

        Collider2D[] AttackEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        if(AttackEnemies.Length >= 1)
        {
            foreach (Collider2D combat in AttackEnemies)
            {
                if (combat.tag == EnemyTag && combat.gameObject.layer != 8)
                {
                    fighting.Add(combat);
                }
            }
            if (fighting.Count >= 1 &&  this.GetComponent<Enemy>().IsRanged == true)
            {
                direction = ((Vector2)path.vectorPath[currentWaypoint] + rb.position).normalized;
                if(Time.time>=nextStrike)
                {
                    RangedAttack();
                    nextStrike = Time.time+attackRate;
                }
            }
            else
            {
                direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            }
        }

        
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if( distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
    void Update()
    {
        // //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        // //rb.velocity = new Vector2(-moveSpeed, 0);
        Vector2 currentspeed = rb.velocity;
        float currentspeedy = rb.velocity.y;
        float currentspeedx = rb.velocity.x;

        //float maximumspeedalt = -1f;
        //Vector2 Maximumspeed = new Vector2(2,2);

        if(rb.velocity.magnitude > new Vector2(maximumspeed,currentspeedy).magnitude || rb.velocity.magnitude > new Vector2(currentspeedx,maximumspeed).magnitude)
        {
            if(rb.velocity.magnitude > new Vector2(maximumspeed,currentspeedy).magnitude)
            {
                if(rb.velocity.x > 0)
                {
                    rb.velocity = new Vector2(maximumspeed,currentspeedy);
                }
                if(rb.velocity.x < 0)
                {
                    maximumspeed = maximumspeed*-1;
                    rb.velocity = new Vector2(maximumspeed,currentspeedy);
                }
            }
            if(rb.velocity.magnitude > new Vector2(currentspeedx,maximumspeed).magnitude)
            {
                if(rb.velocity.y > 0)
                {
                    rb.velocity = new Vector2(currentspeedx,maximumspeed);
                }
                if(rb.velocity.y < 0)
                {
                    maximumspeed = maximumspeed*-1;
                    rb.velocity = new Vector2(currentspeedx,maximumspeed);
                }
            }
        }


        if(enemy == null)
        {
            //speed = 0;
            Targeter();
        }
        else
        {
            //speed = 0;
            //Turnfixed();
        }
        
        // float step = moveSpeed * Time.deltaTime;
        // transform.position = Vector2.MoveTowards(transform.position, MovePoint.position, step);
        

        if(Time.time>=nextUpdate)
        {
            nextUpdate=Mathf.FloorToInt(Time.time)+1;
            if (Input.GetKeyDown("escape"))
            {
                Application.Quit();
            }
            UpdateEverySecond();
        }

        List<Collider2D> fighting = new List<Collider2D>();
        fighting.Clear();

        Collider2D[] AttackEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        
        if(AttackEnemies.Length >= 1)
        {
            foreach (Collider2D combat in AttackEnemies)
            {
                if (combat.tag == EnemyTag && combat.gameObject.layer != 8)
                {
                    fighting.Add(combat);
                }
            }
            if (fighting.Count >= 1 &&  this.GetComponent<Enemy>().IsRanged == false)
            {
                animator.SetFloat("Melee_Combat", 1);
                speed = 0;
                if(Time.time>=nextStrike)
                {
                    BasicAttack();
                    nextStrike = Time.time+attackRate;
                }
            }
            else if (fighting.Count >= 1 &&  this.GetComponent<Enemy>().IsRanged == true)
            {
                animator.SetFloat("Ranged_Combat", 1);
                speed = 0;
                rb.velocity = Vector3.zero;
                if(Time.time>=nextStrike)
                {
                    RangedAttack();
                    nextStrike = Time.time+attackRate;
                }
            }
            else
            {
                speed = Base_moveSpeed;
            }
        }
    }

    void UpdateEverySecond()
    {
        //Turn();
        
        animator.SetFloat("Melee_Combat", 0);
        animator.SetFloat("Ranged_Combat", 0);
        //CheckEnd();
        
    }
    void BasicAttack(){

        List<Collider2D> target = new List<Collider2D>();
        target.Clear();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        
        if(hitEnemies.Length >= 1)
        {
            foreach (Collider2D enemies in hitEnemies)
            {
                if (enemies.tag == EnemyTag && enemies.gameObject.layer != 8)
                {
                    target.Add(enemies);
                }
            }
            if(target.Count >= 1)
            {
                speed = 0;
                int rng = Random.Range (0, target.Count);
                Collider2D enemy = target[rng];
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                try 
                {
                    ShockAttack();
                    
                }
                catch{}
            }
        }

            // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            // if(hitEnemies.Length >= 1)
            // {
            //     moveSpeed = 0;
            //     int rng = Random.Range (0, hitEnemies.Length);
            //     Collider2D enemy = hitEnemies[rng];
            //     enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            // }
            // else
            // {
            //     moveSpeed = Base_moveSpeed;
            // }
            // // foreach(Collider2D enemy in hitEnemies)
            // // {
            //     //print("We hit" + enemy.name );
            //     //enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            // //}
    }
    void RangedAttack(){
        
        List<Collider2D> target = new List<Collider2D>();
        target.Clear();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        
        if(hitEnemies.Length >= 1)
        {
            foreach (Collider2D enemies in hitEnemies)
            {
                if (enemies.tag == EnemyTag && enemies.gameObject.layer != 8)
                {
                    target.Add(enemies);
                }
            }
            if(target.Count >= 1)
            {
                speed = 0;
                int rng = Random.Range (0, target.Count);
                Collider2D enemy = target[rng];
                   
                    Vector3 targ = enemy.transform.position;
                    Vector3 objectPos = transform.position;
                    targ.x = targ.x - objectPos.x;
                    targ.y = targ.y - objectPos.y;
                    float angle = Mathf.Atan2(targ.y, targ.x ) * Mathf.Rad2Deg -90; //+ Angles;


                    GameObject arrow = Instantiate(Projectile) as GameObject;
                    arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    arrow.GetComponent<ProjectileScript> ().EnemyTag = EnemyTag;
                    arrow.GetComponent<ProjectileScript> ().attackDamage = attackDamage;
                    arrow.GetComponent<ProjectileScript> ().moveSpeed = projectileSpeed;
                    
                    Vector3 location = transform.position;

                    arrow.transform.position = new Vector3(location.x, location.y);

                    //Transform Child = this.gameObject.transform.GetChild(0);
                    //Child.transform.position = new Vector3(location.x, location.y);
            }
        }
    }
    void ShockAttack()
    {
        Vector2 currentspeed = rb.velocity;
        float currentspeedy = rb.velocity.y;
        float currentspeedx = rb.velocity.x;
        

        //float maximumspeedalt = -1f;
        //Vector2 Maximumspeed = new Vector2(2,2);

        // if(rb.velocity.magnitude > new Vector2(maximumspeed,currentspeedy).magnitude || rb.velocity.magnitude > new Vector2(currentspeedx,maximumspeed).magnitude)
        // {
            if(currentspeedy > 0)
            {
                if(currentspeedx > 0)
                {
                    if(currentspeedy > currentspeedx)
                    {
                        chargespeed = currentspeedy;
                    }
                    if(currentspeedx > currentspeedy)
                    {
                        chargespeed = currentspeedx;
                    }
                }
                if(currentspeedx < 0)
                {
                    currentspeedx = currentspeedx*-1;
                    if(currentspeedy > currentspeedx)
                    {
                        chargespeed = currentspeedy;
                    }
                    if(currentspeedx > currentspeedy)
                    {
                        chargespeed = currentspeedx;
                    }
                }
            }
            if(currentspeedy < 0)
            {
                currentspeedy = currentspeedy*-1;
                if(currentspeedx > 0)
                {
                    if(currentspeedy > currentspeedx)
                    {
                        chargespeed = currentspeedy;
                    }
                    if(currentspeedx > currentspeedy)
                    {
                        chargespeed = currentspeedx;
                    }
                }
                if(currentspeedx < 0)
                {
                    currentspeedx = currentspeedx*-1;
                    if(currentspeedy > currentspeedx)
                    {
                        chargespeed = currentspeedy;
                    }
                    if(currentspeedx > currentspeedy)
                    {
                        chargespeed = currentspeedx;
                    }
                }
            }
        // }
        float chargedamage = chargespeed * attackDamage * chargebonus;
        int Chargedamage = (int) chargedamage;
        //print("Charged for: " + Chargedamage + " Damage!");
        enemy.GetComponent<Enemy>().TakeDamage(Chargedamage);
    }
    void Turn()
    {
        List<Collider2D> targeting = new List<Collider2D>();
        targeting.Clear();

        Collider2D[] searchEnemies = Physics2D.OverlapCircleAll(attackPoint.position, detectRange);

        if(searchEnemies.Length >= 1)
        {
            foreach (Collider2D enemy in searchEnemies)
            {
                if (enemy.tag == EnemyTag)
                {
                    targeting.Add(enemy);
                }
            }
            if(targeting.Count >= 1)
            {
                speed = 0;
                int rng = Random.Range (0, targeting.Count);
                Collider2D enemy = targeting[rng];
                //enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

                Vector3 targ = enemy.transform.position;
                Vector3 objectPos = transform.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;
                float angle = Mathf.Atan2(targ.y, targ.x ) * Mathf.Rad2Deg -90; //+ Angles;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }


        
        // Collider2D[] searchEnemies = Physics2D.OverlapCircleAll(attackPoint.position, detectRange, enemyLayers);
        // if(searchEnemies.Length >= 1)
        // {
        //     int target = Random.Range (0, searchEnemies.Length);
        //     Collider2D enemy = searchEnemies[target];
            
            
        //     //Debug.Log("found " + enemy);
            
        //     Vector3 targ = enemy.transform.position;
        //     //targ.z = 0f;

        //     Vector3 objectPos = transform.position;
        //     targ.x = targ.x - objectPos.x;
        //     targ.y = targ.y - objectPos.y;

        //     float angle = Mathf.Atan2(targ.y, targ.x ) * Mathf.Rad2Deg + Angles;
        //     transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        // }
    }
    void Turnfixed()
    {
        if(name != "Tower")
        {
            try{
                Vector3 targ = enemy.transform.position;
                Vector3 objectPos = transform.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;
                float angle = Mathf.Atan2(targ.y, targ.x ) * Mathf.Rad2Deg -90; //+ Angles;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            catch{}
        }
    }
    void CheckEnd()
    {
        List<Collider2D> searching = new List<Collider2D>();
        searching.Clear();
        Collider2D[] searchEnemies = Physics2D.OverlapCircleAll(attackPoint.position, detectRange);
        
        if(searchEnemies.Length >= 1)
        {
            foreach (Collider2D combat in searchEnemies)
            {
                if (combat.tag == EnemyTag)
                {
                    searching.Add(combat);
                }
            }
            if (searching.Count == 0)
            {
                speed = 0;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

//     // void FixedUpdate() //Output
//     // {

//     // }

    void Die()
    {
        GameObject[] Units = GameObject.FindGameObjectsWithTag("Units") as GameObject[];
        foreach(GameObject Unit in Units)
        {
            if(Unit.GetComponent<UnitHandler>().units.name == this.name) //Unit.GetComponent<UnitHandler>().units.name
            {
                if (Unit.transform.parent.name == this.GetComponent<Enemy>().Nation)
                {
                    Unit.GetComponent<UnitHandler>().units.number -= 1;
                }
            }
        }
        if(this.name == "Militia")
        {
            GameManager.instance.MilitiaDied += 1;
        }
        Destroy(this.gameObject);
    }
    void Targeter()
    {
        target = new List<Collider2D>();
        target.Clear();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, detectRange);
        
        if(hitEnemies.Length >= 1)
        {
            foreach (Collider2D enemies in hitEnemies)
            {
                if (enemies.tag == EnemyTag && enemies.gameObject.layer != 8)
                {
                    target.Add(enemies);
                }
            }
            if(target.Count >= 1)
            {
                enemy = target[0];
                float oldDifferenceY = enemy.gameObject.transform.position.y - this.gameObject.transform.position.y;
                if(oldDifferenceY<=0)
                {
                    oldDifferenceY = oldDifferenceY*-1;
                }
                float oldDifferenceX = enemy.gameObject.transform.position.x - this.gameObject.transform.position.x;
                if(oldDifferenceX<=0)
                {
                    oldDifferenceX = oldDifferenceX*-1;
                }
                float olddistance = oldDifferenceY+oldDifferenceX;

                foreach ( Collider2D targets in target)
                {
                    float differenceY = targets.gameObject.transform.position.y - this.gameObject.transform.position.y;
                    if(differenceY<=0)
                    {
                        differenceY = differenceY*-1;
                    }
                    float differenceX = targets.gameObject.transform.position.x - this.gameObject.transform.position.x;
                    if(differenceX<=0)
                    {
                        differenceX = differenceX*-1;
                    }
                    float distance = differenceY+differenceX;


                    if ( distance <= olddistance )
                    {
                        enemy = targets;
                        oldDifferenceY = enemy.gameObject.transform.position.y - this.gameObject.transform.position.y;
                        if(oldDifferenceY<=0)
                        {
                            oldDifferenceY = oldDifferenceY*-1;
                        }
                        oldDifferenceX = enemy.gameObject.transform.position.x - this.gameObject.transform.position.x;
                        if(oldDifferenceX<=0)
                        {
                            oldDifferenceX = oldDifferenceX*-1;
                        }
                        olddistance = oldDifferenceY+oldDifferenceX;
                    }
                }
                AItarget = enemy.gameObject.transform;
            }
        }
        else
        {
            speed = 0;
        }
    }
}

    