using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoulGames.EasyGridBuilderPro;

public class ConveyourBelt : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private bool isBuilt;

    //CONVEYOUR VARIABLES
    [Header("Conveyoour settings")]
    [SerializeField] private bool placed;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject horse;

    [SerializeField] private bool full;
    [SerializeField] private bool seeNextCon;
    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField] private ConveyourBelt nextConveyor;
    [SerializeField] private GameObject itemOnBelt;

    [SerializeField] private Transform checkLocation;


    //BUILDING FUNCTIONS
    private void OnEnable()
    {
        MultiGridBuildConditionManager.OnBuildConditionCheckBuildableGridObject += CheckBuildConditionBuildableGridObject;
        MultiGridBuildConditionManager.OnBuildConditionCompleteBuildableGridObject += CompleteBuildConditionBuildableGridObject;
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
                    if (this.gameObject.name == "Conveyor_Down(Clone)" || this.gameObject.name == "Conveyor_Right(Clone)" ||
                        this.gameObject.name == "Conveyor_Up(Clone)" || this.gameObject.name == "Conveyor_Left(Clone)")
                    { isBuilt = true; }
                    else if (!isBuilt)
                    {
                        isBuilt = true;
                        gameManager = FindObjectOfType<GameManager>();
                        gameManager.PayGold(buildableGridObjectTypeSO.buildConditionSO.goldAmount);
                        placed=true;
                    }
                }
                //And you can leave rest of the code from here as it is
            }
        }
    }

    //CONVEYOR FUNCTIONS

    public void FixedUpdate()
    {
        if(placed)
        {
            boxCollider.enabled = true;
            if (!seeNextCon)
            {
                seeNextCon = true;
                Debug.Log("Casting for next" + this.transform.position.y);

                RaycastHit2D hit = Physics2D.Raycast(checkLocation.transform.position,
                new Vector2(checkLocation.transform.position.x + 1, checkLocation.transform.position.y), 2f);

                Debug.DrawLine(checkLocation.transform.position,
                    new Vector2(checkLocation.transform.position.x + 1, checkLocation.transform.position.y), Color.red, 5f);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "Conveyor")
                    {
                        Debug.Log("Getting next conveyor");
                        nextConveyor = hit.collider.gameObject.GetComponent<ConveyourBelt>();
                    }
                }
            }
            if (nextConveyor != null)
            {
                if (full)
                {
                    if (!nextConveyor.GetFull())
                    {
                        Give();
                    }
                    else return;
                }
                else return;
            }
            if (!full) CheckForItem();
        }
    }
        
    public void UnParentItem()
    {
        foreach(Transform child in horse.transform)
        {
            child.parent = null;
        }
    }

    //Checking "fun"ctions

    public void CheckForItem()
    {
        Debug.Log("Casting for item " + this.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position,
            new Vector2(this.gameObject.transform.position.x + 1, this.gameObject.transform.position.y), 2f);

        Debug.DrawLine(this.gameObject.transform.position,
            new Vector2(this.gameObject.transform.position.x + 1, this.gameObject.transform.position.y), Color.blue, 5f);

        if (hit.collider != null)
        {
            if(hit.collider.gameObject.tag =="Item"|| hit.collider.gameObject.tag == "Crate"|| hit.collider.gameObject.tag == "Bag")
            {
                Receive(hit.collider.gameObject);
            }
        }
    }
    
    //Getting and giving functions
    public void Receive(GameObject item)
    {
        full = true;
        itemOnBelt=item;
        item.transform.SetParent(horse.transform);
        item.transform.position = horse.transform.position;
    }
    public void Give()
    {
        full=false;
        animator.SetTrigger("GiveNext");
    }
    
    //Animator functions
    public void AnimGiveNext()
    {
        itemOnBelt.transform.SetParent(null);
        nextConveyor.Receive(itemOnBelt);
    }
    //Utillity functions
    public bool GetFull()
    {return full;}


}
