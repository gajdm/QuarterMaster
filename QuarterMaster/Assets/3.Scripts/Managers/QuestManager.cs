using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoulGames.Utilities;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    public GameObject questSpace;

    public Text text;

    [SerializeField] private int currentQuestInt;
    [SerializeField] private Quest currentQuest;
    
    [Header("Booleans")]
    [Header("Backstage"), Space(10f)]
    
    [SerializeField] private bool aquiredMan;
    [SerializeField] private bool label;
    [SerializeField] private bool sort;
    [SerializeField] private bool orderFinish;
    [Header("Integers")]
    [SerializeField] private int labelBuild;
    [SerializeField] private int racksBuild;
    [SerializeField] private int goldEarned;
    [SerializeField] private int ordersFinished;


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

                break;
        } 
    }
    //Action functions referenced from outside of the manager
    public void AcquiredManufacturer()
    {
        aquiredMan = true;
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
        orderFinish = true;
        CheckQuest();
    }
    public void GoldEarned(int number)
    {
        goldEarned += number;
        CheckQuest();
    }

    //Utillity functions
    public void ResetBackend()
    {
         aquiredMan = false;
         label = false;
         sort = false;
         orderFinish = false;
        
         labelBuild = 0;
         racksBuild = 0;
         goldEarned = 0;
         ordersFinished = 0;
    }
}
