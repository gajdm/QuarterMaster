using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelStation : MonoBehaviour
{
    [SerializeField]private Crate currentCrate;
    [SerializeField] private Transform spawn;
    public int crateCount;
    public GameObject itemBody;
    public int itemCount;
    public GameObject newItem;


    [SerializeField] private float timer;
    [SerializeField] private float timeLimit;
    public void FixedUpdate()
    {
        if(crateCount>0)
        {
            timer += Time.deltaTime;
            if (timer >= timeLimit)
            {
                Debug.Log(itemCount);
                itemCount= itemCount-1;
                timer = 0;
                if(itemCount > 0)
                {
                    itemCount--;
                    RemoveItems();
                }
                else
                    RemoveCrate();
                
            }
        }
    }
    public void RemoveItems()
    {
        itemBody.GetComponent<Item>().Equals(currentCrate.GetItems().Count);
        Debug.Log("LOOK HERE " + itemBody.GetComponent<Item>().GetAddress());
        GameObject newItem = Instantiate(itemBody, spawn);
        newItem.transform.parent = spawn.transform;
        itemCount = currentCrate.GetItems().Count;
    }
    public void AddCrate(Crate crate)
    {
        currentCrate = crate;
        itemCount = crate.GetItems().Count;
        crateCount++;
    }
    public void RemoveCrate()
    {
        currentCrate = null;
        crateCount--;
    }
    public void SetItemCount()
    {
        itemCount--;
    }
}
