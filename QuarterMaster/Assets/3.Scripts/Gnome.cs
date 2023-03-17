using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoulGames.EasyGridBuilderPro;

public class Gnome : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private bool isBuilt;
    private void OnEnable()
    {
        MultiGridBuildConditionManager.OnBuildConditionCheckBuildableGridObject += CheckBuildConditionBuildableGridObject;
        MultiGridBuildConditionManager.OnBuildConditionCompleteBuildableGridObject += CompleteBuildConditionBuildableGridObject;
    }
    private void OnDisable()
    {
        MultiGridBuildConditionManager.OnBuildConditionCheckBuildableGridObject -= CheckBuildConditionBuildableGridObject;
        MultiGridBuildConditionManager.OnBuildConditionCompleteBuildableGridObject -= CompleteBuildConditionBuildableGridObject;
    }
    private void CheckBuildConditionBuildableGridObject(BuildableGridObjectTypeSO buildableGridObjectTypeSO)
    {
        gameManager = FindObjectOfType<GameManager>();
        foreach (BuildableGridObjectTypeSO item in MultiGridBuildConditionManager.BuildableGridObjectTypeSOList)
        {
            if (item == buildableGridObjectTypeSO && item.enableBuildCondition)
            {
                //You can simply replace this if statement with your own conditions
                if (buildableGridObjectTypeSO.buildConditionSO.goldAmount <= gameManager.GetGoldCurrent())
                //And you can leave rest of the code from here as it is
                {
                    MultiGridBuildConditionManager.BuidConditionResponseBuildableGridObject = true;
                    return;
                }
                else
                {
                    MultiGridBuildConditionManager.BuidConditionResponseBuildableGridObject = false;
                    return;
                }
            }
        }
        MultiGridBuildConditionManager.BuidConditionResponseBuildableGridObject = false;
        return;
    }
    private void CompleteBuildConditionBuildableGridObject(BuildableGridObjectTypeSO buildableGridObjectTypeSO)
    {
        foreach (BuildableGridObjectTypeSO item in MultiGridBuildConditionManager.BuildableGridObjectTypeSOList)
        {
            if (item == buildableGridObjectTypeSO && item.enableBuildCondition)
            {
                //You can simply replace this if statement with your own conditions
                if (buildableGridObjectTypeSO.buildConditionSO.payGoldOnBuild)
                {
                    if (this.gameObject.name == "Gnome_Down(Clone)" || this.gameObject.name == "Gnome_Right(Clone)" ||
                        this.gameObject.name == "Gnome_Up(Clone)" || this.gameObject.name == "Gnome_Left(Clone)")
                    { isBuilt = true; }
                    else if (!isBuilt)
                    {
                        isBuilt = true;
                        gameManager = FindObjectOfType<GameManager>();
                        gameManager.PayGold(buildableGridObjectTypeSO.buildConditionSO.goldAmount);
                    }
                }
                //And you can leave rest of the code from here as it is
            }
        }
    }
}
