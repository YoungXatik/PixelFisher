using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OnButtonClick : MonoBehaviour
{
    private Vector3 _defaultScale = Vector3.one;

    public void OnClick()
    {
        gameObject.transform.DOScale(_defaultScale - Vector3.one * 0.08f, 0.1f).SetLoops(2, LoopType.Yoyo).From(_defaultScale).SetEase(Ease.Linear);
    }
}
