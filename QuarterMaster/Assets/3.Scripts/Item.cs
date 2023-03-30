using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoulGames.Utilities;

public class Item : MonoBehaviour
{
     public Sprite itemIcon;
     public string itemAddress;
     public string itemCode;
     public int level;
    public int value;
    

    //SETTING
    public void SetIcon(Sprite icon)
    {
        itemIcon = icon;
    }
    public void SetCode(string code)
    {
        itemCode = code;
        
    }
    public void SetAddress(string address)
    {
        itemAddress = address.Substring(0,4);
    }
    public void SetLevel(int number)
    {
        level = number;
    }
    public void SetValue(int number)
    {
        value = number;
    }


    //GETTING
    public Sprite GetIcon()
    {
        return itemIcon;
    }
    public string GetAddress()
    {
        return itemAddress;
    }
    public string GetCode()
    {
        return itemCode;
    }
    public int GetLevel()
    {
        return level; 
    }
    public int GetValue()
    {
        return value;
    }

}
