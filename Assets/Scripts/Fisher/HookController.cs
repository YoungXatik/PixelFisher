using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Transform hookTransform;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private BoxCollider2D hookCollider;

    [SerializeField] private Vector3 startHookPosition;
    [SerializeField] private Vector3 inWaterHookPosition;
    
    [SerializeField] private float hookMaxLength;

    [SerializeField] private float rotationMultiplier = 1.2f;
    [SerializeField] private float maxAngle = 30;
    [SerializeField] private float maximalXPosition, minimalXPosition;

    [SerializeField] private float delay;
    [SerializeField] private float rotationDelay;

    [SerializeField] private Vector3 startHookScale;
    
    [SerializeField] private float timeToPutHookDown;
    [SerializeField] private float timeToPutHookUp;
    
    private Vector2 _currentHookPosition;
    private Camera _mainCamera;
    private FisherAnimator _fisherAnimator;
    private int _hookStrength;
    private int _fishCount;

    private bool _canMove;

    private Tween _tween;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        _fisherAnimator = GetComponent<FisherAnimator>();
        startHookScale = hookTransform.localScale;
        hookTransform.localScale = Vector3.zero;
    }

    private void Update()
    {
        XAxisMovement();
    }

    private void XAxisMovement()
    {
        if (_canMove && Input.GetMouseButton(0))
        {
            Vector3 vector = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = hookTransform.position;
            position.x = Mathf.Clamp(vector.x, minimalXPosition, maximalXPosition);
            _tween = hookTransform.DOMoveX(position.x, delay).SetEase(Ease.Linear);
            _currentHookPosition = hookTransform.position;
            
            Vector3 rotate = hookTransform.eulerAngles;
            //rotate.z = (rotationMultiplier * hookTransform.position.x);
            rotate.z = Mathf.Clamp((rotationMultiplier * vector.x), -maxAngle, maxAngle);
            hookTransform.rotation = Quaternion.Euler(rotate);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_tween != null)
            {
                _tween.Kill();
                hookTransform.DORotate(new Vector3(0, 0, 0), rotationDelay).SetEase(Ease.Linear);
                hookTransform.position = _currentHookPosition;
            }
        }
    }

    public void PutHookInTheWater()
    {
        hookTransform.DOScale(startHookScale, 1f).From(0).SetEase(Ease.Linear);
        hookTransform.DOMoveY(inWaterHookPosition.y, 1f).OnComplete(StartFishing);
    }
    
    public void StartFishing()
    {
        _canMove = true;
        cameraFollow.SetHookIsTarget(hookTransform);
        hookTransform.DOMoveY((-hookMaxLength), timeToPutHookDown)
            .OnComplete(delegate
            {
                hookCollider.enabled = true;
                TakeHookBack();
            });
    }

    private void TakeHookBack()
    {
        hookTransform.DOMoveY((startHookPosition.y), timeToPutHookUp)
            .OnComplete(delegate
            {
                _canMove = false;
                PutHookOutOfWater();
            });
    }
    
    private void PutHookOutOfWater()
    {
        hookTransform.DOScale(0, 1f).From(startHookScale).SetEase(Ease.Linear);
        hookTransform.DOMoveY(hookTransform.position.y, 1f).OnComplete(delegate
        {
            _fisherAnimator.EndFishingAnimation();
            cameraFollow.SetTargetToNull();
            cameraFollow.ChangeCameraPosition(startHookPosition);
        });
    }
}