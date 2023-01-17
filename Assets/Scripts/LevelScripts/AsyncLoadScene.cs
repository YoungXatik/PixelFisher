using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoadScene : MonoBehaviour
{
    private AsyncOperation _asyncOperation;
    [SerializeField] private Image loadBarImage;
    [SerializeField] private TextMeshProUGUI loadBarText;

    [SerializeField] private int sceneToLoadID;

    [SerializeField] private GameObject loadingScreenObject;

    private bool _canTapToLoadLevel;

    public void ActivateScreen()
    {
        loadingScreenObject.SetActive(true);
    }

    public void StartLoading()
    {
        StartCoroutine(LoadSceneCoroutine());
    }
    
    private IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _asyncOperation = SceneManager.LoadSceneAsync(sceneToLoadID);
        _asyncOperation.allowSceneActivation = false;
        while (!_asyncOperation.isDone)
        {
            float progress = _asyncOperation.progress / 0.9f;
            loadBarImage.fillAmount = progress;
            loadBarText.text = $"Loading...{progress * 100}%";

            if (_asyncOperation.progress >= 0.9f && !_asyncOperation.allowSceneActivation)
            {
                _asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
