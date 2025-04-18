using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public Transform player;
    public float walkBobSpeed = 6f;
    public float walkBobAmount = 0.015f;
    public float runBobSpeed = 9f;
    public float runBobAmount = 0.03f;

    public KeyCode runKey = KeyCode.LeftShift;

    private float defaultYPos;
    private float timer = 0f;
    private Vector3 lastPlayerPos;

    void Start()
    {
        defaultYPos = transform.localPosition.y;
        lastPlayerPos = player.position;
    }

    void Update()
    {
        Vector3 velocity = (player.position - lastPlayerPos) / Time.deltaTime;
        lastPlayerPos = player.position;

        float horizontalVelocity = new Vector3(velocity.x, 0, velocity.z).magnitude;

        bool isRunning = Input.GetKey(runKey);

        float bobSpeed = isRunning ? runBobSpeed : walkBobSpeed;
        float bobAmount = isRunning ? runBobAmount : walkBobAmount;

        if (horizontalVelocity > 0.1f)
        {
            timer += Time.deltaTime * bobSpeed;
            float newY = defaultYPos + Mathf.Sin(timer) * bobAmount;
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        }
        else
        {
            timer = 0;
            Vector3 currentPos = transform.localPosition;
            currentPos.y = Mathf.Lerp(currentPos.y, defaultYPos, Time.deltaTime * walkBobSpeed);
            transform.localPosition = currentPos;
        }
    }
}