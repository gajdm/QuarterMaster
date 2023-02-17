using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelStation : MonoBehaviour
{
    [SerializeField] private List<Item> listOfItems;
    [SerializeField] private GameObject itemObject;
    [SerializeField] private Crate currentCrate;
    //UNPACKING VARIABLES
    [SerializeField] private int secondsToWait;
    [SerializeField] private bool emptySpawnLocation;
    [SerializeField] private Transform spawnTransfrom;

    public void GetNewCrate(Crate crate)
    {
        if (currentCrate == null)
        {
            currentCrate = crate;
            listOfItems = currentCrate.GetListOfItems();
            StartCoroutine(Unpacking());
        }
        else
            Debug.Log("I'm full");
    }
    public IEnumerator Unpacking()
    {
        if (listOfItems != null )
        {
            Item item = itemObject.GetComponent<Item>();
            Instantiate(itemObject, spawnTransfrom);
            item = listOfItems[0];
            listOfItems.Remove(item);
        }
        else
        {
            Debug.Log("Stopping Coro");
            StopCoroutine(Unpacking());
        } 

        yield return new WaitForSeconds(secondsToWait);
        StartCoroutine(Unpacking());
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
