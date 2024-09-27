using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float motorForce = 1500f;       
    public float brakeForce = 3000f;       
    public float turnAngle = 30f;          

    public WheelCollider frontLeftWheel;   
    public WheelCollider frontRightWheel;  
    public WheelCollider rearLeftWheel;    
    public WheelCollider rearRightWheel;   

    private float moveInput;               
    private float turnInput;               

    public Rigidbody rb; 

    public float driftThreshold = 0.3f;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        float steerAngle = turnInput * turnAngle;
        frontLeftWheel.steerAngle = steerAngle;
        frontRightWheel.steerAngle = steerAngle;

        frontLeftWheel.motorTorque = Mathf.Lerp(frontLeftWheel.motorTorque, moveInput * motorForce, Time.deltaTime); ;
        frontRightWheel.motorTorque = Mathf.Lerp(frontRightWheel.motorTorque, moveInput * motorForce, Time.deltaTime); ;

        if (Input.GetKey(KeyCode.Space))
        {
            frontLeftWheel.brakeTorque = brakeForce;
            frontRightWheel.brakeTorque = brakeForce;
            rearLeftWheel.brakeTorque = brakeForce;
            rearRightWheel.brakeTorque = brakeForce;
        }
        else
        {
            frontLeftWheel.brakeTorque = 0;
            frontRightWheel.brakeTorque = 0;
            rearLeftWheel.brakeTorque = 0;
            rearRightWheel.brakeTorque = 0;
        }

        UpdateWheelPosition(frontLeftWheel);
        UpdateWheelPosition(frontRightWheel);
        UpdateWheelPosition(rearLeftWheel);
        UpdateWheelPosition(rearRightWheel);

        WheelHit rearLeftHit;
        WheelHit rearRightHit;

        rearLeftWheel.GetGroundHit(out rearLeftHit);
        rearRightWheel.GetGroundHit(out rearRightHit);

        //Drift
        if (Mathf.Abs(rearLeftHit.sidewaysSlip) > driftThreshold || Mathf.Abs(rearRightHit.sidewaysSlip) > driftThreshold)
        {
            gameManager.isDrifting = true;
        }
        else
        {
            gameManager.isDrifting = false;
        }
    }

    private void UpdateWheelPosition(WheelCollider collider)
    {
        Vector3 pos;
        Quaternion quat;
        collider.GetWorldPose(out pos, out quat);

        if (collider.transform.childCount > 0)
        {
            Transform wheel = collider.transform.GetChild(0);
            wheel.position = pos;
            wheel.rotation = quat * Quaternion.Euler(0, -90, 0);
        }
    }
}
