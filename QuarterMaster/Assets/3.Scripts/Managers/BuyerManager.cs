using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyerManager : MonoBehaviour
{
    // The brain of the order and sorting system

    //VARIABLES

    //Timer variables
    [SerializeField] private Text debugText;
    [SerializeField] private float timer;
    [SerializeField] private float timeLimit;
    //Bags variables

    //Address variables
    [SerializeField] private int rackInt;
    [SerializeField] private string rackStr;
    [SerializeField] private int bagInt;
    [SerializeField] private string bagStr;
    [SerializeField] private string address;

    //Order variables
    [SerializeField] private int orderNumber;

    //Code variables
    [SerializeField] private int code;

    //Dictionary class
    [SerializeField] private Dictionary<string,bool> addDic = new Dictionary<string,bool>();


    //FUNCTIONS

    public void Start()
    {
    }
    public void Update()
    {
    }
    public void FixedUpdate()
    {
        
        //timer += Time.deltaTime;
        //if (timer >= timeLimit)
        //{
        //    OrderCheck();
        //    timer = 0;
        //}
    }
    public void OrderCheck()
    {
        
    }
    public void RackPlaced()
    {
        
    }
    public void CreateNewAddress()
    {
        rackStr = rackInt.ToString("00");
        bagStr = bagInt.ToString("00");
        address = rackStr + bagStr;
        Debug.Log("New adress created: " + address);
    }
    public void CheckAddressList(int add)
    {
        for (int i = 0; i < 4;)
        {
            if(addDic.ContainsValue(false))
            {
            }
            i--;
        }
    }
    public void AssignExistingAddress()
    {

    }
}
