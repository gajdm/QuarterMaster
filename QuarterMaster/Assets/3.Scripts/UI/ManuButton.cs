using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoulGames.Utilities;

public class ManuButton : MonoBehaviour
{
    public ManuUI manuUI;
    public GameManager gameManager;
    [SerializeField] private int buttonNumber;
    [SerializeField] private Text nameText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text priceText;
    [SerializeField] private GameObject otherPriceText;
    [SerializeField] private Image image;

    [SerializeField] private Manufacturer man;
    [SerializeField] private bool available;

    public void UpdateButton(Manufacturer manufacturer)
    {
        if(!available)
        {
            man = manufacturer;
            nameText.text = manufacturer.manuName;
            levelText.text = manufacturer.manuLevel.ToString();
            priceText.text = manufacturer.priceToBuy.ToString();
            image.sprite = manufacturer.manuIcon;
            GetComponent<ToolTip>().SetHeader(nameText.text);
            GetComponent<ToolTip>().SetContent("Needs "+priceText.text+" to buy.");
        }
        else
        {
            priceText.text = "Open";
            levelText.text = manufacturer.manuLevel.ToString();

        }
    }
    public void Check()
    {
        if (!available)
        {
            gameManager.CheckManuButton(int.Parse(priceText.text),this, man);
        }
        else
        {
            manuUI.OpenManuCard(buttonNumber);
        }
    }
    public void SetAvailable(bool value)
    { 
        man.available = value;
        available = value;
        GetComponent<ToolTip>().SetContent("Left click to open.");
    }
}
