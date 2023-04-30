using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] public float RotationSpeed;

    private void Update()
    {
        transform.Rotate(0, 0, -1 * RotationSpeed);
    }
}
