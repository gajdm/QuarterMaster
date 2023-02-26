using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoulGames.EasyGridBuilderPro;

public class Rack : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] private string[] bagNames;
    [SerializeField] private GameObject player;
    [SerializeField] private ItemInteraction playerBrain;
    [SerializeField] private Transform spawn;

    [SerializeField] private bool canBuy;
    [SerializeField] private bool canBuild;

    //Managers
    [SerializeField] private BuyerManager buyerManager;
    [SerializeField] private UIManager uiManager;

    //FUNCTIONS
    public void OnEnable()
    {
        Debug.Log("OnEnable");
        //MultiGridBuildConditionManager.OnBuildConditionCheckBuildableGridObject += CheckBuildConditionBuildableGridObject;
        //MultiGridBuildConditionManager.OnBuildConditionCompleteBuildableGridObject += CompleteBuildConditionBuildableGridObject;

        buyerManager =FindObjectOfType<BuyerManager>();
        playerBrain = FindObjectOfType<ItemInteraction>();
        player=playerBrain.gameObject;
        uiManager=FindObjectOfType<UIManager>();

        gameManager = FindObjectOfType<GameManager>();
        gameManager.PayGold(50);
        canBuild = false;
        StartCoroutine(ResetCanBuild());
        Debug.Log("Added Rack to buyer manager");
        buyerManager.RackPlaced(this);
    }
    public void OnDisable()
    {
        Debug.Log("OnDisable");
        //MultiGridBuildConditionManager.OnBuildConditionCheckBuildableGridObject -= CheckBuildConditionBuildableGridObject;
        //MultiGridBuildConditionManager.OnBuildConditionCompleteBuildableGridObject -= CompleteBuildConditionBuildableGridObject;
    }

    //Conditions
    //private void CheckBuildConditionBuildableGridObject(BuildableGridObjectTypeSO buildableGridObjectTypeSO)
    //{
    //    gameManager = FindObjectOfType<GameManager>();
    //    foreach (BuildableGridObjectTypeSO item in MultiGridBuildConditionManager.BuildableGridObjectTypeSOList)
    //    {
    //        if (item == buildableGridObjectTypeSO && item.enableBuildCondition)
    //        {
    //            //You can simply replace this if statement with your own conditions
    //            if (buildableGridObjectTypeSO.buildConditionSO.goldAmount <= gameManager.GetGoldCurrent())
    //            //And you can leave rest of the code from here as it is
    //            {
    //                MultiGridBuildConditionManager.BuidConditionResponseBuildableGridObject = true;
    //                return;
    //            }
    //            else
    //            {
    //                MultiGridBuildConditionManager.BuidConditionResponseBuildableGridObject = false;
    //                return;
    //            }
    //        }
    //    }
    //    MultiGridBuildConditionManager.BuidConditionResponseBuildableGridObject = false;
    //    return;
    //}
    //private void CompleteBuildConditionBuildableGridObject(BuildableGridObjectTypeSO buildableGridObjectTypeSO)
    //{
    //    foreach (BuildableGridObjectTypeSO item in MultiGridBuildConditionManager.BuildableGridObjectTypeSOList)
    //    {
    //        if (item == buildableGridObjectTypeSO && item.enableBuildCondition)
    //        {
    //            //You can simply replace this if statement with your own conditions
    //            if (buildableGridObjectTypeSO.buildConditionSO.payGoldOnBuild)
    //            {
    //                gameManager.PayGold(buildableGridObjectTypeSO.buildConditionSO.goldAmount);
    //                canBuild = false;
    //                StartCoroutine(ResetCanBuild());
    //                Debug.Log("Added Rack to buyer manager");
    //                buyerManager.RackPlaced(this);
    //            }
    //            //And you can leave rest of the code from here as it is
    //        }
    //    }
    //}
    //Functions
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
        if (player.GetComponentInChildren<Item>() != null)
        {
            Item item = player.GetComponentInChildren<Item>();
            if (item.GetAddress() == bagNames[bagNumber])
            {
                buyerManager.ItemAdded(item, spawn);
                player.GetComponent<ItemInteraction>().SetIsHolding(false);
                Destroy(item.gameObject);
            }
        }
        else
        { return; }
    }
    //PLACING
    public IEnumerator ResetCanBuild()
    {
        yield return new WaitForSeconds(5);
        canBuild = true;
    }
}
