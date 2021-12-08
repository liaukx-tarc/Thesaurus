using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public float openAngle, changingSpeed;
    float closeAngle, targetAngle;
    public bool doorOpening, doorClosing; // The boolen to call door close/open
    public bool isOpen;
    public static bool isLock; //The boolen to lock all the door
    bool isSmaller;

    void Start()
    {
        isLock = false;
        isOpen = false;

        closeAngle = transform.localRotation.eulerAngles.y;
        targetAngle = transform.localRotation.eulerAngles.y - openAngle;

        if (openAngle < transform.localRotation.eulerAngles.y)
            isSmaller = true;
            
        else
            isSmaller = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLock)
        {
            if (doorOpening)
                OpenDoor();
            else if (doorClosing)
                CloseDoor();
        }
    }

    void OpenDoor()
    {
        if (isSmaller)
        {
            if (targetAngle >= 0)
            {
                this.transform.Rotate(new Vector3(0, 1, 0), -changingSpeed);
                targetAngle -= changingSpeed;
            }

            else
            {
                if (transform.localRotation.eulerAngles.y > openAngle)
                    targetAngle = transform.localRotation.eulerAngles.y - 360 - closeAngle;
                else
                    targetAngle = transform.localRotation.eulerAngles.y - closeAngle;
                doorOpening = false;
                isOpen = true;
            }
        }

        else
        {
            if (targetAngle <= 0)
            {
                this.transform.Rotate(new Vector3(0, 1, 0), changingSpeed);
                targetAngle += changingSpeed;
            }

            else
            {
                if (transform.localRotation.eulerAngles.y < openAngle)
                    targetAngle = transform.localRotation.eulerAngles.y + 360 - closeAngle;
                else
                    targetAngle = transform.localRotation.eulerAngles.y - closeAngle;
                doorOpening = false;
                isOpen = true;
            }
        }
    }

    void CloseDoor()
    {
        if (isSmaller)
        {
            if (targetAngle <= 0)
            {
                this.transform.Rotate(new Vector3(0, 1, 0), changingSpeed);
                targetAngle += changingSpeed;
            }

            else
            {
                targetAngle = transform.localRotation.eulerAngles.y - openAngle;
                doorClosing = false;
                isOpen = false;
            }
        }

        else
        {
            if (targetAngle >= 0)
            {
                this.transform.Rotate(new Vector3(0, 1, 0), -changingSpeed);
                targetAngle -= changingSpeed;
            }

            else
            {
                targetAngle = transform.localRotation.eulerAngles.y - openAngle;
                doorClosing = false;
                isOpen = false;
            }
        }
    }
}

