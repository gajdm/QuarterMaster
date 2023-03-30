using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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
            manufacturer.available = false;
        }
    }
    public void UpdateManuAvailability(int manuNumber)
    {
        manufacturerList[manuNumber].available = true;
    }

    //ORDER FUNCTIONS

    public List<GameObject> GetListOfItems()
    {
        List <GameObject> list = new List<GameObject>();
        for (int i = 0; i < RandomizeNumberOfItems(minNum,maxNum); i++)
        {
            list.Add(GenerateItem());
        }
        return list;
    }
    public GameObject GenerateItem()
    {
        GameObject gameObj = new GameObject();
        Item newItem = gameObj.AddComponent<Item>();

        Manufacturer manu = GetRandomManufacturer();
        if (manu == null) return null;
        else
        {
            newItem.SetIcon(manu.GetIcon());
            newItem.SetLevel(manu.GetLevel());
            newItem.SetValue(manu.GetValue());
            return gameObj;
        }
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
        List<Manufacturer> availableMan = new List<Manufacturer>();
        foreach (Manufacturer man in manufacturerList)
        {
            if (man.available)
            {
                availableMan.Add(man);
            }
        }

        if(availableMan.Count == 0)
        {
            return null;
        }
        else
        {

            Manufacturer manufacturer = availableMan[Random.Range(0, availableMan.Count)];
            return manufacturer;
        }
    }


}
