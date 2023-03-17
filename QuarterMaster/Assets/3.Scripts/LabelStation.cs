using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SoulGames.EasyGridBuilderPro;

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
    [SerializeField] private bool emptySpawnLocation;
    [SerializeField] private Transform spawnTransfrom;
    //Condition variables
    [SerializeField] private bool isBuilt;

    
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
                    }    
                }
                //And you can leave rest of the code from here as it is
            }
        }
    }
    public void GetNewCrate(Crate crate)
    {
        if (currentCrate == null)
        {
            currentCrate = crate;
            listOfItems = currentCrate.GetListOfItems();
            StartCoroutine(Unpacking());
        }
        else
        {
            crateList.Add(crate);
        }
    }
    public void Check()
    {
        if (listOfItems.Count != 0)
        {
            StartCoroutine(Unpacking());
        }
        else
        {
            Destroy(currentCrate.gameObject);
            currentCrate = null;
            Debug.Log("Empty Crate");
        }
    }
    public IEnumerator Unpacking()
    {
        GameObject newGameObj = Instantiate(itemPrefab, spawnTransfrom);
        //audioManager.PlaySound("Label");

        Item newItem = newGameObj.GetComponent<Item>();
        Item listItem = listOfItems[0].GetComponent<Item>();

        newItem.name = listItem.name;
        newItem.SetIcon(listItem.GetIcon());
        newItem.SetAddress(listItem.GetAddress());
        newItem.SetCode(listItem.GetCode());

        listOfItems.Remove(listOfItems[0]);

        yield return new WaitForSeconds(secondsToWait);
        Check();
    }
    //Checking if the spawnLocation is free
    public void OnTriggerEnter2D(Collider2D collision)
    {
        emptySpawnLocation = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        emptySpawnLocation = true;
    }
}
