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

    //Items
    [SerializeField] private int itemNumber;
    [SerializeField] private string itemAddress;
    [SerializeField] private string itemCode;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private GameObject cratePrefab;
    [SerializeField] private ImportPortal importPortal;

    [SerializeField] private int numberOfItems;
    public bool valie = true;

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
        Debug.Log(numberOfItems);
        CreateItem(order);
        CreateCrate();
    }
    public void CreateItem(string order)
    {
        itemAddress = order;
        itemCode = order;
        Debug.Log("Item added to the item list: ");
    }
    public void CreateCrate()
    {
        Debug.Log("Created a crate: The number of items "+itemNumber+" The code of the items are "+itemCode+" The address of the items are "+itemAddress);
        cratePrefab.GetComponent<Crate>().SetItems(itemNumber,itemAddress,itemCode);
        importPortal.StoreCrate(cratePrefab);
    }


}
