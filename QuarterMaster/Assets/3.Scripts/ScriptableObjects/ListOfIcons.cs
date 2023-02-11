using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ListOfIcons", menuName = "List of icons")]
public class ListOfIcons : ScriptableObject
{
    [Tooltip("One collum of icons.")]
    [SerializeField] private Sprite[] icons;

    [Tooltip("What level. Used in calculating price.")]
    [SerializeField] private int level;

    //VARIABLES
    //
    //FUNCTIONS

    
    public Sprite GetRandomIcon()   // Returns random sprite from the list
    {
        Sprite icon = null;
        icon = icons[Random.Range(0, icons.Length)];
        return icon;
    }
    
    public int GetLevel()           // Returns the level of the whole list
    {
        return level;
    }
    
}
