using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public string order;
    public Sprite icon;
    public string GetOrder()
    {
        return order;
    }
    public void SetOrder(string name)
    { order = name; }
    public Sprite GetIcon()
        { return icon; }
}
