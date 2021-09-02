using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{

    public int nextUpdate=1;
    public float attackRange = 15f;
    public float projectileSpeed = 7f;
    public int attackDamage = 8;
    public float attackRate = 3f;
    public string EnemyTag = "Ally";
    public GameObject Projectile;

    // Start is called before the first frame update
    void Update()
    {
        if(Time.time>=attackRate)
        {
            attackRate=Mathf.FloorToInt(Time.time)+1;
            RangedAttackTower();
        }
        
    }

    // Update is called once per frame
    void RangedAttackTower(){
        
        List<Collider2D> target = new List<Collider2D>();
        target.Clear();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(this.transform.position, attackRange);
        
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
}
