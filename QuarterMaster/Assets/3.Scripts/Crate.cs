using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    public void SetItems(List<Item> list)
    {
        items = list;
    }
    public List<Item> GetItems()
    {
        return items;
    }
   
}
