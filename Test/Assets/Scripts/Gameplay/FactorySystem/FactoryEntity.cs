using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.FactorySystem.Items;
using Gameplay.FactorySystem.Storage;
using UnityEngine;

namespace Gameplay.FactorySystem
{
    [Serializable]
    public class FactoryEntity
    {
        [Header("Produce settings")]
        [SerializeField] private float manufactureDuration;
        [Space]
        [Header("Items settings")]
        public List<Item> consumableItems;
        public List<Item> manufacturedItems;
        
        [Space] 
        [Header("Storages settings")] 
        public StorageBase storageToFill;
        public StorageBase storageToGrab;

        public event Action<int, Transform> OnGrabItem;
        public event Action<int, Transform> OnPutItem;

        public float ManufactureDuration => manufactureDuration;

        public void InitializeStorages()
        {
            if (HasConsumableItems())
            {
                storageToGrab.InitializeStorage(consumableItems);
            }
            
            storageToFill.InitializeStorage(manufacturedItems);
        }

        public bool HasConsumableItems()
        {
            return consumableItems.Any();
        }

        public bool StorageToGrabHasItems()
        {
            foreach (var item in consumableItems)
            {
                if (!storageToGrab.HasItems(item.id))
                {
                    return false;
                }
            }
            
            return true;
        }

        public bool StorageToFillIsFull()
        {
            return  storageToFill.StorageIsFull();
        }

        public void GrabItems()
        {
            consumableItems.ForEach(i =>
            {
                storageToGrab.GrabFromStorage(i.id, 1);
                OnGrabItem?.Invoke(i.id, storageToGrab.transform);
            });
        }

        public void PutItems()
        {
            manufacturedItems.ForEach(i =>
            {
                storageToFill.FillStorage(i.id, 1);
                OnPutItem?.Invoke(i.id, storageToFill.transform);
            });
        }
    }
}