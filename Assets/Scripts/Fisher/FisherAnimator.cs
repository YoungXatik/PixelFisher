using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisherAnimator : MonoBehaviour
{
    [SerializeField] private Animator fisherAnimator;

    public void PlayFishingAnimation()
    {
        fisherAnimator.SetBool("fishing",true);
        fisherAnimator.SetBool("idle",false);
        EventManager.OnGameStartedInvoke();
    }

    public void EndFishingAnimation()
    {
        fisherAnimator.SetBool("fishing",false);
    }

    public void ToIdleState()
    {
        fisherAnimator.SetBool("idle",true);
    }
}
