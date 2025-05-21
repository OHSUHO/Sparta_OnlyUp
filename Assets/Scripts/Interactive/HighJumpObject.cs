using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJumpObject : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private float highJumpForce = 30f;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(Vector3.up * highJumpForce, ForceMode.Impulse);
        }
        
    }
}
