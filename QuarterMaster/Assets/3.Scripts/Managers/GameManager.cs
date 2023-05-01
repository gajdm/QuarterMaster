using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager { get; private set; }

    //Managers
    [Space][Header("MANAGERS")]
    [SerializeField] private BuyerManager buyerManager;
    [SerializeField] private ManufacturerManager manuManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LogsSystem logsSystem;
    [SerializeField] private AudioManager audioManager;

    //Items
    [Space][Header("ITEMS")]
    [SerializeField] private int itemNumber;
    [SerializeField] private string itemAddress;
    [SerializeField] private string itemCode;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private GameObject cratePrefab;
    [SerializeField] private ImportPortal importPortal;
    [SerializeField] private List<GameObject> items;

    //Economy

    [Space][Header("ECONOMY")]
    [SerializeField] private int goldCurrent;
    [SerializeField] private int goldStarting;
    [SerializeField] private Text itemGoldText;
    [SerializeField] private Text manuGoldText;

    [SerializeField] private Text spentText;
    [SerializeField] private Animator animator;

    //Build
    [Space][Header("BUILD")]
    [SerializeField] private bool buildBool;
    [SerializeField] private ItemInteraction playerBrain;
    [SerializeField] private PlayerMovement playerMovement;


    //FUNCTIONS
    public void Awake()
    {
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(this);
        }
        else if (Manager != this)
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        UpdateGoldTexts();
    }
    public void NewOrder(string order)
    {
        items.Clear();

        if (manuManager == null) manuManager = FindObjectOfType<ManufacturerManager>();
        items = manuManager.GetListOfItems();

        foreach (GameObject gameObj in items)
        {
            gameObj.GetComponent<Item>().SetAddress(order);
            gameObj.GetComponent<Item>().SetCode(order);
        }

        buyerManager.AddOrder(items);

        CreateCrate(items);


    }
    public void CreateCrate(List<GameObject> list)
    {
        GameObject GO = cratePrefab;
        Crate newCrate = GO.GetComponent<Crate>();

        newCrate.SetAmount(list.Count);
        newCrate.SetItems(list);

        importPortal.StoreCrate(GO);
        logsSystem.AddLog("Created a crate with " + list.Count + " items");
    }
    public void StopGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        Application.Quit(); 
    }

    //ECONOMY FUNCTIONS
    public int GetGoldCurrent()
    { 
        return goldCurrent; 
    }
    public void CheckManuButton(int price, ManuButton manButton, Manufacturer man)
    {
        if(price <= goldCurrent)
        {
            PayGold(price);
            manButton.SetAvailable(true);
            manButton.UpdateButton(man);
        }
    }
    public void PayGold(int price)
    {
        audioManager.PlaySound("Pay");
        spentText.text = price.ToString();
        animator.SetTrigger("Pay");

        goldCurrent -= price; 
        UpdateGoldTexts(); 
    }
    public void ReceiveGold(int amount)
    {
        spentText.text = amount.ToString();
        animator.SetTrigger("Get");

        goldCurrent += amount;
        UpdateGoldTexts();
        FindObjectOfType<QuestManager>().GoldEarned(amount);
    }
    public void UpdateGoldTexts()
    { 
        itemGoldText.text = goldCurrent.ToString(); 
        manuGoldText.text = goldCurrent.ToString();
    } 

    //Build Mode

    public void ToggleBuildMode()
    {
        if(buildBool)
        {
            buildBool = false;

            playerBrain.SetCanInteract(true);
            playerMovement.SetCanMove(true);

        }
        else
        {
            buildBool = true;

            playerBrain.SetCanInteract(false);
            playerMovement.SetCanMove(false);

        }
    }

}
