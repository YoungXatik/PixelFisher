using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HookController : MonoBehaviour
{
    public static HookController Instance;

    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Transform hookTransform;
    [SerializeField] private Transform fishingHookTransform;
    [SerializeField] private BoxCollider2D hookCollider;

    [SerializeField] private Vector3 startHookPosition;

    [SerializeField] private float hookMaxLength;
    [SerializeField] private float cameraFollowLength = -14;
    private float _trueCameraFollowLength;

    [SerializeField] private float rotationMultiplier = 1.2f;
    [SerializeField] private float maxAngle = 30;
    [SerializeField] private float maximalXPosition, minimalXPosition;

    [SerializeField] private float delay;
    [SerializeField] private float rotationDelay;

    [SerializeField] private Vector3 startHookScale;

    private Vector2 _currentHookPosition;
    private Camera _mainCamera;
    private FisherAnimator _fisherAnimator;

    private bool _canMove;

    private Tween _movementXTween;
    private Tween _movementYTween;

    private bool _canMoveDown;
    private Vector2 _moveDirection;
    private float _trueHookSpeed;
    private bool _hookReachMaxLength;
    private bool _hookGoingUp;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float hookSpeed;

    [SerializeField] private float withoutCameraHookSpeed = 15;
    private float _trueWithoutCameraHookSpeed;

    public List<Fish> hookedFish = new List<Fish>();
    
    [SerializeField] private RewardedAdMenu rewardedAdMenu;
    private float _fishDepthValue;
    private void Awake()
    {
        Instance = this;
        _mainCamera = Camera.main;
        _fisherAnimator = GetComponent<FisherAnimator>();
        startHookScale = hookTransform.localScale;
        hookTransform.localScale = Vector3.zero;
        Application.targetFrameRate = 60;
    }

    public void BoostCameraFollowLenght(float lengthMultiplier, float newSpeedMultiplier)
    {
        cameraFollowLength += (lengthMultiplier * cameraFollowLength);
        withoutCameraHookSpeed *= newSpeedMultiplier;
    }

    public void CancelBoostCameraFollowLength()
    {
        cameraFollowLength = _trueCameraFollowLength;
        withoutCameraHookSpeed = _trueWithoutCameraHookSpeed;
    }

    private void Update()
    {
        XAxisMovement();
    }

    private void OnEnable()
    {
        EventManager.OnLengthValueChanged += UpdateHookLength;
    }

    private void OnDisable()
    {
        EventManager.OnLengthValueChanged -= UpdateHookLength;
    }

    private void Start()
    {
        UpdateHookLength();
        _trueCameraFollowLength = cameraFollowLength;
        _trueWithoutCameraHookSpeed = withoutCameraHookSpeed;
        _trueFishesToTakeHookUp = _fishesToTakeHookUp;
    }

    [SerializeField] private Booster boosterComponent;
    [SerializeField] private Booster strengthBoosterComponent;

    private void UpdateHookLength()
    {
        if (PlayerPrefs.HasKey("HookLengthLevel" + SceneManager.GetActiveScene().name))
        {
            hookMaxLength =
                -boosterComponent.boostedValue[
                    PlayerPrefs.GetInt("HookLengthLevel" + SceneManager.GetActiveScene().name)];
        }
    }

    public float GetHookLength()
    {
        if (PlayerPrefs.HasKey("HookLengthLevel"+ SceneManager.GetActiveScene().name))
        {
            hookMaxLength = -boosterComponent.boostedValue[PlayerPrefs.GetInt("HookLengthLevel" + SceneManager.GetActiveScene().name)] + cameraFollowLength;
            return hookMaxLength;
        }
        else
        {
            return hookMaxLength + cameraFollowLength;
        }
    }

    private void XAxisMovement()
    {
        if (_canMove && Input.GetMouseButton(0))
        {
            Vector3 vector = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = hookTransform.position;
            position.x = Mathf.Clamp(vector.x, minimalXPosition, maximalXPosition);
            _movementXTween = hookTransform.DOMoveX(position.x, delay).SetEase(Ease.Linear);
            _currentHookPosition = hookTransform.position;

            Vector3 rotate = hookTransform.eulerAngles;
            rotate.z = Mathf.Clamp((rotationMultiplier * vector.x), -maxAngle, maxAngle);
            hookTransform.rotation = Quaternion.Euler(rotate);
            fishingHookTransform.rotation = Quaternion.Euler(rotate * 2.5f);
        }
        else if (Input.GetMouseButtonUp(0) && _canMove)
        {
            if (_movementXTween != null)
            {
                _movementXTween.Kill();
                hookTransform.DORotate(new Vector3(0, 0, 0), rotationDelay).SetEase(Ease.Linear);
                fishingHookTransform.DORotate(new Vector3(0, 0, 0), rotationDelay).SetEase(Ease.Linear);
                hookTransform.position = _currentHookPosition;
            }
        }
    }

    public void PutHookInTheWater()
    {
        rigidbody.constraints = RigidbodyConstraints2D.None;
        EventManager.OnGameStartedInvoke();
        hookTransform.position = startHookPosition;
        hookTransform.DOScale(startHookScale, 1f).From(0).SetEase(Ease.Linear).OnComplete(delegate
        {
            _moveDirection = new Vector2(0, -1);
            _trueHookSpeed = withoutCameraHookSpeed;
            _canMoveDown = true;
            hookCollider.enabled = true;
            cameraFollow.SetHookIsTarget(hookTransform);
        });
    }


    private void FixedUpdate()
    {
        if (_canMoveDown)
        {
            rigidbody.velocity = _moveDirection * _trueHookSpeed;
            if (hookTransform.position.y <= cameraFollowLength)
            {
                EnableMovementAndCameraFollow();
            }

            if (hookTransform.position.y <= (hookMaxLength + cameraFollowLength))
            {
                if (!_hookGoingUp)
                {
                    TakeHookUp();
                }
            }

            if (_hookGoingUp)
            {
                if (hookTransform.position.y >= startHookPosition.y)
                {
                    CancelFishing();
                }
            }
        }
    }

    private void EnableMovementAndCameraFollow()
    {
        _canMove = true;
    }

    public void IncreaseFishesToTakeHookUp(int value)
    {
        _fishesToTakeHookUp = value;
    }

    public void DecreaseFishedToTakeHookUp()
    {
        _fishesToTakeHookUp = _trueFishesToTakeHookUp;
    }

    private int _fishesToTakeHookUp = 0;
    private int _trueFishesToTakeHookUp;

    public void CheckForFirstFishEntry()
    {
        if (!_hookReachMaxLength)
        {
            if (hookedFish.Count > _fishesToTakeHookUp)
            {
                _hookReachMaxLength = true;
                TakeHookUp();
            }
        }
        else
        {
            return;
        }
    }

    private void TakeHookUp()
    {
        _hookGoingUp = true;
        DOTween.To(x => _trueHookSpeed = x, _trueHookSpeed, 0, 0.1f).SetEase(Ease.Linear).OnComplete(delegate
        {
            _moveDirection = new Vector2(0, 1);
            _trueHookSpeed = hookSpeed;
        });
    }

    private void CancelFishing()
    {
        _canMoveDown = false;
        _canMove = false;
        hookCollider.enabled = false;
        _hookGoingUp = false;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        PutHookOutOfWater();
    }

    public void HookCountIsOver()
    {
        hookCollider.enabled = false;
        TakeHookUp();
    }

    private void PutHookOutOfWater()
    {
        hookTransform.DOScale(0, 1f).From(startHookScale).SetEase(Ease.Linear);
        hookTransform.DOMove(startHookPosition, 1f).OnComplete(delegate
        {
            hookCollider.enabled = false;
            _fisherAnimator.EndFishingAnimation();
            cameraFollow.SetTargetToNull();
            cameraFollow.ChangeCameraPosition(startHookPosition);
            _hookReachMaxLength = false;
            ShowRewardedAdMenu();
            ClearFishDepathValue();
            EventManager.OnGameEndedInvoke();
        });
    }

    public void FishDepthValueCounter(Fish fish)
    {
        _fishDepthValue += Mathf.Abs(fish.fishType.SpawnDepth);
    }

    private void ClearFishDepathValue()
    {
        _fishDepthValue = 0;
    }
    
    private void ShowRewardedAdMenu()
    {
        var sum = Mathf.Abs(hookMaxLength) *
                  PlayerPrefs.GetInt("HookStrengthLevel" + SceneManager.GetActiveScene().name);
        if (sum >= _fishDepthValue)
        {
            if (sum != 0)
            {
                rewardedAdMenu.OpenMenu();
            }
        }
    }
}