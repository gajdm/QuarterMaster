using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyerManager : MonoBehaviour
{
    // The brain of the order and sorting system

    //VARIABLES
    GameManager gameManager;
    //Timer variables
    [SerializeField] private Text debugText;
    [SerializeField] private float timer;
    [SerializeField] private float timeLimit;

    //Bags and Racks variables
    [SerializeField] private List<string> rackList = new List<string>();
    [SerializeField] private List<Rack> racks = new List<Rack>();
    [SerializeField] private string[] currentAddresses = new string[4];

    [SerializeField] private GameObject bag;

    //Address variables
    [SerializeField] private int rackInt;
    [SerializeField] private string rackStr;
    [SerializeField] private int bagInt;
    [SerializeField] private string bagStr;
    [SerializeField] private string address;

    //"Dictionary" variables
    [SerializeField] private List<string> addressList = new List<string>();
    [SerializeField] private List<bool> addressBoolList = new List<bool>();

    //Order variables
    [SerializeField] private string orderNumber;
    [SerializeField] private List<string> orderList;

    //Code variables
    [SerializeField] private int code;

    //FUNCTIONS
    public void FixedUpdate()
    {
        timer += Time.deltaTime;
        debugText.text = "Next Order in " + timer.ToString("00") + "/" + timeLimit;
        if (timer >= timeLimit)
        {
            timer = 0;
            OrderCheck();
        }
    }
    //ORDER FUNCTIONS
    public void OrderCheck()
    {
        for(int i = 0; i < addressBoolList.Count; i++)
        {
            if (addressBoolList[i] == false)
            {
                CreateNewOrder();
                address = addressList[i];
                addressBoolList[i] = true;
            }
        }
    }
    public void CreateNewOrder()
    {
        CreateCode();
        if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
        gameManager.NewOrder(orderNumber);
    }
    public void CreateCode()
    {
        orderNumber = address + code.ToString("000");
        orderList.Add(orderNumber);
        Debug.Log(orderNumber);
        code++;
    }
    public void FinishOrder(string code)
    {
        orderList.Remove(code);
    }

    //RACK FUNCTIONS

    public void RackPlaced(Rack rack) //Logic that happens once the rack is placed
    {
        if(rackList.Count > 0) //Takes an unused rack address
        {
            rackStr = rackList[0];
        }
        else //Creates 4 new adresses for the bags inside the rack
        {
            currentAddresses = new string[4];
            rackInt++;
            bagInt = 1;
            for(int i = 0; i < 4; i++)
            {
                CreateNewAddress();
                bagInt++;
                currentAddresses[i] = address;
            }
            rack.GetBagNames(currentAddresses);
        }
    }
    public void RackRemoved(Rack rack)
    {

    }
    public void UpdateRackList(Rack rack)
    {
        racks.Add(rack);
    }

    //ADDRESS FUNCTIONS

    public void CreateNewAddress() // Creates new addresses and adds them to the Address List
    {
        rackStr = rackInt.ToString("00");
        bagStr = bagInt.ToString("00");
        address = rackStr + bagStr;
        addressList.Add(address);
        addressBoolList.Add(false);
    }
    public void CheckAddressList(int add, bool rack)
    {
        if (rack)
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
   


}
