using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElevatorController2 : MonoBehaviour
{
    public Transform[] floors;
    public float moveSpeed = 5.0f;
    public TMP_InputField floorInputField;
    public int currentFloor = 0;

    public Transform playerObjectTransform;

    private bool isMoving = false;
    //private bool isPlayerIn = false;

    private void Start()
    {
        floorInputField.text = (currentFloor + 1).ToString();
        floorInputField.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 targetPosition = floors[currentFloor].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            //playerObjectTransform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }

        MoveToFloor();
    }

    public void MoveToFloor()
    {
        int targetFloor;
        if (int.TryParse(floorInputField.text, out targetFloor))
        {
            if (targetFloor >= 1 && targetFloor <= floors.Length)
            {
                currentFloor = targetFloor - 1;
                isMoving = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        floorInputField.gameObject.SetActive(true);
        //isPlayerIn = true;
        other.transform.SetParent(transform);
        //сделать дочерним для лифта
    }
    private void OnTriggerExit(Collider other)
    {
        floorInputField.gameObject.SetActive(false);
        //isPlayerIn = false;
        other.transform.SetParent(null);
        //убрать из дочернего от лифта
    }
}
