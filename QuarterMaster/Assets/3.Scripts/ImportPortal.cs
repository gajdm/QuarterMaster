using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportPortal : MonoBehaviour
{
    [SerializeField] private List<GameObject> crates = new List<GameObject>();
    [SerializeField] private GameObject actionCrate;
    [SerializeField] private Transform spawnLocation;
    public void StoreCrate(GameObject crate)
    {
        actionCrate = crate;
        crates.Add(actionCrate);
        if (crates.Count == 1) SpawnCrate();
    }
    public void SpawnCrate()
    {
        Instantiate(crates[0], spawnLocation);
        crates.Remove(crates[0]);
    }
}
