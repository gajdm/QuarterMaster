using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManufacturerManager : MonoBehaviour
{
    [SerializeField]    private UIManager uiManager;
    [SerializeField]    private Manufacturer[] manufacturerList;

    [Header("Item Generation")]
    [SerializeField]    private int minNumber;
    [SerializeField]    private int maxNumber;

    [SerializeField] private int items;
    [SerializeField] private string[] listOfItems;
    [SerializeField] private Sprite[] spriteList;

    //UPDATING FUNCTIONS
    public void UpdateManuLevel(int manuNumber)//Further development
    {

    }
    public void UpdateManuPrice(int manuNumber)//Further development
    {

    }
    public void UpdateManuIcon(int manuNumber)//Further development
    {

    }
    public void UpdateManuAvailability(int manuNumber)
    {
        manufacturerList[manuNumber].available = true;
    }

    //ORDER FUNCTIONS
    public void Start()
    {
        foreach (var manufacturer in manufacturerList)
        {
            manufacturer.manuLevel = 1;
        }
    }
    public void GatherData()
    {
        foreach(var manu in manufacturerList)
        {
            if(manu.available)
            {
                items = RandomizeNumberOfItems(minNumber, maxNumber);
            }
        }  
    }
    public Manufacturer[] GetListOfManufacturers()
    {
        return manufacturerList;
    }
    public int RandomizeNumberOfItems(int min,int max)
    {
        return Random.Range(min,max);
    }
    public int GetNumberOfItems()
    {
        GatherData();
        return items;
    }


}
