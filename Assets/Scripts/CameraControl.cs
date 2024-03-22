using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform car;
    public float smoothing;
    public float rotSmoothing;
    public GameObject Cam1;

    // Start is called before the first frame update
    void Start()
    {
        smoothing = 30000f;
        rotSmoothing = 1f;
        Cam1.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, car.position, smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, car.rotation, rotSmoothing);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }
}
