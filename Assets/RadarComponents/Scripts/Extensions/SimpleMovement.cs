// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField]
    private int speed = 10;

    public void FixedUpdate()
    {
        Vector3 Movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.Translate(Movement * speed * Time.fixedDeltaTime, Space.Self);
    }
}
