using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public List<GameObject> list;
    public int full;
    public int count;
    public int value;
    public string orderCode;
    public bool done = false;

    public void SetOrderList(List<GameObject> GameObjects)
    {
        list = GameObjects;
    }
    public void SetFullInt(int number)
    { full = number; }
    public void SetCount(int number)
    { count = number; }
    public void AddItem()
    {
        count++;
        if(count == full)
        {
            done = true;
        }
    }
    public void SetOrderCode(string code)
    { orderCode = code; }
    public string GetCode()
    { return orderCode; }
    public bool GetDone()
    {
        return done;
    }
    public void SetValue(int number)
    {
        value = number;
    }
    public int GetValue()
    {
        return value;
    }
}
