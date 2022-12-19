using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject[] uiObjects;
    private Animator _uiAnimator;

    private void Start()
    {
        _uiAnimator = GetComponent<Animator>();
        EventManager.OnGameStarted += PlayDisableAnimation;
        EventManager.OnGameEnded += PlayEnableAnimation;
    }

    private void PlayDisableAnimation()
    {
        _uiAnimator.SetBool("start",true);
        _uiAnimator.SetBool("idle",false);
    }

    private void PlayEnableAnimation()
    {
        _uiAnimator.SetBool("start",false);
    }

    private void ToIdleState()
    {
        _uiAnimator.SetBool("idle",true);
    }
    
    private void DisableUI()
    {
        for (int i = 0; i < uiObjects.Length; i++)
        {
            uiObjects[i].SetActive(false);
        }
    }

    private void EnableUI()
    {
        for (int i = 0; i < uiObjects.Length; i++)
        {
            uiObjects[i].SetActive(true);
        }
    }
}
