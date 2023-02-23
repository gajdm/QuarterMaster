using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager { get; private set; }

    //Managers

    [SerializeField] private BuyerManager buyerManager;
    [SerializeField] private ManufacturerManager manuManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LogsSystem logsSystem;

    //Items
    [SerializeField] private int itemNumber;
    [SerializeField] private string itemAddress;
    [SerializeField] private string itemCode;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private GameObject cratePrefab;
    [SerializeField] private ImportPortal importPortal;

    [SerializeField] private List<GameObject> items;


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
        if (Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();
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

}
