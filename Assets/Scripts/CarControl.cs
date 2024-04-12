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
    public float gravityMultiplier;
    public int cheeseCounter;
    public float boostDuration;
    public float boostTimer;
    public bool usedBoost = false;
    public bool drivable = false;
    public float countdownTimer;
    public float elapsedTimer;

    private Rigidbody rb;
    public TextMeshProUGUI cheeseCount;
    public GameObject displayCheese;
    public TextMeshProUGUI timeCount;
    public GameObject displayTime;
    public GameObject elapsedTimeDisplay;
    public TextMeshProUGUI elapsedTime;
    public GameObject countdownDisplay;
    public TextMeshProUGUI countDown;

    void Start() {
        rb = GetComponent<Rigidbody>();
        cheeseCount.text = ("Cheese Counter: " + cheeseCounter);
        countDown.text = ("Counting Down: " + Mathf.RoundToInt(countdownTimer));
    }

    void Update() {

        if (countdownTimer > 0) {
            UpdateCountDownTimer();
            countdownTimer -= Time.deltaTime;
        } else {
            drivable = true;
            elapsedTimeDisplay.SetActive(true);
            countdownDisplay.SetActive(false);
            elapsedTimer += Time.deltaTime;
            UpdateElapsedTime();
        }

        if (boostTimer > 0)
        {
            boostTimer -= Time.deltaTime;
            UpdateBoostTimer();
        } else if (boostTimer == 0 && usedBoost == true) {
            usedBoost = false;
            speed = originalSpeed;
            displayTime.SetActive(false);
        } else {
            boostTimer = 0;
        }
    }

    void FixedUpdate() {
        if (drivable) {
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
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 forceToAdd = transform.forward;
            forceToAdd.y = 0;
            rb.AddForce(forceToAdd * speed * 10);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vector3 forceToAdd = -transform.forward;
            forceToAdd.y = 0;
            rb.AddForce(forceToAdd * speed * 10);
        }

        Vector3 locVel = transform.InverseTransformDirection(rb.velocity);
        locVel = new Vector3(0, locVel.y, locVel.z);
        rb.velocity = new Vector3(transform.TransformDirection(locVel).x, rb.velocity.y, transform.TransformDirection(locVel).z);
    }

    void Turn () {
        if (rb.velocity.magnitude >= 0.01f) {
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddTorque(-Vector3.up * turnSpeed * 10);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rb.AddTorque(Vector3.up * turnSpeed * 10);
            }
        }
    }

    void Fall() {
        rb.AddForce(Vector3.down * gravityMultiplier * 10);
    }

    public void AddCheese() {
        cheeseCounter += 1;
        UpdateCheese();
    }

    void UpdateCheese() {
        cheeseCount.text = ("Cheese Counter: " + cheeseCounter);
    }

    void UpdateBoostTimer() {
        timeCount.text = ("Remaining Boost Time: " + Mathf.RoundToInt(boostTimer));
    }

    void UpdateElapsedTime() {
        elapsedTime.text = ("Elapsed Time: " + Mathf.RoundToInt(elapsedTimer) + " seconds");
    }

    void UpdateCountDownTimer() {
        countDown.text = ("Counting Down: " + Mathf.RoundToInt(countdownTimer));
    }
}