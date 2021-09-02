using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHandler : MonoBehaviour
{
    public Units units;

    void Start()
    {
        if (this.name == "Swordsmen")
        {
            units.number = 10;//this.transform.parent.GetComponent<NationHandler>().nation.swordsmen;
        }
        if (this.name == "Archer")
        {
            units.number = 10;//this.transform.parent.GetComponent<NationHandler>().nation.archers;
        }
    }
}
