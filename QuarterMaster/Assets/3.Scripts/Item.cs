using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private string itemCode;
    [SerializeField] private string itemAddress;

    public void SetCode(string code)
    {
        itemCode = code;
    }
    public void SetAddress(string address)
    {
        itemAddress = address.Substring(0,4);
    }
    public string GetAddress()
    {
        return itemAddress;
    }

}
