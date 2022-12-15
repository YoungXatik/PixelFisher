using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fish : MonoBehaviour
{
    [field: SerializeField] public FishType fishType { get; private set; }
    [SerializeField] private float minSpeed = 0.5f, maxSpeed = 1.8f;
    [SerializeField] private float timeToChangeScale = 0.2f;
    
    private enum MovementType
    {
        Basic, Bouncing
    }

    [SerializeField] private MovementType movementType;
    
    private Rigidbody2D _rigidbody;
    private Transform _fishTransform;
    private BoxCollider2D _boxCollider2D;
    private int _fishCost;
    private bool _canMove;
    private float _fishSpeed;
    private Vector2 _moveDirection;

    private void Awake()
    {
        _fishTransform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _fishSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Start()
    {
        var Seq = DOTween.Sequence();
        switch (movementType)
        {
            case MovementType.Bouncing:
                Seq.Append(DOTween.To(x => _moveDirection.y = x, 1, -1, 1f).SetEase(Ease.Linear));
                Seq.Append(DOTween.To(x => _moveDirection.y = x, -1, 1, 1f).SetEase(Ease.Linear));
                Seq.SetLoops(-1);
                break;
            case MovementType.Basic:
                return;
        }
    }

    public void SetFishValues(Vector2 moveDirection, Vector2 moveScale)
    {
        _moveDirection = moveDirection;
        _fishTransform.localScale = new Vector3(moveScale.x,1,1);
        _canMove = true;
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            _rigidbody.velocity = _moveDirection * _fishSpeed;
        }
    }

    public void ChangeMoveDirection()
    {
        if (_moveDirection.x >= 1)
        {
            _moveDirection.x = -1;
            _fishTransform.DOScale(new Vector3(-1, 1, 0), timeToChangeScale);
        }
        else
        {
            _moveDirection.x = 1;
            _fishTransform.DOScale(new Vector3(1, 1, 0), timeToChangeScale);
        }
    }

    public void FishHasBeenHooked(Transform hookTransform)
    {
        FishesSpawner.Instance.spawnedFish.Remove(this);
        _canMove = false;
        _boxCollider2D.enabled = false;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        _fishTransform.parent = hookTransform;
        _fishTransform.localPosition = Vector2.zero.normalized;
        _fishTransform.rotation = Quaternion.Euler(new Vector3(0f,0f,90f));
        
    }
}
