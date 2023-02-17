using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{

    //VARIABLES
    // Crate icon
    [SerializeField] private Sprite icon; 

    // The list of items that the crate carries
    [SerializeField] private List<Item> listOfItems = new List<Item>(); 
    
    // Amount of items inside the crate
    [SerializeField] private int amount;  

    //VARIABLES
    //
    //FUNCTIONS

    public void SetItems(List<Item> items)
    {
        listOfItems = items;
    }
    public void SetAmount(int number)
    {
        amount = number;
    }

    //GETTING
    public Sprite GetIcon()
        { return icon; }
    public List<Item> GetListOfItems()
        { return listOfItems; }
    public int GetAmount()
        { return amount; }

   
}
