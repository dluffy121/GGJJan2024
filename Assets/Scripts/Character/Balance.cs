using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Balance : MonoBehaviour
{
    [SerializeField]
    private float targetRotation;
    [SerializeField]
    private Rigidbody2D rb_character;
    [SerializeField]
    private float force;

    private void Update()
    {
        rb_character.MoveRotation(Mathf.LerpAngle(rb_character.rotation, targetRotation, force * Time.fixedDeltaTime));
    }
}
