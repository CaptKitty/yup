using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmyRatio : MonoBehaviour
{

    public Slider armyRatios;

    public Slider armyequipment;

    public Text descriptionText;

    public Text equipmentText;

    private int originalarchers;
    private int originalswordsmen;

    private GameObject player;

    public void Start()
    {
        player = GameObject.Find("PLAYER");
        originalarchers = player.GetComponent<NationHandler>().nation.archers;
        originalswordsmen = player.GetComponent<NationHandler>().nation.swordsmen;

        armyRatios.value = player.GetComponent<NationHandler>().nation.archers * 10 / (player.GetComponent<NationHandler>().nation.archers + player.GetComponent<NationHandler>().nation.swordsmen);
        armyequipment.value = (player.GetComponent<NationHandler>().nation.AttackModifier - 1) * 100;

           armyRatios.onValueChanged.AddListener(delegate {ArmyRatios();    });
        armyequipment.onValueChanged.AddListener(delegate {ArmyEquipment(); });
    }

    public void ArmyRatios()
    {
        int new_value = (int) armyRatios.value;

        player.GetComponent<NationHandler>().nation.archers   = (int) ( 20 * ( armyRatios.value/10));
        player.GetComponent<NationHandler>().nation.swordsmen = (int) (20 - (20 * (armyRatios.value/10)));
        descriptionText.text = player.GetComponent<NationHandler>().nation.swordsmen + " Infantry\n" + player.GetComponent<NationHandler>().nation.archers + " Archers";
    }
    public void ArmyEquipment()
    {
        player.GetComponent<NationHandler>().nation.AttackModifier = 1 + (armyequipment.value/100);
        player.GetComponent<NationHandler>().nation.HealthModifier = 1 - (armyequipment.value/100);
        equipmentText.text = (100 + (int) armyequipment.value) + "% Army Damage\n" + (100 - (int) armyequipment.value) + "% Army Health";
    }
}
