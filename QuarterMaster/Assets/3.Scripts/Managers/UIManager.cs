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
    [SerializeField] private CanvasGroup tooltipQ;

    [SerializeField] private Button[] bagButtons;
    [SerializeField] private Bag[] bags;
    [SerializeField] private GameObject player;

    public void SwitchUI(string name, bool value)
    {
        CanvasGroup canvasGroup;
        switch (name)
        {
            case "ManuUI":
                canvasGroup = manuUi;
                break;
            case "RackUI":
                canvasGroup= rackUi;
                break;
            default:
                Debug.Log("Wrong UI name");
                return;
        }
        if(value) canvasGroup.alpha = 1;
        else canvasGroup.alpha = 0;
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
    }
    public void OpenTooltips(bool bool1, string string1, bool bool2, string string2)
    {
        if(bool1)
        {
            tooltipE.alpha = 1;
            tooltipE.GetComponentInChildren<Text>().text = string1;   
        }
        if(bool2)
        {
            tooltipQ.alpha = 1;
            tooltipQ.GetComponentInChildren<Text>().text = string2;
        }
    }
    public void CloseTooltips()
    {
        tooltipE.alpha = 0;
        tooltipQ.alpha = 0;
    }
    public void UpdateRackUI(Rack rack)
    {
        string[] bagNames = rack.SendBagNames(); 
        for (int i = 0; i < bagNames.Length; i++)
        {
            bagButtons[i].gameObject.GetComponentInChildren<Text>().text = bagNames[i];
        }
    }
    public void BagCheck(Button button)
    {
        string address = player.GetComponentInChildren<Item>().GetAddress();
        if (button.GetComponentInChildren<Text>().text == address )
        {
            button.GetComponent<Image>().color = Color.green;
        }
        else button.GetComponent<Image>().color = Color.red;

    }

}
