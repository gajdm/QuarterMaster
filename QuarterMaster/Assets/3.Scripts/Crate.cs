using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{

    //VARIABLES
    // Crate icon
    [SerializeField] private Sprite icon; 

    // The list of items that the crate carries
    [SerializeField] private List<GameObject> listOfItems = new List<GameObject>(); 
    
    // Amount of items inside the crate
    [SerializeField] private int amount;  

    //VARIABLES
    //
    //FUNCTIONS

    public void SetItems(List<GameObject> items)
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
    public List<GameObject> GetListOfItems()
        { return listOfItems; }
    public int GetAmount()
        { return amount; }

   
}
