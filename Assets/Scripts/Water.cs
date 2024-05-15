using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject cart;
    private void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player")
        {
            CarControl cartScript = cart.GetComponent<CarControl>();
            if (cartScript != null)
            {
                cartScript.inWater = true;
            }
        }
    }

    private void OnTriggerExit(Collider collision) {
        CarControl cartScript = cart.GetComponent<CarControl>();
        if (cartScript != null)
        {
            cartScript.inWater = false;
        }
    }
}
