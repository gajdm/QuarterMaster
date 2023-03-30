using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string task;
    public string tooltip;
    public enum QuestType
    {
        AcquireManufacturer,
        BuildRacks,
        BuildLabels,
        FinishOrders,
        Label,
        Sort,
        UpgradeManufacturers,
        EarnGold,

    }
    public QuestType questType;

    public int racksToBuild;
    public int labelsToBuild;
    public int goldToEarn;
    public int ordersToFinish;
}
