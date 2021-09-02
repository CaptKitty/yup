using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacles : MonoBehaviour
{
    public int protectionlevel;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.layer = 8;
        protectionlevel = GameManager.instance.attackedCountryFortifications;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
