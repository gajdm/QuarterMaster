using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LabelStation : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private List<GameObject> listOfItems;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private List<Crate> crateList;
    [SerializeField] private Crate currentCrate;
    //UNPACKING VARIABLES
    [SerializeField] private int secondsToWait;
    [SerializeField] private bool emptySpawnLocation;
    [SerializeField] private Transform spawnTransfrom;

    
    private void OnEnable()
    {
        audioManager = FindObjectOfType<AudioManager>();
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
        audioManager.PlaySound("Label");

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
