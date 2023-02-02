using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup manuUi;
    [SerializeField] private CanvasGroup buyerUi;
    [SerializeField] private CanvasGroup rackUi;

    [SerializeField] private CanvasGroup tooltipE;
    [SerializeField] private CanvasGroup toolTipQ;

    public void SwitchUI(string name, bool value)
    {
        CanvasGroup canvasGroup;
        switch (name)
        {
            case "ManuUI":
                canvasGroup = manuUi;
                break;
            default:
                Debug.Log("Wrong UI name");
                return;
        }
        if(value) canvasGroup.alpha = 1;
        else canvasGroup.alpha = 0;
        canvasGroup.interactable = value;
    }
    public void OpenTooltips(int number)
    {
        if(number ==1)
            tooltipE.alpha = 1;
        if(number ==2)
            toolTipQ.alpha = 1;
    }

}
