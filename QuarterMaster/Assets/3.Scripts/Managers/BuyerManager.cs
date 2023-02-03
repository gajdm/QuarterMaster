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

    //Bags and Racks variables
    [SerializeField] private List<string> rackList = new List<string>();
    [SerializeField] private List<Rack> racks = new List<Rack>();

    //Address variables
    [SerializeField] private int rackInt;
    [SerializeField] private string rackStr;
    [SerializeField] private int bagInt;
    [SerializeField] private string bagStr;
    [SerializeField] private string address;

    //Order variables
    [SerializeField] private string orderNumber;
    [SerializeField] private List<string> orderList;

    //Code variables
    [SerializeField] private int code;

    //Dictionary variables
    [SerializeField] private Dictionary<string,bool> addDic = new Dictionary<string,bool>();
    [SerializeField] private List<string> addressList = new List<string>();
    [SerializeField] private List<bool> addressBoolList = new List<bool>();


    //FUNCTIONS

    public void Start()
    {
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            RackPlaced(); 
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
    public void RackPlaced() //Logic that happens once the rack is placed
    {
        if(rackList.Count > 0) //Takes an unused rack address
        {
            rackStr = rackList[0];
        }
        else //Creates 4 new adresses for the bags inside the rack
        {
            rackInt++;
            bagInt = 1;
            for(int i = 0; i < 4; i++)
            {
                CreateNewAddress();
                bagInt++;
            }
        }
    }
    public void CreateNewAddress() // Creates new addresses and adds them to the Address List
    {
        rackStr = rackInt.ToString("00");
        bagStr = bagInt.ToString("00");
        address = rackStr + bagStr;
        addressList.Add(address);
        addressBoolList.Add(true);
    }
    public void CreateCode()
    {
        orderNumber = address + code.ToString("000");
        orderList.Add(orderNumber);
        code++;
    }
    public void FinishOrder(string code)
    {
        orderList.Remove(code);
    }
    public void CheckAddressList(int add,bool rack)
    {
        if(rack)
        {
            for (int i = 0; i < 4;)
            {
                if (!addressBoolList[i])
                {
                    AssignExistingAddress(addressList[i]);
                }
            }
        }
        else
        {

        }
        
    }
    public void AssignExistingAddress(string word)
    {
        address = word;
    }
    public void CreateNewOrder()
    {

    }
    public void UpdateRackList(Rack rack)
    {
        racks.Add(rack);
    }

}
