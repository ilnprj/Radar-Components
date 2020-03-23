using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform target;
    public Transform player;

    public Transform transformExample;

   
    private void Update()
    {
                transformExample.LookAt(target);
                transform.localRotation = Quaternion.Euler(0, 0, -transformExample.eulerAngles.y);
    }
}