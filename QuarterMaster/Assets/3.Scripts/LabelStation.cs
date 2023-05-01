using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SoulGames.EasyGridBuilderPro;
using SoulGames.Utilities;

public class LabelStation : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private List<GameObject> listOfItems;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private List<Crate> crateList;
    [SerializeField] private Crate currentCrate;
    //UNPACKING VARIABLES
    [SerializeField] private int secondsToWait;
    [SerializeField] private Transform spawnTransfrom;
    //Condition variables
    [SerializeField] private bool isBuilt;
    //Tick
    [SerializeField] private float timer;
    [SerializeField] private float limit;
    [SerializeField] private bool unpacking;

    
    private void OnEnable()
    {
        MultiGridBuildConditionManager.OnBuildConditionCheckBuildableGridObject += CheckBuildConditionBuildableGridObject;
        MultiGridBuildConditionManager.OnBuildConditionCompleteBuildableGridObject += CompleteBuildConditionBuildableGridObject;

        audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnDisable()
    {
        MultiGridBuildConditionManager.OnBuildConditionCheckBuildableGridObject -= CheckBuildConditionBuildableGridObject;
        MultiGridBuildConditionManager.OnBuildConditionCompleteBuildableGridObject -= CompleteBuildConditionBuildableGridObject;
    }
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
                    if (this.gameObject.name == "Labelling_Down(Clone)" || this.gameObject.name == "Labelling_Right(Clone)" ||
                        this.gameObject.name == "Labelling_Up(Clone)" || this.gameObject.name == "Labelling_Left(Clone)" )
                    { isBuilt = true; }
                    else if (!isBuilt)
                    {
                        isBuilt = true;
                        gameManager = FindObjectOfType<GameManager>();
                        gameManager.PayGold(buildableGridObjectTypeSO.buildConditionSO.goldAmount);
                        FindObjectOfType<QuestManager>().BuildLabelling();
                    }    
                }
                //And you can leave rest of the code from here as it is
            }
        }
    }

    //FUNCTIONS
    public void FixedUpdate()
    {
        timer+=Time.deltaTime;
        if(timer>=limit)
        {
            timer = 0;
            Check();
        }
    }
    public void GetNewCrate(Crate crate)
    {
        if (currentCrate == null)
        {
            currentCrate = crate;
            listOfItems = currentCrate.GetListOfItems();
        }
        else
        {
            crateList.Add(crate);
        }
    }
    public void Check()
    {
        if(currentCrate != null)
        {
            if (listOfItems.Count != 0)
            {
                if (CheckSpawn() && !unpacking)
                {
                    Debug.Log("Got here");
                    unpacking = true;
                    StartCoroutine(Unpacking());
                } 
            }
            else
            {
                Destroy(currentCrate.gameObject);
                currentCrate = null;
                Debug.Log("Empty Crate");
            }
        }
    }
    public IEnumerator Unpacking()
    {
        yield return new WaitForSeconds(secondsToWait);

        GameObject newGameObj = Instantiate(itemPrefab, spawnTransfrom);
        FindObjectOfType<QuestManager>().Labelled();

        Item newItem = newGameObj.GetComponent<Item>();
        Item listItem = listOfItems[0].GetComponent<Item>();

        newItem.name = listItem.name;
        newItem.SetIcon(listItem.GetIcon());
        newItem.SetAddress(listItem.GetAddress());
        newItem.SetCode(listItem.GetCode());
        newItem.GetComponent<ToolTip>().SetContent("Address: " + listItem.GetAddress());
        listOfItems.Remove(listOfItems[0]);
        
        unpacking = false;
    }
    //Checking if the spawnLocation is free
    public bool CheckSpawn()
    {
        Transform trans = spawnTransfrom.transform;
        if(trans.childCount < 1) return true; 

        else return false;
    }
}
