using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoulGames.EasyGridBuilderPro;

public class Rack : MonoBehaviour
{
    [SerializeField] private string[] bagNames;

    //Managers
    [SerializeField] private BuyerManager buyerManager;
    [SerializeField] private UIManager uiManager;

    //FUNCTIONS
    
    public void AssignBags()
    {
        if(uiManager != null)uiManager = FindObjectOfType<UIManager>();
        uiManager.UpdateRackUI(this);
    }
    public void ArchiveRack()
    {
        buyerManager.UpdateRackList(this);
    }
    public string[] SendBagNames()
    {
        return bagNames;
    }
    public void GetBagNames()
    {

    }
}
