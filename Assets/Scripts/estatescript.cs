using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class estatescript : MonoBehaviour
{
    public Text culture;
    public Text population;
    public Dropdown dropdown;

    public Dropdown dropdownTax;

    void Start()
    {
        dropdown.options.Add(new Dropdown.OptionData() {text = "0% Draft"});
        dropdown.options.Add(new Dropdown.OptionData() {text = "10% Draft"});
        dropdown.options.Add(new Dropdown.OptionData() {text = "25% Draft"});

        dropdownTax.options.Add(new Dropdown.OptionData() {text = "1% Tax"});
        dropdownTax.options.Add(new Dropdown.OptionData() {text = "2.5% Tax"});
        dropdownTax.options.Add(new Dropdown.OptionData() {text = "10% Tax"});

        GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in theArrays)
        {
            foreach(PopType poptype in nation.GetComponent<NationHandler>().nation.popTypes)
            {
                if("PLAYER" == nation.name && this.name == poptype.culture)
                {
                    culture.text = poptype.culture.ToString();
                    population.text = poptype.population.ToString();
                    if(poptype.draftrate == 0f)
                    {
                        dropdown.value = 0;
                    }
                    if(poptype.draftrate == 0.10f)
                    {
                        dropdown.value = 1;
                    }
                    if(poptype.draftrate == 0.25f)
                    {
                        dropdown.value = 2;
                    }
                    if(poptype.taxrate == 0.01f)
                    {
                        dropdownTax.value = 0;
                    }
                    if(poptype.taxrate == 0.025f)
                    {
                        dropdownTax.value = 1;
                    }
                    if(poptype.taxrate == 0.10f)
                    {
                        dropdownTax.value = 2;
                    }
                }
            }
        }

    }
    public void OnDraftLawChange()
    {
        GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in theArrays)
        {
            foreach(PopType poptype in nation.GetComponent<NationHandler>().nation.popTypes)
            {
                if("PLAYER" == nation.name && this.name == poptype.culture)
                {
                    if(dropdown.value == 0)
                    {
                        poptype.draftrate = 0f;
                    }
                    if(dropdown.value == 1)
                    {
                        poptype.draftrate = 0.10f;
                    }
                    if(dropdown.value == 2)
                    {
                        poptype.draftrate = 0.25f;
                    }
                }
            }
        }
    }
    public void OnTaxLawChange()
    {
        GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in theArrays)
        {
            foreach(PopType poptype in nation.GetComponent<NationHandler>().nation.popTypes)
            {
                if("PLAYER" == nation.name && this.name == poptype.culture)
                {
                    if(dropdownTax.value == 0)
                    {
                        poptype.taxrate = 0.01f;
                        poptype.growthrate = 0.12f;
                    }
                    if(dropdownTax.value == 1)
                    {
                        poptype.taxrate = 0.025f;
                        poptype.growthrate = 0.10f;
                    }
                    if(dropdownTax.value == 2)
                    {
                        poptype.taxrate = 0.10f;
                        poptype.growthrate = 0.05f;
                    }
                }
            }
        }
    }
}
