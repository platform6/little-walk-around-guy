using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalRotator : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 50f; // Rotation speed in degrees per second

    [SerializeField]
    private float diagonalDegree = 45f; // Degree of diagonal rotation

    // Update is called once per frame
    void Update()
    {
        // Calculate the rotation amount for both X and Y axes
        float rotationX = Mathf.Sin(Mathf.Deg2Rad * diagonalDegree) * rotationSpeed * Time.deltaTime;
        float rotationY = Mathf.Cos(Mathf.Deg2Rad * diagonalDegree) * rotationSpeed * Time.deltaTime;

        // Apply the rotation
        transform.Rotate(new Vector3(rotationX, rotationY, 0f));
    }
}

