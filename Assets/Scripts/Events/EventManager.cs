using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action OnGameStarted;

    public static void OnGameStartedInvoke()
    {
        OnGameStarted?.Invoke();
    }

    public static Action OnGameEnded;

    public static void OnGameEndedInvoke()
    {
        OnGameEnded?.Invoke();
    }

    public static Action OnLengthValueChanged;
    public static Action OnStrengthValueChanged;
    public static Action OnOfflineMoneyValueChanged;

    public static void OnLengthValueChangedInvoke()
    {
        OnLengthValueChanged?.Invoke();
    }
    
    public static void OnStrengthValueChangedInvoke()
    {
        OnStrengthValueChanged?.Invoke();
    }
    
    public static void OnOfflineMoneyValueChangedInvoke()
    {
        OnOfflineMoneyValueChanged?.Invoke();
    }

    public static Action OnMoneyChanged;
    public static Action OnDiamondsChanged;

    public static void OnMoneyChangedInvoke()
    {
        OnMoneyChanged?.Invoke();
    }

    public static void OnDiamondsChangedInvoke()
    {
        OnDiamondsChanged?.Invoke();
    }

    public static Action OnCommonFishHooked;

    public static void OnCommonFishHookedInvoke()
    {
        OnCommonFishHooked?.Invoke();
    }
    
    public static Action OnRareFishHooked;

    public static void OnRareFishHookedInvoke()
    {
        OnRareFishHooked?.Invoke();
    }

    public static Action OnAchievementCollected;

    public static void OnAchievementCollectedInvoke()
    {
        OnAchievementCollected?.Invoke();
    }

    public static Action OnFishCollected;

    public static void OnFishCollectedInvoke()
    {
        OnFishCollected?.Invoke();
    }

    public static Action OnDailyRewardsUpdate;

    public static void OnDailyRewardsUpdateInvoke()
    {
        OnDailyRewardsUpdate?.Invoke();
    }

    public static Action OnNewLevelOpened;

    public static void OnNewLevelOpenedInvoke()
    {
        OnNewLevelOpened?.Invoke();
    }

    public static Action OnUIMenuEnter;

    public static void OnUIMenuEnterInvoke()
    {
        OnUIMenuEnter?.Invoke();
    }
    
    public static Action OnUIMenuExit;

    public static void OnUIMenuExitInvoke()
    {
        OnUIMenuExit?.Invoke();
    }
}
