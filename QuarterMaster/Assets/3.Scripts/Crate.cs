using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{

    //VARIABLES
    // Crate icon
    [SerializeField] private Sprite icon; 

    // The list of items that the crate carries
    [SerializeField] private List<Item> items = new List<Item>(); 
    
    // Amount of items inside the crate
    [SerializeField] private int amount;  

    //VARIABLES
    //
    //FUNCTIONS

    public void SetItems(List<Item> items)
    {

    }

   
}
