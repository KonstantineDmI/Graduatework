using Gameplay.FactorySystem.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideQuest : MonoBehaviour
{
    public int id;
    public bool questIsPassed;
    public bool questIsActive;

    public event Action OnQuestPassed;


    public int CurrentAmount
    {
        get => _currentAmount;
        set
        {
            if(_currentAmount + value >= neededAmountOfItems)
            {
                questIsPassed = true;
                OnQuestPassed?.Invoke();
                return;
            }

            _currentAmount += value;

        }
    }


    [SerializeField] public Item itemForQuest;
    [SerializeField] private int neededAmountOfItems;

    private int _currentAmount;

}
