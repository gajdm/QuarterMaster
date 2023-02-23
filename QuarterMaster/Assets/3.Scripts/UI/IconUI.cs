using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconUI : MonoBehaviour
{
    [SerializeField] private Text address;
    [SerializeField] private Text code;
    [SerializeField] private Image icon;

    [SerializeField] private string emptyAddress;
    [SerializeField] private string emptyCode;
    [SerializeField] private Sprite emptyIcon;
    [SerializeField] private Sprite bagIcon;

    public void UpdateUI(GameObject GO)
    {
        if (GO.GetComponent<Crate>() != null)
        {
            Crate crate = (Crate)GO.GetComponent<Crate>();
            address.text = "Crate";
            code.text = "Amount of items: " +crate.GetListOfItems().Count.ToString();
            icon.sprite = crate.GetIcon();
        }
        if (GO.GetComponent<Item>() != null)
        {
            Item item = (Item)GO.GetComponent<Item>();
            address.text = "Address: "+item.GetAddress();
            code.text = "Code: " + item.GetCode();
            icon.sprite = item.GetIcon();
        }
        if (GO.GetComponent<Bag>() != null)
        {
            Bag bag = (Bag)GO.GetComponent<Bag>();
            address.text = "Bag";
            code.text = "Code: " +bag.GetOrder();
            icon.sprite = bagIcon;
        }
    }
    public void ClearUI()
    {
        address.text = emptyAddress;
        code.text = emptyCode;
        icon.sprite = emptyIcon;
    }
}
