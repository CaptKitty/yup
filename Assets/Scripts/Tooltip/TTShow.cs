using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTShow : MonoBehaviour
{
    public void OnMouseEnter(string Text)
    {
        TTScreenSpaceUI.ShowTooltipStatic(Text);
    }

    public void OnMouseExit()
    {
        TTScreenSpaceUI.HideTooltipStatic();
    }
}
