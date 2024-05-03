using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    void Start() {
        
    }


    public void PlayGame() {
        StartCoroutine(DelayedLoad());
    }

    IEnumerator DelayedLoad() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame() {
        Application.Quit();
    }
}
