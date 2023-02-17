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
    private List<ListOfIcons> helperList;
    private int helperInt;

    //VARIABLES
    //
    //FUNCTIONS

    public void PickList()
    {
        helperList.Clear();
        switch(manuLevel) // Decides which lists of icons should be used when picking the sprite. Depending on level.
        {
            case 1:
                helperList.Add(listOfListsOfIcons[0]);
                break;
            case 2:
                helperList.Add(listOfListsOfIcons[0]);
                helperList.Add(listOfListsOfIcons[1]);
                break;
            case 3:
                helperList.Add(listOfListsOfIcons[0]);
                helperList.Add(listOfListsOfIcons[1]);
                helperList.Add(listOfListsOfIcons[2]);
                break;
            case 4:
                helperList.Add(listOfListsOfIcons[1]);
                helperList.Add(listOfListsOfIcons[2]);
                helperList.Add(listOfListsOfIcons[3]);
                break;
            case 5:
                helperList.Add(listOfListsOfIcons[2]);
                helperList.Add(listOfListsOfIcons[3]);
                helperList.Add(listOfListsOfIcons[4]);
                break;
            case 6:
                helperList.Add(listOfListsOfIcons[3]);
                helperList.Add(listOfListsOfIcons[4]);
                helperList.Add(listOfListsOfIcons[5]);
                break;
            case 7:
                helperList.Add(listOfListsOfIcons[4]);
                helperList.Add(listOfListsOfIcons[5]);
                helperList.Add(listOfListsOfIcons[6]);
                break;
            case 8:
                helperList.Add(listOfListsOfIcons[5]);
                helperList.Add(listOfListsOfIcons[6]);
                helperList.Add(listOfListsOfIcons[7]);
                break;
            case 9:
                helperList.Add(listOfListsOfIcons[6]);
                helperList.Add(listOfListsOfIcons[7]);
                helperList.Add(listOfListsOfIcons[8]);
                break;
            case 10:
                helperList.Add(listOfListsOfIcons[8]);
                helperList.Add(listOfListsOfIcons[9]);
                break;
            default:
                break;
        }
    }
    public Sprite GetIcon()
    {
        PickList();
        helperInt = Random.Range(0, helperList.Count - 1);
        ListOfIcons listOfIcons = helperList[helperInt];  
        Sprite icon = listOfIcons.GetRandomIcon();
        return icon;
    }
    public int GetLevel()
    {
        return helperInt+1;
    }

}
