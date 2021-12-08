using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tilt : MonoBehaviour
{
    public Vector3 currentRot;
    // Start is called before the first frame update
   
    public float MaxTiltAngle = 20.0f;
    public float tiltSpeed = 30.0f; // tilting speed in degrees/second
    Vector3 curRot;
    float maxX;
    float maxZ;
    float minX;
    float minZ;

    void Start()
    {
        // Get initial rotation
        curRot = this.transform.eulerAngles;
        // calculate limit angles:
        maxX = curRot.x + MaxTiltAngle;
        maxZ = curRot.z + MaxTiltAngle;
        minX = curRot.x - MaxTiltAngle;
        minZ = curRot.z - MaxTiltAngle;
    }

    void Update()
    {
        // "rotate" the angles mathematically:
        curRot.x += Input.GetAxis("Vertical") * Time.deltaTime * tiltSpeed;
        curRot.z += Input.GetAxis("Horizontal") *-1 * Time.deltaTime * tiltSpeed;
        // Restrict rotation along x and z axes to the limit angles:
        curRot.x = Mathf.Clamp(curRot.x, minX, maxX);
        curRot.z = Mathf.Clamp(curRot.z, minZ, maxZ);

        // Set the object rotation
        this.transform.eulerAngles = curRot;
    }
    // Update is called once per frame

}
