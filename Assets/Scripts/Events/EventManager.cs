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
}
