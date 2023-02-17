using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager { get; private set; }

    //Managers

    [SerializeField]private BuyerManager buyerManager;
    [SerializeField]private ManufacturerManager manuManager;
    [SerializeField]private UIManager uiManager;
    [SerializeField]private LogsSystem logsSystem;

    //Items
    [SerializeField] private int itemNumber;
    [SerializeField] private string itemAddress;
    [SerializeField] private string itemCode;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private GameObject cratePrefab;
    [SerializeField] private ImportPortal importPortal;

    [SerializeField] private int numberOfItems;


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
    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();
    }
    public void NewOrder(string order)
    {
        if (manuManager == null) manuManager = FindObjectOfType<ManufacturerManager>();
        numberOfItems = manuManager.GetNumberOfItems();
        CreateItem(order);
        CreateCrate();
    }
    public void CreateItem(string order)
    {
        itemAddress = order;
        itemCode = order;
    }
    public void CreateCrate()
    {
       
        //cratePrefab.GetComponent<Crate>().SetItems();
        importPortal.StoreCrate(cratePrefab);
        logsSystem.AddLog("Created a crate with "+itemNumber+" items");
    }


}
