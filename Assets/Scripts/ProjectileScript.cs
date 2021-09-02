using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public Transform MovePoint;
    public float moveSpeed;
    public int attackDamage = 10;
    public int attackRange = 0;
    public string EnemyTag;
    private float rangeTimer = 3f;
    private float nextUpdate;

    private int piercing = 0;
    
    void Start()
    {
        nextUpdate=Mathf.FloorToInt(Time.time)+rangeTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>=nextUpdate)
        {
            Destroy(this.gameObject);
        }
        
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, MovePoint.position, step);
        
        // List<Collider2D> target = new List<Collider2D>();
        // target.Clear();

        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange);
        
        // if(hitEnemies.Length >= 1)
        // {
        //     foreach (Collider2D enemies in hitEnemies)
        //     {
        //         if (enemies.tag == EnemyTag)
        //         {
        //             target.Add(enemies);
        //         }
        //     }
        //     if(target.Count >= 1)
        //     {
        //         moveSpeed = 0;
        //         int rng = Random.Range (0, target.Count);
        //         Collider2D enemy = target[rng];
        //         if (enemy.tag == EnemyTag && enemy.gameObject.layer != 8)
        //         {
        //             enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        //         }
        //         Destroy(this.gameObject);
        //     }
        // }
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == EnemyTag && hit.gameObject.layer != 8)
        {
            hit.GetComponent<Enemy>().TakeDamage(attackDamage);
            Destroy(this.gameObject);
        }
        if (hit.tag == EnemyTag && hit.gameObject.layer == 8)
        {
            int rng = Random.Range (0, 2);//hit.gameObject.GetComponent<obstacles>().protectionlevel);
            //print(rng);
            if(rng <= piercing)
            {
                //arrow passes through
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
