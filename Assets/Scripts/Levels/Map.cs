using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField] private Image menuImage;

    [SerializeField] private Button openButton, closeButton;

    [SerializeField] private GameObject indicator;

    private void OnEnable()
    {
        EventManager.OnNewLevelOpened += ShowIndicator;
    }

    private void OnDisable()
    {
        EventManager.OnNewLevelOpened -= ShowIndicator;
    }

    public void OpenMenu()
    {
        EventManager.OnUIMenuEnterInvoke();
        indicator.SetActive(false);
        openButton.interactable = false;
        menuImage.transform.DOScale(1, 0.25f).From(0).SetEase(Ease.Linear).OnComplete(delegate
        {
            closeButton.interactable = true;
        });
    }

    public void CloseMenu()
    {
        EventManager.OnUIMenuExitInvoke();
        closeButton.interactable = false;
        menuImage.transform.DOScale(0, 0.25f).From(1).SetEase(Ease.Linear).OnComplete(delegate
        {
            openButton.interactable = true;
        });
    }

    private void ShowIndicator()
    {
        indicator.SetActive(true);
    }

    private void HideIndicator()
    {
        indicator.SetActive(false);
    }
    
}
