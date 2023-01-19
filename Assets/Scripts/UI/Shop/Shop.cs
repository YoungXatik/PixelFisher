using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    #region Singleton

    public static Shop Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    [SerializeField] private GameObject shopMainImage;

    [SerializeField] private GameObject diamondsScrollArea;
    [SerializeField] private GameObject boostersScrollArea;
    [SerializeField] private Transform diamondsScrollAreaContainer,boostersScrollAreaContainer;

    [SerializeField] private Button diamondsMenuButton;
    [SerializeField] private Button boostersMenuButton;

    [SerializeField] private Button closeShopButton;
    [SerializeField] private Button openShopButton;

    [SerializeField] private float diamondsScrollAreaStartYPosition, boostersScrollAreaStartYPosition;
    
    private void Start()
    {
        closeShopButton.interactable = false;
        diamondsMenuButton.interactable = false;
        boostersMenuButton.interactable = false;
    }

    public void OpenShop()
    {
        EventManager.OnUIMenuEnterInvoke();
        shopMainImage.SetActive(true);
        shopMainImage.transform.DOScale(1, 0.25f).SetEase(Ease.Linear).From(0).OnComplete(delegate
        {
            closeShopButton.interactable = true;
            openShopButton.interactable = false;
            diamondsMenuButton.interactable = true;
            boostersMenuButton.interactable = true;
            OpenBoosterArea();
        });
    }

    public void CloseShop()
    {
        EventManager.OnUIMenuExitInvoke();
        CloseDiamondsArea();
        shopMainImage.transform.DOScale(0, 0.25f).SetEase(Ease.Linear).From(1).OnComplete(delegate
        {
            openShopButton.interactable = true;
            closeShopButton.interactable = false;
            diamondsMenuButton.interactable = false;
            boostersMenuButton.interactable = false;
        });
    }

    public void OpenDiamondsArea()
    {
        CloseBoosterArea();
        diamondsMenuButton.interactable = false;
        closeShopButton.interactable = false;
        diamondsScrollArea.transform.DOScale(1, 0.25f).SetEase(Ease.Linear).From(0).OnComplete(delegate
        {
            closeShopButton.interactable = true;
            diamondsScrollAreaContainer.position = new Vector3(diamondsScrollAreaContainer.position.x,diamondsScrollAreaStartYPosition,diamondsScrollAreaContainer.position.z);
        });
    }

    public void CloseDiamondsArea()
    {
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
        boostersMenuButton.interactable = false;
        closeShopButton.interactable = false;
        boostersScrollArea.transform.DOScale(1, 0.25f).SetEase(Ease.Linear).From(0).OnComplete(delegate
        {
            closeShopButton.interactable = true;
            boostersScrollAreaContainer.position = new Vector3(boostersScrollAreaContainer.position.x,boostersScrollAreaStartYPosition,boostersScrollAreaContainer.position.z);
        });
    }

    public void CloseBoosterArea()
    {
        closeShopButton.interactable = false;
        boostersScrollArea.transform.DOScale(0, 0.25f).SetEase(Ease.Linear).From(0).OnComplete(delegate
        {
            boostersMenuButton.interactable = true;
            closeShopButton.interactable = true;
        });
    }
}
