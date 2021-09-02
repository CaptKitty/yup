using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pops
{
    public string name;

    public int totalPopulation;

    public List<PopType> poplist;// = new List<PopType>();
}

[System.Serializable]
public class PopType
{
    public string culture { get; set; }

    public int population { get; set; }

    public float growthrate = 0.1f;
    public float draftrate = 0.1f;
    public float taxrate = 0.025f;
}