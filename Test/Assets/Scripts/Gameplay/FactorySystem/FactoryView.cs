using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.FactorySystem
{
    public class FactoryView : MonoBehaviour
    {
        [SerializeField] private Image consumableItemImage;

        private int _itemId;

        public int ItemId
        {
            get => _itemId;
            set => _itemId = value;
        }

        public void SetSprite(Sprite sprite)
        {
            consumableItemImage.sprite = sprite;
        }
    }
}
