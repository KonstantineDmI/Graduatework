using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int id;
    public List<SideQuest> sideQuests;


    public SideQuest GetCurrentSideQuest()
    {
        return sideQuests.Find(q => !q.questIsPassed);
    }

    private void Awake()
    {
        InitializeQuest();
    }

    private void Start()
    {
        InitializeSideQuest();
    }

    private void InitializeQuest()
    {
        sideQuests.ForEach(q => q.OnQuestPassed += InitializeSideQuest);
    }

    private void InitializeSideQuest()
    {
        sideQuests.ForEach(q => q.QuestIsActive = false);
        var here = sideQuests.First(q => !q.questIsPassed && !q.QuestIsActive).QuestIsActive = true;
    }
}
