using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoulGames.EasyGridBuilderPro;
using SoulGames.Utilities;

public class Rack : MonoBehaviour
{
    GameManager gameManager;
    public AudioManager audioManager;

    [SerializeField] private string[] bagNames;
    [SerializeField] private GameObject player;
    [SerializeField] private ItemInteraction playerBrain;
    [SerializeField] private Transform spawn;

    [SerializeField] private bool isBuilt;

    //Managers
    [SerializeField] private BuyerManager buyerManager;
    [SerializeField] private UIManager uiManager;

    //FUNCTIONS
    public void OnEnable()
    {
        MultiGridBuildConditionManager.OnBuildConditionCheckBuildableGridObject += CheckBuildConditionBuildableGridObject;
        MultiGridBuildConditionManager.OnBuildConditionCompleteBuildableGridObject += CompleteBuildConditionBuildableGridObject;
        

        buyerManager = FindObjectOfType<BuyerManager>();
        playerBrain = FindObjectOfType<ItemInteraction>();
        player = playerBrain.gameObject;
        uiManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void OnDisable()
    {
        MultiGridBuildConditionManager.OnBuildConditionCheckBuildableGridObject -= CheckBuildConditionBuildableGridObject;
        MultiGridBuildConditionManager.OnBuildConditionCompleteBuildableGridObject -= CompleteBuildConditionBuildableGridObject;
    }

    //Conditions
    private void CheckBuildConditionBuildableGridObject(BuildableGridObjectTypeSO buildableGridObjectTypeSO)
    {
        gameManager = FindObjectOfType<GameManager>();
        foreach (BuildableGridObjectTypeSO item in MultiGridBuildConditionManager.BuildableGridObjectTypeSOList)
        {
            if (item == buildableGridObjectTypeSO && item.enableBuildCondition)
            {
                //You can simply replace this if statement with your own conditions
                if (buildableGridObjectTypeSO.buildConditionSO.goldAmount <= gameManager.GetGoldCurrent())
                //And you can leave rest of the code from here as it is
                {
                    MultiGridBuildConditionManager.BuidConditionResponseBuildableGridObject = true;
                    return;
                }
                else
                {
                    MultiGridBuildConditionManager.BuidConditionResponseBuildableGridObject = false;
                    return;
                }
            }
        }
        MultiGridBuildConditionManager.BuidConditionResponseBuildableGridObject = false;
        return;
    }
    private void CompleteBuildConditionBuildableGridObject(BuildableGridObjectTypeSO buildableGridObjectTypeSO)
    {
        foreach (BuildableGridObjectTypeSO item in MultiGridBuildConditionManager.BuildableGridObjectTypeSOList)
        {
            if (item == buildableGridObjectTypeSO && item.enableBuildCondition)
            {
                //You can simply replace this if statement with your own conditions
                if (buildableGridObjectTypeSO.buildConditionSO.payGoldOnBuild)
                {
                    if (this.gameObject.name == "Rack(Clone)")
                    { isBuilt = true; }
                    else if (!isBuilt)
                    {
                        audioManager.PlaySound("Placed");
                        isBuilt = true;
                        FindObjectOfType<LogsSystem>().AddLog("Added new rack !!");
                        gameManager.PayGold(buildableGridObjectTypeSO.buildConditionSO.goldAmount);
                        buyerManager.RackPlaced(this);
                        FindObjectOfType<QuestManager>().BuildRack();
                    }
                }
                //And you can leave rest of the code from here as it is
            }
        }
    }
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
        ToolTip tip = GetComponentInChildren<ToolTip>();
        tip.SetContent(names[0]+"\n"+names[1] + "\n"+names[2] + "\n"+names[3]);
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
                FindObjectOfType<QuestManager>().Sorted();
            }
        }
        else
        { return; }
    }
}
