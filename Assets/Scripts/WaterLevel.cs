using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    public GameObject waterbody;

    private void OnTriggerEnter(Collider collision)
    {
        Destroy(this.gameObject); 
        if (collision.tag == "Player") {
            Vector3 position = waterbody.transform.position;
            position.y += 0.1f;
            waterbody.transform.position = position;
        }

    }
}
