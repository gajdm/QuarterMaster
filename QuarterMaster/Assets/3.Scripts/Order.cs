using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public List<GameObject> list;
    public int full;
    public int count;
    public string orderCode;

    public void SetOrderList(List<GameObject> GameObjects)
    {
        list = GameObjects;
    }
    public void SetFullInt(int number)
    { full = number; }
    public void SetCount(int number)
    { count = number; }
    public void SetOrderCode(string code)
    { orderCode = code; }
}
