using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.FactorySystem.Items;
using UnityEngine;

namespace Gameplay.FactorySystem.Storage
{
    [Serializable]
    public class StorageEntity
    {
        [SerializeField] private int capacity;
        [SerializeField] private int maxCapacity;
        
        [SerializeField] private List<ItemsHolder> _itemsHolders;

        public List<ItemsHolder> ItemsHolder => _itemsHolders;
        public int MaxCapacity => maxCapacity;

        public void InitializeMaxCapacity(int currentValue)
        {
            maxCapacity = currentValue;
        }

        public void SetItemsToStorage(List<Item> items)
        {
            _itemsHolders = new List<ItemsHolder>();
            items.ForEach(i =>
            {
                _itemsHolders.Add(new ItemsHolder() {itemId = i.id, itemsCapacity = 0, maxCapacity = (maxCapacity / items.Count)});
            });
        }
    }
}
