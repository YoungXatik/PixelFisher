using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopMainImage;

    [SerializeField] private GameObject diamondsScrollArea;
    [SerializeField] private GameObject boostersScrollArea;

    [SerializeField] private Button diamondsMenuButton;
    [SerializeField] private Button boostersMenuButton;

    [SerializeField] private Button closeShopButton;
    [SerializeField] private Button openShopButton;

    private void Start()
    {
        closeShopButton.interactable = false;
        diamondsMenuButton.interactable = false;
        boostersMenuButton.interactable = false;
    }

    public void OpenShop()
    {
        shopMainImage.SetActive(true);
        shopMainImage.transform.DOScale(1, 0.25f).SetEase(Ease.Linear).From(0).OnComplete(delegate
        {
            closeShopButton.interactable = true;
            openShopButton.interactable = false;
            diamondsMenuButton.interactable = true;
            boostersMenuButton.interactable = true;
        });
    }

    public void CloseShop()
    {
        CloseBoosterArea();
        CloseDiamondsArea();
        shopMainImage.transform.DOScale(0, 0.25f).SetEase(Ease.Linear).From(1).OnComplete(delegate
        {
            openShopButton.interactable = true;
            closeShopButton.interactable = false;
            diamondsMenuButton.interactable = false;
            boostersMenuButton.interactable = false;
            shopMainImage.SetActive(false);
        });
    }

    public void OpenDiamondsArea()
    {
        CloseBoosterArea();
        diamondsScrollArea.SetActive(true);
        diamondsMenuButton.interactable = false;
        closeShopButton.interactable = false;
        diamondsScrollArea.transform.DOScale(1, 0.25f).SetEase(Ease.Linear).From(0).OnComplete(delegate
        {
            closeShopButton.interactable = true;
        });
    }

    public void CloseDiamondsArea()
    {
        diamondsScrollArea.SetActive(false);
        closeShopButton.interactable = false;
        diamondsScrollArea.transform.DOScale(0, 0.25f).SetEase(Ease.Linear).From(0).OnComplete(delegate
        {
            diamondsMenuButton.interactable = true;
            closeShopButton.interactable = true;
        });
    }
    
    public void OpenBoosterArea()
    {
        CloseDiamondsArea();
        boostersScrollArea.SetActive(true);
        boostersMenuButton.interactable = false;
        closeShopButton.interactable = false;
        boostersScrollArea.transform.DOScale(1, 0.25f).SetEase(Ease.Linear).From(0).OnComplete(delegate
        {
            closeShopButton.interactable = true;
        });
    }

    public void CloseBoosterArea()
    {
        boostersScrollArea.SetActive(false);
        closeShopButton.interactable = false;
        boostersScrollArea.transform.DOScale(0, 0.25f).SetEase(Ease.Linear).From(0).OnComplete(delegate
        {
            boostersMenuButton.interactable = true;
            closeShopButton.interactable = true;
        });
    }
}
