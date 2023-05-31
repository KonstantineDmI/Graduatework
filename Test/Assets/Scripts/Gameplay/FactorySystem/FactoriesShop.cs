using Gameplay.PlayerBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class FactoriesShop : MonoBehaviour
{
    public List<FactoryShopEntity> factoriesShopEntities;
    public List<FactoryGameObject> factories;
    public List<FactoryBuySpot> factoriesBuySpots;
    public WalletController wallet;
    public PurchaseFactoryView view;


    private void Awake()
    {
        factoriesBuySpots.ForEach(x =>
        {
            x.OnBuy += PurchaseFactory;
            x.OnActive += ActivateUI;
        });
        InitFactories();
    }

    private void InitFactories()
    {
        factories.ForEach(factory =>
        {
            factory.objectToDisable.SetActive(true);
            factory.objectToEnable.SetActive(false);
            if (factoriesShopEntities.Find(x => x.id == factory.id).unlocked)
            {
                factory.objectToDisable.SetActive(false);
                factory.objectToEnable.SetActive(true);
            }
        });
    }

    private void ActivateUI(int id, bool state)
    {
        if (state)
        {
            var factoryEntity = factoriesShopEntities.Find(x => x.id == id);
            view.SetText(factoryEntity.name, factoryEntity.price.ToString());
        }

        view.Activate(state);
    }

    private void PurchaseFactory(int id)
    {
        var factoryEntity = factoriesShopEntities.Find(x => x.id == id);

        if (factoryEntity.unlocked)
        {
            return;
        }

        if (wallet.Balance - factoryEntity.price >= 0)
        {
            factoriesShopEntities.Find(x => x.id == id).unlocked = true;
            InitFactories();
            wallet.DecreaseBalance(factoryEntity.price);
            ActivateUI(0, false);
        }
    }
}
