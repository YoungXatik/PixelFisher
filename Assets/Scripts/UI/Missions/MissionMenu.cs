using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MissionMenu : MonoBehaviour
{
    [SerializeField] private Transform menuImage;

    [SerializeField] private Transform questsArea;
    [SerializeField] private Button questsAreaButton;
    
    [SerializeField] private Transform dailyMissionsArea;
    [SerializeField] private Button dailyMissionButton;

    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    public void OpenMenu()
    {
        menuImage.gameObject.SetActive(true);
        openButton.interactable = false;
        closeButton.interactable = false;
        menuImage.DOScale(1, 0.25f).From(0).SetEase(Ease.Linear).OnComplete(delegate
        {
            closeButton.interactable = true;
        });
    }

    public void CloseMenu()
    {
        openButton.interactable = false;
        closeButton.interactable = false;
        menuImage.DOScale(0, 0.25f).From(1).SetEase(Ease.Linear).OnComplete(delegate
        {
            openButton.interactable = true;
            menuImage.gameObject.SetActive(false);
        });
    }

    public void OpenQuestMenu()
    {
        CloseDailyMissionMenu();
        questsAreaButton.interactable = false;
        dailyMissionButton.interactable = false;
        questsArea.DOScale(1, 0.25f).From(0).SetEase(Ease.Linear).OnComplete(delegate
        {
            dailyMissionButton.interactable = true;
        });
    }

    public void CloseQuestMenu()
    {
        questsAreaButton.interactable = false;
        dailyMissionButton.interactable = false;
        questsArea.DOScale(0, 0.25f).From(1).SetEase(Ease.Linear).OnComplete(delegate
        {
            questsAreaButton.interactable = true;
        });
    }
    
    public void OpenDailyMissionMenu()
    {
        CloseQuestMenu();
        questsAreaButton.interactable = false;
        dailyMissionButton.interactable = false;
        dailyMissionsArea.DOScale(1, 0.25f).From(0).SetEase(Ease.Linear).OnComplete(delegate
        {
            questsAreaButton.interactable = true;
        });
    }

    public void CloseDailyMissionMenu()
    {
        questsAreaButton.interactable = false;
        dailyMissionButton.interactable = false;
        dailyMissionsArea.DOScale(0, 0.25f).From(1).SetEase(Ease.Linear).OnComplete(delegate
        {
            dailyMissionButton.interactable = true;
        });
    }
    
}
