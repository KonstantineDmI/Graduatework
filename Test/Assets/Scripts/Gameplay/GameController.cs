using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.FactorySystem;
using Gameplay.FactorySystem.Items;
using Gameplay.FactorySystem.Storage;
using Gameplay.PlayerBehaviour;
using UnityEngine;

namespace Gameplay
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private List<StorageBase> storages;
        [SerializeField] private List<FactoryBase> factories;
        [SerializeField] private WalletController walletController;
        [SerializeField] private ItemsPool itemsPool;

        private Coroutine _triggerActionRoutine;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            storages.ForEach(s =>
            {
                s.OnPlayerEnter += StartTriggerActionTracking;
                s.OnPlayerExit += StopTriggerActionTracking;
            });
        }

        private void StartTriggerActionTracking(StorageBase storage, List<int> itemsIds, int value)
        {
            _triggerActionRoutine = StartCoroutine(TriggerActionCoroutine(storage, itemsIds, value));
        }

        private void StopTriggerActionTracking()
        {
            StopCoroutine(_triggerActionRoutine);
            _triggerActionRoutine = null;
        }

        private IEnumerator TriggerActionCoroutine(StorageBase storage, List<int> itemsIds, int value)
        {
            while (true)
            {
                yield return new WaitForSeconds(playerController.GrabDuration);
                TriggerAction(storage, itemsIds, value);
            }
        }


        private void TriggerAction(StorageBase storage, List<int> itemsIds, int value)
        {
            itemsIds.ForEach(itemId =>
            {
                switch (storage.StorageTypes)
                {
                    case StorageTypes.Input:
                    {
                        if (walletController.HaveNoItems(itemId) || storage.FullOfItemsById(itemId))
                        {
                            return;
                        }
                        walletController.DecreaseWallet(itemId, value);
                        storage.FillStorage(itemId, value);
                        if (!playerController.BackPackView.ItemByIdIsExist(itemId))
                        {
                            return;
                        }
                        var item = playerController.BackPackView.GetItemById(itemId);
                        itemsPool.ItemsVisualAnimation.MakeTransitionAnimation(item,playerController.BackPackView.RemoveItem(itemId),storage.transform, true);
                        break;
                    }
                    case StorageTypes.Output:
                    {
                        if (walletController.StackIsFull(itemId) || storage.StorageIsEmpty())
                        {
                            return;
                        }
                    
                        walletController.IncreaseWallet(itemId, value);
                        storage.GrabFromStorage(itemId, value);
                        if (!playerController.BackPackView.AllSlotsAreBusy())
                        {
                            var item = itemsPool.GetFreeItemById(itemId);
                            itemsPool.ItemsVisualAnimation.MakeTransitionAnimation(item, storage.transform,
                            playerController.BackPackView.AddItem(item),false);
                        }

                        break;
                    }
                }
            });
        }
    }
}
