using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManuButton : MonoBehaviour
{
    public ManuUI manuUI;
    [SerializeField] private int buttonNumber;
    [SerializeField] private Text nameText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text priceText;
    [SerializeField] private Image image;

    [SerializeField] private bool available;
    [SerializeField] private bool canBuy; // will be deleted once we have an economy system.

    public void UpdateButton(Manufacturer manufacturer)
    {
        if (available)
        {
            nameText.text = manufacturer.manuName;
            levelText.text = manufacturer.manuLevel.ToString();
            priceText.text = manufacturer.priceToBuy.ToString();
            image.sprite = manufacturer.manuIcon;
        }
    }
    public void BuyManu()
    {
        available = true;
    }
    public void Check()
    {
        if (canBuy && available)
        {
            manuUI.OpenManuCard(buttonNumber);        
        }
        else if(canBuy)
        {

        }

    }
}
