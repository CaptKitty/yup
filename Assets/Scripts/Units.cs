using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Units
{
    public string name = "swordsmen";

    public bool ranged = false;

    public float attackRange = 1.20f;
    public float projectileSpeed = 5;
    public int attackDamage = 10;
    public float attackRate = 1;
    public int maxhealth = 50;
    public float MovementSpeed = 2000f;
    public float maximumspeed = 2f;
    public float chargebonus = 0.25f;
    public int number;
    public string projectile = "arrow";
    public int cost = 1;

    public enum Weapons{
        Axe,
        Sword,
        Pike,
        Longbow,
        Crossbow,
        None
    }

    public enum Armor{
        Heavy,
        Light,
        None
    }
    public enum Mounts{
        None,
        Horse
    }

    public Weapons weapon;
    public Armor armor;
    public Mounts mount = Mounts.None;
}
