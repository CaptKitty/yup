using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnitCustomizer : MonoBehaviour
{
    public GameObject UnitCustomizerGUI;

    private List<Units.Weapons> weapondropoptions = new List<Units.Weapons> { Units.Weapons.Sword, Units.Weapons.Axe};

    public  int vx = 100;
    public  int vy = 370;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Units") as GameObject[];
        foreach(GameObject units in theArrays)
        {
            if("PLAYER" == units.transform.parent.GetComponent<NationHandler>().nation.tribe.ToString())
            {
                GameObject Cunit = Instantiate(UnitCustomizerGUI) as GameObject;
                Cunit.name = units.GetComponent<UnitHandler>().units.name;
                //transformstuff
                    Cunit.transform.SetParent(GameObject.Find("Canvas").transform);
                    RectTransform rt = Cunit.GetComponent<RectTransform>();
                    rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, rt.rect.width);
                    rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, rt.rect.height);
                    Cunit.GetComponent<RectTransform>().position = new Vector2(vx, vy);
                    vx = vx + 350;
                Cunit.transform.Find("Name/NamePlaceholder").GetComponent<Text>().text = units.GetComponent<UnitHandler>().units.name;
                Cunit.transform.Find("Weapon/LabelText").    GetComponent<Text>().text = units.GetComponent<UnitHandler>().units.weapon.ToString();
                //Cunit.transform.Find("Weapon/LabelText").GetComponent<Text>().text = units.GetComponent<UnitHandler>().units.weapon.ToString();
                //Cunit.transform.Find("Weapon/Template/Viewport/Content/Item/Item Label").GetComponent<Text>().text = units.GetComponent<UnitHandler>().units.weapon.ToString();
                //Cunit.transform.Find("Weapon").GetComponent<Dropdown>().AddOptions("meow");
            }
        }
        //Custom();
    }

    void Custom()
    {
        GameObject Cunit = Instantiate(UnitCustomizerGUI) as GameObject;
        Cunit.name = "CUSTOM";
        //transformstuff
            Cunit.transform.SetParent(GameObject.Find("Canvas").transform);
            RectTransform rt = Cunit.GetComponent<RectTransform>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, rt.rect.width);
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, rt.rect.height);
            Cunit.GetComponent<RectTransform>().position = new Vector2(vx, vy);
            vx = vx + 250;
    }

    // Update is called once per frame

}
