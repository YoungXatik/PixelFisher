using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fish : MonoBehaviour
{
    [SerializeField] private FishType fishType;
    [SerializeField] private float minSpeed = 1, maxSpeed = 3;
    [SerializeField] private float timeToChangeScale = 0.1f;
    
    private Rigidbody2D _rigidbody;
    private Transform _fishTransform;
    private int _fishCost;
    private bool _canMove;
    private float _fishSpeed;
    private Vector2 _moveDirection;

    private void Awake()
    {
        _fishTransform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _fishSpeed = Random.Range(minSpeed, maxSpeed);
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
}
