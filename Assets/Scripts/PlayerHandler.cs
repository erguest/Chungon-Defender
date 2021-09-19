using System;
using UnityEngine;
public class PlayerHandler : MonoBehaviour
{
    [SerializeField] float movementSpeed = 25f;
    [SerializeField] float xRange = 14f;
    [SerializeField] float yRange = 8f;
    [SerializeField] float clampOffset = 8f;

    [SerializeField] GameObject laser;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;

    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = 15f;

    float xThrow, yThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }
    void ProcessRotation()
    {
        // pitch
        float pitchPosition = (transform.localPosition.y - clampOffset) * positionPitchFactor;
        float pitchInput = yThrow * controlPitchFactor;

        float pitch = pitchPosition + pitchInput;
        // yaw
        float yawPosition = transform.localPosition.x * positionYawFactor;

        float yaw = yawPosition;
        // roll
        float rollInput = xThrow * controlRollFactor;
        float roll = rollInput;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void ProcessTranslation()
    {
        // Input 
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        // X Translate Calc
        float xOffset = xThrow * Time.deltaTime * movementSpeed;
        float newXpos = transform.localPosition.x + xOffset;
        //X Clamping
        float clampXPos = Mathf.Clamp(newXpos, -xRange, xRange);


        // Y Translate Calc
        float yOffset = yThrow * Time.deltaTime * movementSpeed;
        float newYpos = transform.localPosition.y + yOffset;
        // Y Clamping/Offset
        float clampTop = clampOffset + yRange;
        float clampBot = clampOffset - yRange;
        float clampYPos = Mathf.Clamp(newYpos, clampBot, clampTop);

        //Actual Translate
        transform.localPosition = new Vector3(clampXPos, clampYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {ActivateLaser(true);}
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {ActivateLaser(false);}
    }
    void ActivateLaser(bool isActive)
    {
        var laserEmission = laser.GetComponent<ParticleSystem>().emission;
        laserEmission.enabled = isActive;
    }

    
}
