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
    [SerializeField] private Item itemData;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private List<Item> items = new List<Item>();
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
        if(valie)
        {
            items.Clear();
            if (manuManager == null) manuManager = FindObjectOfType<ManufacturerManager>();
            numberOfItems = manuManager.GetNumberOfItems();
            Debug.Log(numberOfItems);


            for (int i = 0; i < numberOfItems; i++)
            {
                CreateItem(order);
            }

            
            CreateCrate();
            valie = false;

        }
        
        
    }
    public void CreateItem(string order)
    {
        itemData.SetAddress(order);
        itemData.SetCode(order);
        Debug.Log("Item added to the item list: " +itemData.GetAddress());


        items.Add(itemData);

    }
    public void CreateCrate()
    {
        cratePrefab.GetComponent<Crate>().SetItems(items);
        importPortal.StoreCrate(cratePrefab);
    }


}
