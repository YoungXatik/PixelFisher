using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject[] uiObjects;
    [SerializeField] private Button playButton;
    
    private Animator _uiAnimator;

    private void Start()
    {
        _uiAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.OnGameStarted += PlayDisableAnimation;
        EventManager.OnGameEnded += PlayEnableAnimation;
    }

    private void OnDisable()
    {
        EventManager.OnGameStarted -= PlayDisableAnimation;
        EventManager.OnGameEnded -= PlayEnableAnimation;
    }

    private void PlayDisableAnimation()
    {
        playButton.interactable = false;
        _uiAnimator.SetBool("start",true);
        _uiAnimator.SetBool("idle",false);
    }

    private void PlayEnableAnimation()
    {
        playButton.interactable = true;
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
