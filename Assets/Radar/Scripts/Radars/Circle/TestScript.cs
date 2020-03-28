using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform target;
    public Transform playerTransform;

    private GameObject lookAtObject;

    
    private void Awake()
    {
        lookAtObject = new GameObject();
        lookAtObject.transform.SetParent(playerTransform);
        lookAtObject.transform.localPosition = Vector3.zero;
        lookAtObject.transform.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        //transform.localRotation = Quaternion.Euler(0, 0, -rot.transform.eulerAngles.y);
    }
}