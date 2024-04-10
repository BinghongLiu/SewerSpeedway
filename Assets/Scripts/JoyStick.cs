using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour
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

    private Rigidbody rb;
    public TextMeshProUGUI cheeseCount;
    public GameObject displayCheese;
    public TextMeshProUGUI timeCount;
    public GameObject displayTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cheeseCount.text = "Cheese Counter: " + cheeseCounter;
    }

    void Update()
    {
        if (boostTimer > 0)
        {
            boostTimer -= Time.deltaTime;
            UpdateBoostTimer();
        }
        else if (boostTimer <= 0 && usedBoost)
        {
            usedBoost = false;
            speed = originalSpeed;
            displayTime.SetActive(false);
        }

        if (Input.GetButtonDown("Fire1") && cheeseCounter > 0 && !usedBoost)
        {
            usedBoost = true;
            speed = boostedSpeed;
            boostTimer = boostDuration;
            cheeseCounter -= 1;
            displayTime.SetActive(true);
            UpdateCheese();
            UpdateBoostTimer();
        }
    }

    void FixedUpdate()
    {
        Accelerate();
        Turn();
        Fall();
    }

    void Accelerate () {
        if (cheeseCounter > 0 && usedBoost == false) {
            if (Input.GetButtonDown("JoystickButton2")) {
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
        if (rb.velocity.magnitude >= 0.01f)
        {
            float horizontalInput = Input.GetAxis("Horizontal"); // Get the horizontal axis input
            rb.AddTorque(Vector3.up * turnSpeed * 10 * horizontalInput);
        }
    }

    void Fall()
    {
        rb.AddForce(Vector3.down * gravityMultiplier * 10);
    }

    public void AddCheese()
    {
        cheeseCounter += 1;
        UpdateCheese();
    }

    void UpdateCheese()
    {
        cheeseCount.text = "Cheese Counter: " + cheeseCounter;
    }

    void UpdateBoostTimer()
    {
        timeCount.text = "Remaining Boost Time: " + boostTimer.ToString("F2") + "s";
    }
}
