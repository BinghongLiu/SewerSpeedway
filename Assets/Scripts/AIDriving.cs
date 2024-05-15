using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDriving : MonoBehaviour
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
    public bool started = false;
    public bool inWater = false;

    private Rigidbody rb;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float closeEnough = 1.0f; // How close the car needs to be to the waypoint to consider it reached

    void Start() {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Countdown());
    }

    void Update() {
        if (started) {
            NavigateTowardsWaypoint();
        }
    }

    void FixedUpdate() {
        if (started) {
            AccelerateTowardsTarget();
            ApplyGravity();
        }
    }

    void NavigateTowardsWaypoint() {
        if (currentWaypointIndex < waypoints.Length) {
            Vector3 targetDirection = waypoints[currentWaypointIndex].position - transform.position;
            float distance = targetDirection.magnitude;
            if (distance < closeEnough) {
                Debug.Log("Waypoint Reached, Moving to Next: " + currentWaypointIndex); // Debug waypoint progression
                currentWaypointIndex++;
            } else {
                float step = turnSpeed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
        }
    }

    void AccelerateTowardsTarget() {
        if (currentWaypointIndex < waypoints.Length) {
            Vector3 moveDirection = (waypoints[currentWaypointIndex].position - transform.position).normalized;
            rb.AddForce(moveDirection * speed * (inWater ? 8 : 10));
        }
    }

    void ApplyGravity() {
        rb.AddForce(Vector3.down * gravityMultiplier * (inWater ? 8 : 10));
    }

    IEnumerator Countdown() {
        yield return new WaitForSeconds(3.0f);
        started = true;
    }
}