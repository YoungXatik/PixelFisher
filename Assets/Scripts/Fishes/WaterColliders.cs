using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterColliders : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Fish fish;
        if (other.gameObject.TryGetComponent<Fish>(out fish))
        {
            fish.ChangeMoveDirection();
        }
    }
}
