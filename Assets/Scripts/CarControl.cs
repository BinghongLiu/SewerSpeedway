using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarControl : MonoBehaviour
{
    public float originalSpeed;
    public float speed;
    public float boostedSpeed;
    public float turnSpeed;
    public float waterSpeed;
    public float gravityMultiplier;
    public int cheeseCounter;
    public float boostDuration;
    public float boostTimer;
    public bool usedBoost = false;
    public bool started = false;
    public bool inWater = false;

    private Rigidbody rb;
    public TextMeshProUGUI cheeseCount;
    public GameObject displayCheese;
    public TextMeshProUGUI timeCount;
    public GameObject displayTime;
    public TextMeshProUGUI elapsedTime;
    private float startTime;

    public AudioSource engineSound;
    public AudioSource revUpSound;
    public AudioSource startUpSound;


    void Start() {
        rb = GetComponent<Rigidbody>();
        cheeseCount.text = "Cheese Counter: " + cheeseCounter;
        StartCoroutine(PlayStartUpSound());
    }

    void Update() {
        if (started) {
            if (boostTimer > 0)
            {
                boostTimer -= Time.deltaTime;
                UpdateBoostTimer();
            } else if (boostTimer == 0 && usedBoost == true) {
                usedBoost = false;
                speed = originalSpeed;
                displayTime.SetActive(false);
                UpdateEngineSound(1.0f);
            } else {
                boostTimer = 0;
            }

            UpdateElapsedTime();
        }
    }

    void FixedUpdate() {
        if (started) {
            Accelerate();
            Turn();
            Fall();
        }
    }

    void Accelerate () {
        if (cheeseCounter > 0 && usedBoost == false) {
            if (Input.GetKey(KeyCode.Space)) {
                usedBoost = true;
                speed = boostedSpeed;
                boostTimer += boostDuration;
                cheeseCounter -= 1;
                displayTime.SetActive(true);
                UpdateCheese();
                UpdateBoostTimer();
                UpdateEngineSound(1.3f);
                //StartCoroutine(PlayRevUpSound());
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 forceToAdd = transform.forward;
            forceToAdd.y = 0;
            if (inWater) {
                rb.AddForce(forceToAdd * speed * 8);
            } else {
                rb.AddForce(forceToAdd * speed * 10);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vector3 forceToAdd = -transform.forward;
            forceToAdd.y = 0;
            if (inWater) {
                rb.AddForce(forceToAdd * speed * 8);
            } else {
                rb.AddForce(forceToAdd * speed * 10);
            }
        }

        Vector3 locVel = transform.InverseTransformDirection(rb.velocity);
        locVel = new Vector3(0, locVel.y, locVel.z);
        rb.velocity = new Vector3(transform.TransformDirection(locVel).x, rb.velocity.y, transform.TransformDirection(locVel).z);
    }

    void Turn () {
        if (rb.velocity.magnitude >= 0.01f) {
            if (Input.GetKey(KeyCode.A))
            {
                if (inWater) {
                    rb.AddTorque(-Vector3.up * turnSpeed * 8);
                } else {
                    rb.AddTorque(-Vector3.up * turnSpeed * 10);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (inWater) {
                    rb.AddTorque(Vector3.up * turnSpeed * 8);
                } else {
                    rb.AddTorque(Vector3.up * turnSpeed * 10);
                }
            }
        }
    }

    void Fall() {
        if (inWater) {
            rb.AddForce(Vector3.down *  gravityMultiplier * 8);
        } else {
            rb.AddForce(Vector3.down * gravityMultiplier * 10);
        }
    }

    public void AddCheese() {
        cheeseCounter += 1;
        UpdateCheese();
    }

    void UpdateCheese() {
        cheeseCount.text = ("Cheese Counter: " + cheeseCounter);
    }

    void UpdateBoostTimer() {
        timeCount.text = ("Remaining Boost Time: " + Mathf.Round(boostTimer));
    }

    void UpdateEngineSound(float speedPitch) {
        engineSound.pitch = speedPitch;
    }

    IEnumerator PlayRevUpSound() {
        revUpSound.Play();
        yield return new WaitForSeconds(1.5f);
        revUpSound.Stop();
    }

    IEnumerator PlayStartUpSound() {
        elapsedTime.text = "3";
        yield return new WaitForSeconds(1.0f);
        elapsedTime.text = "2";
        yield return new WaitForSeconds(1.0f);
        elapsedTime.text = "1";
        yield return new WaitForSeconds(1.0f);
        elapsedTime.text = "Go!";
        yield return new WaitForSeconds(0.5f);
        startTime = Time.time;
        started = true; 
        engineSound.Play();
    }

    void UpdateElapsedTime() {
        float timeElapsed = Time.time - startTime;  // Calculate elapsed time
        elapsedTime.text = "Elapsed Time: " + timeElapsed.ToString("F2");  // Update the TMP GUI text
    }
}