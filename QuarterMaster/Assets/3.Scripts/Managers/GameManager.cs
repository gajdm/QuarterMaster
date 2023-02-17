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

    [SerializeField] private List<Item> items;


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
        items.Clear();

        if (manuManager == null) manuManager = FindObjectOfType<ManufacturerManager>();
        items = manuManager.GetListOfItems();

        foreach (Item item in items)
        {
            item.SetAddress(itemAddress);
            item.SetCode(itemCode);
        }

        CreateCrate(items);
    }
    public void CreateCrate(List<Item> list)
    {
        GameObject GO = cratePrefab;
        Crate newCrate = GO.GetComponent<Crate>();

        newCrate.SetAmount(list.Count);
        newCrate.SetItems(list);

        importPortal.StoreCrate(GO);
        logsSystem.AddLog("Created a crate with "+list.Count+" items");
    }


}
