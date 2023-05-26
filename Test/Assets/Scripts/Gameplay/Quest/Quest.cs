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

    private void Start()
    {
        InitializeQuest();
        InitializeSideQuest();
    }

    private void InitializeQuest()
    {
        sideQuests.ForEach(q => q.OnQuestPassed += InitializeSideQuest);
    }

    private void InitializeSideQuest()
    {
        sideQuests.ForEach(q => q.questIsActive = false);
        Debug.Log(sideQuests.First(q => !q.questIsPassed && !q.questIsActive));
        var here = sideQuests.First(q => !q.questIsPassed && !q.questIsActive).questIsActive = true;
    }
}
