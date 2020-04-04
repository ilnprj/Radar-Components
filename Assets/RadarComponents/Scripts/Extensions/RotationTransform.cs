// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj


using UnityEngine;

/// <summary>
/// Example rotation
/// </summary>
public class RotationTransform : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationVector = default;

    [SerializeField]
    private float speed = default;

    private void Update()
    {
        transform.Rotate(rotationVector * speed);    
    }
}
