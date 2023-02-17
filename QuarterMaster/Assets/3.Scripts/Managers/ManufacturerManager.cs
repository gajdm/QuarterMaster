using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManufacturerManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Manufacturer[] manufacturerList;

    [Header("Item Generation")]
    [SerializeField] private int minNum;
    [SerializeField] private int maxNum;

    [SerializeField] private Item itemPrefab;

    //UPDATING FUNCTIONS
    public void Start()
    {
        foreach (var manufacturer in manufacturerList)
        {
            manufacturer.manuLevel = 1;
        }
    }
    public void UpdateManuAvailability(int manuNumber)
    {
        manufacturerList[manuNumber].available = true;
    }

    //ORDER FUNCTIONS

    public List<Item> GetListOfItems()
    {
        List <Item> list = new List<Item>();
        for (int i = 0; i < RandomizeNumberOfItems(minNum,maxNum); i++)
        {
            list.Add(GenerateItem());
        }
        return list;
    }
    public Item GenerateItem()
    {
        Item newItem = itemPrefab;
        Manufacturer manu = GetRandomManufacturer();
        newItem.SetIcon(manu.GetIcon());
        newItem.SetLevel(manu.GetLevel());
        return newItem;
    }
    public Manufacturer[] GetListOfManufacturers()
    {
        return manufacturerList;
    }

    //RANDOMIZATION
    public int RandomizeNumberOfItems(int min,int max)
    {
        return Random.Range(min,max+1);
    }
    public Manufacturer GetRandomManufacturer()
    {
        Manufacturer manufacturer = manufacturerList[Random.Range(0,4)];
        return manufacturer;
    }


}
