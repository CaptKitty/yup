using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanRights
{
    public string RightsName { get; set; }

    public enum RightsSpread{
        Universal,
        Accepted,
        Brothers,
        Exclusive,
        None
    }

    public RightsSpread rightsSpread { get; set; }

}

[System.Serializable]
public class Laws
{
    public string name = "Code of Laws";

    public float growthrate = 0.1f;
    
    public float draftrate = 0.1f;

    public float taxrate = 0.025f;  

    public List<HumanRights> humanRights = new List<HumanRights>();
}
