using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoulGames.Utilities;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    public GameObject questSpace;

    public Text text;

    [SerializeField] private int currentQuestInt;
    [SerializeField] private Quest currentQuest;
    
    [Header("Booleans")]
    [Header("Backstage"), Space(10f)]
    
    [SerializeField] public bool acquiredMan;
    [SerializeField] public bool upgradedMan;
    [SerializeField] private bool label;
    [SerializeField] private bool sort;
    [SerializeField] private bool orderFinish;
    [Header("Integers")]
    [SerializeField] private int labelBuild = 0;
    [SerializeField] private int racksBuild = 0;
    [SerializeField] private int goldEarned = 0;
    [SerializeField] private int ordersFinished = 0;


    private void Start()
    {
        currentQuestInt = 0;
        AssignQuest(quests[currentQuestInt]);
    }
    private void AssignQuest(Quest quest)
    {
        currentQuest = quest;

        questSpace.GetComponent<ToolTip>().SetContent(quest.tooltip);
        text.text = quest.task;

        ResetBackend();
    }

    public void CheckQuest()
    {
        switch(currentQuest.questType)
        {
            case Quest.QuestType.AcquireManufacturer:
                if(acquiredMan)
                {
                    currentQuestInt++;
                    AssignQuest(quests[currentQuestInt]);
                }
                break;
            case Quest.QuestType.BuildRacks:
                if(currentQuest.racksToBuild <= racksBuild)
                {
                    currentQuestInt++;
                    AssignQuest(quests[currentQuestInt]);
                }
                break;
            case Quest.QuestType.BuildLabels:
                if(currentQuest.labelsToBuild <= labelBuild)
                {
                    currentQuestInt++;
                    AssignQuest(quests[currentQuestInt]);
                }
                break;
            case Quest.QuestType.Label:
                if(label)
                {
                    currentQuestInt++;
                    AssignQuest(quests[currentQuestInt]);
                }
                    break;
            case Quest.QuestType.Sort:
                if(sort)
                {
                    currentQuestInt++;
                    AssignQuest(quests[currentQuestInt]);
                }
                break;
            case Quest.QuestType.FinishOrders:
                if(currentQuest.ordersToFinish<=ordersFinished)
                {
                    currentQuestInt++;
                    AssignQuest(quests[currentQuestInt]);
                }
                break;
            case Quest.QuestType.UpgradeManufacturers:
                if(upgradedMan)
                {
                    currentQuestInt++;
                    AssignQuest(quests[currentQuestInt]);
                }
                break;
            case Quest.QuestType.EarnGold:
                if(currentQuest.goldToEarn <= goldEarned)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    currentQuestInt++;
                    AssignQuest(quests[currentQuestInt]);
                }
                break;
            default:
                break;
        } 
    }
    //Action functions referenced from outside of the manager
    public void AcquiredManufacturer()
    {
        acquiredMan = true;
        CheckQuest();
    }
    public void BuildLabelling()
    {
        labelBuild++;
        CheckQuest();
    }
    public void BuildRack()
    {
        racksBuild++;
        CheckQuest();
    }
    public void Labelled()
    {
        label = true;
        CheckQuest();
    }
    public void Sorted()
    {
        sort = true;
        CheckQuest();
    }
    public void OrderFinished()
    {
        ordersFinished++;
        CheckQuest();
    }
    public void GoldEarned(int number)
    {
        goldEarned += number;
        CheckQuest();
    }
    public void UpgradeMan()
    {
        upgradedMan = true;
        CheckQuest();
    }

    //Utillity functions
    public void ResetBackend()
    {
         acquiredMan = false;
         label = false;
         sort = false;
         orderFinish = false;
            upgradedMan=false;
        
         labelBuild = 0;
         racksBuild = 0;
         goldEarned = 0;
         ordersFinished = 0;
    }
}
