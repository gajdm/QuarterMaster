using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Manufacturer", menuName = "Manufacturer")]
public class Manufacturer : ScriptableObject
{
        [Header("Visual")]

    [Tooltip("The name of the manufacturer. Displayed in the UI")]
    public string manuName;         //Name
   
    [Tooltip("A simple description of the manufacturer. Displayed in the UI")]
    [TextArea(3, 5)]
    public string description;      //Description

    [Tooltip("The icon of the manufacturer. Displayed in the UI")]
    public Sprite manuIcon;         //Icon

        [Header("Gameplay")]

    [Tooltip("The price to aquire this manufacturer.")]
    public int priceToBuy;          //Price to buy

    [Tooltip("The basic price of the level 1 item. All other prices are done in code.")]
    public int itemWorth;           //Item worth

    [Tooltip("A list of lists. Use scriptable objects to create a list of icons first. Then, add them here by level.")]
    public List<ListOfIcons> listOfListsOfIcons; //The lists of sprites for the icons

        [Header("Non-editable variables")]

    public int manuLevel = 1;       //Level
    public bool available;          //Availability
    private List<ListOfIcons> helperList = new List<ListOfIcons>();
    private int helperInt;

    //VARIABLES
    //
    //FUNCTIONS

    public void PickList()
    {
        helperList.Clear();
        helperList.Add(listOfListsOfIcons[Random.Range(0, 5)]);
    }
    public Sprite GetIcon()
    {
        PickList();
        Sprite icon = helperList[0].GetIconFromLevel(manuLevel);
        return icon;
    }
    public int GetLevel()
    {
        return manuLevel;
    }
    public int GetValue()
    {
        int price = (itemWorth * manuLevel) / 2;
        return price;
    }

}
