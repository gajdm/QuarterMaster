using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup manuUi;
    [SerializeField] private CanvasGroup buyerUi;
    [SerializeField] private CanvasGroup rackUi;

    [SerializeField] private IconUI iconUI;

    [SerializeField] private CanvasGroup tooltipLeft;
    [SerializeField] private CanvasGroup tooltipRight;

    [SerializeField] private Button[] bagButtons;
    [SerializeField] private Bag[] bags;
    [SerializeField] private Color bagColor;
    [SerializeField] private GameObject player;

    [SerializeField] private Animator upperBarAnimator;
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
    public void OpenTooltips(bool left, string string1, bool right, string string2)
    {
        //if(left)
        //{
        //    tooltipLeft.alpha = 1;
        //    tooltipLeft.GetComponentInChildren<Text>().text = string1;   
        //}
        //if(right)
        //{
        //    tooltipRight.alpha = 1;
        //    tooltipRight.GetComponentInChildren<Text>().text = string2;
        //}
    }
    public void CloseTooltips(bool left, bool right)
    {
        //if(left)tooltipLeft.alpha = 0;
        //if(right)tooltipRight.alpha = 0;
    }
    public void UpdateRackUI(Rack rack)
    {
        string[] bagNames = rack.GetBagNames(); 
        for (int i = 0; i < bagNames.Length; i++)
        {
            bagButtons[i].gameObject.GetComponentInChildren<Text>().text = bagNames[i];
            bagButtons[i].GetComponent<Image>().color = bagColor; 
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
    public void OpenManuUI()
    {
        SwitchUI("ManuUI", true);
        CloseUpperBar();
    }
    public void CloseUpperBar()
    {
        upperBarAnimator.SetTrigger("Action");
    }
    public void UpdateItemBar(GameObject GO)
    { iconUI.UpdateUI(GO);}
    public void ClearItemBar()
    { iconUI.ClearUI();}

}
