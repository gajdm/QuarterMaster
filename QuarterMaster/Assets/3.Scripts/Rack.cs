using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoulGames.EasyGridBuilderPro;

public class Rack : MonoBehaviour
{
    [SerializeField] private string[] bagNames;
    [SerializeField] private GameObject player;
    [SerializeField] private ItemInteraction playerBrain;
    [SerializeField] private Transform spawn;

    //Managers
    [SerializeField] private BuyerManager buyerManager;
    [SerializeField] private UIManager uiManager;

    //FUNCTIONS
    public void OnEnable()
    {
        if(buyerManager == null) buyerManager=FindObjectOfType<BuyerManager>();
        buyerManager.RackPlaced(this);
    }
    public void OnDisable()
    {
        if (buyerManager != null) buyerManager = FindObjectOfType<BuyerManager>();

    }
    public void AssignBags()
    {
        if(uiManager != null)uiManager = FindObjectOfType<UIManager>();
        uiManager.UpdateRackUI(this);
    }
    public void ArchiveRack()
    {
        buyerManager.UpdateRackList(this);
    }
    public string[] GetBagNames()
    {
        return bagNames;
    }
    public void GetBagNames(string[] names)
    {
        bagNames = names;
    }
    //ORDER
    public void CheckItem(int bagNumber)
    {
        if(player.GetComponentInChildren<Item>() != null)
        {
            Item item = player.GetComponentInChildren<Item>();
            if(item.GetAddress() == bagNames[bagNumber])
            {
                buyerManager.ItemAdded(item,spawn);
                Destroy(item.gameObject);
                playerBrain.SetIsHolding(false);
            }
        }
        else
        { return; }
    }
}
