using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconUI : MonoBehaviour
{
    [SerializeField] private Text address;
    [SerializeField] private Text code;
    [SerializeField] private Image icon;

    public void UpdateUI(GameObject GO)
    {
        if (GO.GetComponent<Crate>() == null)
        {
            Crate crate = (Crate)GO.GetComponent<Crate>();
        }
        if (GO.GetComponent<Item>() != null)
        {
            Item item = (Item)GO.GetComponent<Item>();
            address.text = item.GetAddress();
            code.text = item.GetCode();
            icon.sprite = item.GetIcon();
        }
        if (GO.GetComponent<Bag>() == null)
        {
            Bag bag = (Bag)GO.GetComponent<Bag>();
        }
    }
}
