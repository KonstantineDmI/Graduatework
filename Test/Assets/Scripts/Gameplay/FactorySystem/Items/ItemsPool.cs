using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.FactorySystem.Items
{
    public class ItemsPool : MonoBehaviour
    {
        [SerializeField] private List<ItemView> itemsViews;
        [SerializeField] private ItemsVisualAnimation itemsAnimation;

        public ItemsVisualAnimation ItemsVisualAnimation => itemsAnimation;

        public ItemView GetFreeItemById(int id)
        {
            var currentItem = itemsViews.FindAll(i => !i.gameObject.activeSelf).Find(i => i.Id == id);
            currentItem.transform.parent = null;
            
            return currentItem;
        }
    }
}
