using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoors : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public float openSpeed = 2.0f;
    public float openDistance = 1.0f;
    public float closeSpeed = 2.0f;

    private Vector3 leftDoorClosedPosition;
    private Vector3 rightDoorClosedPosition;
    private Vector3 leftDoorOpenPosition;
    private Vector3 rightDoorOpenPosition;

    private bool isLeftDoorOpen = false;
    private bool isRightDoorOpen = false;

    void Start()
    {
        leftDoorClosedPosition = leftDoor.localPosition;
        rightDoorClosedPosition = rightDoor.localPosition;
        leftDoorOpenPosition = leftDoorClosedPosition - Vector3.right * openDistance;
        rightDoorOpenPosition = rightDoorClosedPosition + Vector3.right * openDistance;
    }

    public void OpenDoors()
    {
        if (!isLeftDoorOpen && !isRightDoorOpen)
        {
            isLeftDoorOpen = true;
            isRightDoorOpen = true;
        }
    }

    public void CloseDoors()
    {
        if (isLeftDoorOpen && isRightDoorOpen)
        {
            isLeftDoorOpen = false;
            isRightDoorOpen = false;
        }
    }

    void Update()
    {
        if (isLeftDoorOpen)
        {
            leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftDoorOpenPosition, openSpeed * Time.deltaTime);
        }
        else
        {
            leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftDoorClosedPosition, closeSpeed * Time.deltaTime);
        }

        if (isRightDoorOpen)
        {
            rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightDoorOpenPosition, openSpeed * Time.deltaTime);
        }
        else
        {
            rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightDoorClosedPosition, closeSpeed * Time.deltaTime);
        }
    }
}
