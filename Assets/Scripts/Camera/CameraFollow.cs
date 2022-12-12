using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followOffset;

    public Transform target;
    private Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (target != null)
        {
            float yPos = target.position.y + followOffset;
            _cameraTransform.position = new Vector3(0, yPos + followOffset, 0);
        }
        else
        {
            return;
        }
    }
}
