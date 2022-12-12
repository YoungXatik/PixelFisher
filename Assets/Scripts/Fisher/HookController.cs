using System;
using DG.Tweening;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] private Transform hookTransform;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private BoxCollider2D hookCollider;
    [SerializeField] private Vector3 startHookPosition;
    [SerializeField] private float hookMaxLength;

    [SerializeField] private float rotationMultiplier = 1.2f;
    [SerializeField] private float maxAngle = 30;
    [SerializeField] private float maximalXPosition, minimalXPosition;

    private float _delay = 0.1f;
    private float _delayCounter;

    private Camera _mainCamera;
    private FisherAnimator _fisherAnimator;
    private float _timeToMoveHook;
    private int _hookStrength;
    private int _fishCount;

    private bool _canMove;

    private void Awake()
    {
        _canMove = true;
        _mainCamera = Camera.main;
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
            hookTransform.position = position;

            Vector3 rotate = hookTransform.eulerAngles;
            rotate.z = (rotationMultiplier * hookTransform.position.x);
            hookTransform.rotation = Quaternion.Euler(rotate);
        }
    }

    public void StartFishing()
    {
        _timeToMoveHook = hookMaxLength / 10;
        Debug.Log(_timeToMoveHook);
        hookTransform.DOMoveY((-hookMaxLength), _timeToMoveHook, false)
            .OnComplete(delegate
            {
                hookCollider.enabled = true;
                TakeHookBack();
            });
    }

    private void TakeHookBack()
    {
        _timeToMoveHook = hookMaxLength / 5;
        hookTransform.DOMoveY((startHookPosition.y), _timeToMoveHook, false)
            .OnComplete(_fisherAnimator.EndFishingAnimation);
    }
}