using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnText : MonoBehaviour
{

    public TextMeshProUGUI desctext;
    public TextMeshProUGUI yeartext;

    // Start is called before the first frame update
    public void Start()
    {
        yeartext.text = "Year: " + GameManager.instance.year;
    }

    void FixedUpdate()
    {
        //if(desctext.text == "")
        //{
            GameObject[] Nations = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
            foreach(GameObject Nation in Nations)
            {
                if (Nation.GetComponent<NationHandler>().nation.tribe.ToString() == "PLAYER")
                {
                    desctext.text = "Treasury: " + Nation.GetComponent<NationHandler>().nation.taxTreasury + " +" + Nation.GetComponent<NationHandler>().nation.taxIncome + "\nManpower: " + Nation.GetComponent<NationHandler>().nation.totalRecruits + " +" + Nation.GetComponent<NationHandler>().nation.recruitsIncome;
                }
            }
        //}
    }

    // Update is called once per frame
    public void TurnTextButton()
    {
        GameObject[] Nations = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject Nation in Nations)
        {
            if (Nation.GetComponent<NationHandler>().nation.tribe.ToString() == "PLAYER")
            {
                desctext.text = "Treasury: " + Nation.GetComponent<NationHandler>().nation.taxTreasury + " +" + Nation.GetComponent<NationHandler>().nation.taxIncome + "\nManpower: " + Nation.GetComponent<NationHandler>().nation.totalRecruits + " +" + Nation.GetComponent<NationHandler>().nation.recruitsIncome;
                GameManager.instance.year += 1;
                yeartext.text = "Year: " + GameManager.instance.year;
            }
        }
    }
}
