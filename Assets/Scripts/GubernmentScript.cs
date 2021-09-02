using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GubernmentScript : MonoBehaviour
{
    public GameObject EstateGUI;
    public  int vx = 250;
    public  int vy = 370;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] theArrays = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in theArrays)
        {
            if("PLAYER" == nation.name)
            {
                foreach(PopType poptype in nation.GetComponent<NationHandler>().nation.popTypes)
                {
                    GameObject Estate = Instantiate(EstateGUI) as GameObject;
                    Estate.name = poptype.culture;
                    //transformstuff
                        Estate.transform.SetParent(GameObject.Find("Canvas").transform);
                        RectTransform rt = Estate.GetComponent<RectTransform>();
                        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, rt.rect.width);
                        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, rt.rect.height);
                        Estate.GetComponent<RectTransform>().position = new Vector2(vx, vy);
                        vx = vx + 300;
                }
            }
        }
        //Custom();
    }

    // Start is called before the first frame update
    public void draftButton1()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in theArray)
        {
            if(nation.name == "PLAYER")
            {
                nation.GetComponent<NationHandler>().nation.laws.draftrate = 0.05f;
            }
        }
    }
    public void draftButton2()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in theArray)
        {
            if(nation.name == "PLAYER")
            {
                nation.GetComponent<NationHandler>().nation.laws.draftrate = 0.1f;
            }
        }
    }
    public void draftButton3()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in theArray)
        {
            if(nation.name == "PLAYER")
            {
                nation.GetComponent<NationHandler>().nation.laws.draftrate = 0.25f;
            }
        }
    }
    public void draftButton4()
    {
        GameManager.instance.SaveNation();
        SceneManager.LoadScene("Main");
    }
}
