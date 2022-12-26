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
}
