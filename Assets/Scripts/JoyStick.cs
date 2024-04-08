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

    void Accelerate()
    {
        float triggerInput = Input.GetAxis("Triggers"); // Assumes RT is positive, LT is negative.

        if (triggerInput > 0)
        {
            Vector3 forceToAdd = transform.forward * speed * 10 * triggerInput;
            rb.AddForce(forceToAdd);
        }
        else if (triggerInput < 0)
        {
            Vector3 forceToAdd = -transform.forward * speed * 10 * -triggerInput;
            rb.AddForce(forceToAdd);
        }

        // Keeping the vertical velocity consistent while altering horizontal movement.
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        localVelocity = new Vector3(0, localVelocity.y, localVelocity.z);
        rb.velocity = transform.TransformDirection(localVelocity);
    }

    void Turn()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
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
