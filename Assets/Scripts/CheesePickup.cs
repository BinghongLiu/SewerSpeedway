using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheesePickup : MonoBehaviour
{
    public GameObject cart;
    public AudioSource pickUpSound;

    private void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player")
        {
            Destroy(this.gameObject); 
            CarControl cartScript = cart.GetComponent<CarControl>();
            if (cartScript != null)
            {
                cartScript.AddCheese();
                pickUpSound.Play();
            }
        }
    }
}
