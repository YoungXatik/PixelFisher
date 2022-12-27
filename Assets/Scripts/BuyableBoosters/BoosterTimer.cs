using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterTimer : MonoBehaviour
{
    private Image _boosterImage;
    private TextMeshProUGUI _timerText;

    private float _min, _sec;

    private bool _canTick;
    
    private void Start()
    {
        _boosterImage = GetComponent<Image>();
        _timerText = GetComponentInChildren<TextMeshProUGUI>();

        if (_canTick)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (_canTick)
        {
            _timerText.text = $"{Mathf.RoundToInt(_min)}:{Mathf.RoundToInt(_sec)}";

            _sec -= Time.deltaTime;
            if (_sec <= 0)
            {
                if (_min >= 1)
                {
                    _sec = 60;
                    _min--;
                }
                else
                {
                    DeactivateTimer();   
                }
            }
        }
    }

    public void ActivateTimer(float time, Sprite timerIcon)
    {
        _canTick = true;
        _boosterImage.DOFillAmount(0, time).From(1).SetEase(Ease.Linear);
        gameObject.SetActive(true);
        _boosterImage.sprite = timerIcon;
        _min = time / 60;
        _sec = time % 60;
    }

    public void DeactivateTimer()
    {
        _canTick = false;
        gameObject.SetActive(false);
    }
    
}
