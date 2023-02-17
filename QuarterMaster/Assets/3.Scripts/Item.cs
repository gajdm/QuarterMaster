using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
     public Sprite itemIcon;
     public string itemAddress;
     public string itemCode;
     public int level;
    

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
        itemAddress = address;
    }
    public void SetLevel(int number)
    {
        level = number;
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

}
