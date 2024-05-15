using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(this.gameObject); 
        } else if (collision.tag == "Bot") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            Destroy(this.gameObject);
        }
    }
}
