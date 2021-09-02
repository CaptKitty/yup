using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TTScreenSpaceUI : MonoBehaviour
{
    public static TTScreenSpaceUI Instance { get; private set; }


    private RectTransform BGRectTransform;
    private TextMeshProUGUI TooltipText;
    private RectTransform rectTransform;
    [SerializeField] private RectTransform canvasRectTransform;

    private void Awake()
    {
        Instance = this;

        BGRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        TooltipText = transform.Find("TT_Text").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();

        HideTooltip();
    }

    private void SetText(string TooltipString)
    {
        TooltipText.SetText(TooltipString);
        TooltipText.ForceMeshUpdate();

        Vector2 textSize = TooltipText.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8, 8);

        BGRectTransform.sizeDelta = textSize + paddingSize;
    }

    private void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if(anchoredPosition.x + BGRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - BGRectTransform.rect.width;
        }
        if (anchoredPosition.y + BGRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - BGRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void ShowTooltip(string TooltipString)
    {
        gameObject.SetActive(true);
        SetText(TooltipString);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    
    public static void ShowTooltipStatic(string TooltipString)
    {
        Instance.ShowTooltip(TooltipString);
    }

    public static void HideTooltipStatic()
    {
        Instance.HideTooltip();
    }

    public void HasPositiveWealth()
    {
        GameObject[] Nations = GameObject.FindGameObjectsWithTag("Nation") as GameObject[];
        foreach(GameObject nation in Nations)
        {
            if (nation.GetComponent<NationHandler>().nation.tribe == "PLAYER")
            {
                if (nation.GetComponent<NationHandler>().nation.taxTreasury < 0 || nation.GetComponent<NationHandler>().nation.totalRecruits < 0)
                {
                    TTScreenSpaceUI.ShowTooltipStatic("You can't use more resources then you have.");
                }
            }
        }
    }
    public void ShowPopulation()
    {
        string showpopulationstring = "";
        showpopulationstring += "Population Growth:\n";
        GameObject player = GameObject.Find("PLAYER");
        foreach(PopType poptype in player.GetComponent<NationHandler>().nation.popTypes)
        {
            if(transform.parent.parent.parent.name == poptype.culture) //transform.parent.parent.parent.name
            {
                float growthrate = poptype.growthrate * 100;
                int Growthrate = (int) growthrate;
                showpopulationstring += "Growthrate: +" + Growthrate + "%\n";
                float draftrate = poptype.draftrate * 100;
                int Draftrate = (int) draftrate;
                showpopulationstring += "Draftrate: -" + Draftrate + "%\n";
                showpopulationstring += "\nNet Population Change: ";
                if(Growthrate-Draftrate < 0)
                {
                    // showpopulationstring += "-";
                }
                else
                {
                    showpopulationstring += "+";
                }
                showpopulationstring += Growthrate-Draftrate + "%";
            }
        }
        ShowTooltip(showpopulationstring);
    }
}
