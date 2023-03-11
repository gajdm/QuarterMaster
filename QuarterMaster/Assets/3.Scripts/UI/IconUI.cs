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
    [SerializeField] private Sprite boxIcon;

    [SerializeField] private Animator animator;

    public void UpdateUI(GameObject GO)
    {
        animator.SetBool("IsUp",true);
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
        if (GO.tag == "Box")
        {
            address.text = "Debris";
            code.text = "Just a bunch of rubbish";
            icon.sprite = boxIcon;
        }
    }
    public void ClearUI()
    {
        animator.SetBool("IsUp", false);
        address.text = emptyAddress;
        code.text = emptyCode;
        icon.sprite = emptyIcon;
    }
}
