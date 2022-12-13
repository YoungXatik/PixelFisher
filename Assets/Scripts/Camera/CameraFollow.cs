using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followOffset;

    public Transform target;
    private Transform _cameraTransform;
    [SerializeField] private float delay;

    private void Start()
    {
        _cameraTransform = GetComponent<Transform>();
    }

    public void SetHookIsTarget(Transform target)
    {
        this.target = target;
    }

    public void SetTargetToNull()
    {
        target = null;
    }

    public void ChangeCameraPosition(Vector3 position)
    {
        transform.DOMoveY(position.y, 0.5f).SetEase(Ease.Linear);
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            float yPos = target.position.y + followOffset;
            //_cameraTransform.position = new Vector3(0, yPos + followOffset, 0);
            _cameraTransform.DOMoveY(yPos + followOffset, delay);
        }
        else
        {
            return;
        }
    }
}
