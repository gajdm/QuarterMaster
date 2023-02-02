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
    [SerializeField] private int rackAdress;
    [SerializeField] private int bagAdress;
    [SerializeField] private int address;

    //Code variables
    [SerializeField] private int code;

    //Order variables
    [SerializeField] private int orderNumber;

    //Dictionary class
    [SerializeField] private List<int> numberList = new List<int>();
    [SerializeField] private List<bool> valueList = new List<bool>();
    [SerializeField] private Dictionary<int,bool> valueDictionary = new Dictionary<int,bool>();


    //FUNCTIONS

    public void Start()
    {
        foreach(int number in numberList)
        {
            if(!valueDictionary.ContainsKey(number))
                valueDictionary[number] = true;
        }
        Debug.Log(valueDictionary.Count);
    }
    public void Update()
    {
        
    }
    public void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= timeLimit)
        {
            OrderCheck();
            timer = 0;
        }
    }
    public void OrderCheck()
    {
        
    }
    public void RackPlaced()
    {
    }
    public void CreateAddress()
    {
        rackAdress += rackAdress;
        code += code;
        for(int i = 0; i < 4; i++)
        {

        }
        
    }
}
