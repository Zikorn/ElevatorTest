using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElevatorController : MonoBehaviour
{
    [System.Serializable]
    public class FloorData
    {
        public int floorNumber;
        public Vector3 floorPosition;
        public Vector3 callElevatorButtonPosition;
        public Vector3 callElevatorButtonRotation;
    }

    [Header("Elevator settings")]
    public List<FloorData> floors;
    public float moveSpeed = 5.0f;
    private float accelerationMoveSpeed = 5.0f;
    public float accelerationSpeed = 0.5f;
    public int currentFloorIndex = 0;

    public bool teleportMode = false;

    private bool isMoving = false; 
    private Vector3 targetPosition;

    [Header("Game Objects")]
    public GameObject floorButtonPrefab;
    public GameObject callElevatorButtonPrefab;
    private ScrollRect floorScrollRect; 

    private bool isPlayerIn = false;

    private CameraShake playerCameraShake;

    private ElevatorDoors elevatorDoorsObject;

    private PlayerRayCastInteraction playerRayCast;


    void Start()
    {
        moveSpeed = accelerationMoveSpeed;
        //elevatorAudioSource.clip = elevatorMoveSound;

        playerCameraShake = GameObject.Find("Player").GetComponent<CameraShake>();

        playerRayCast = GameObject.Find("Player").GetComponent<PlayerRayCastInteraction>();

        elevatorDoorsObject = GetComponentInChildren<ElevatorDoors>();

        floorScrollRect = GetComponentInChildren<ScrollRect>();

        transform.position = floors[currentFloorIndex].floorPosition;

        float buttonHeight = floorButtonPrefab.GetComponent<RectTransform>().rect.height;
        Vector3 buttonPosition = Vector3.zero;

        foreach (var floorData in floors)
        {
            GameObject floorButton = Instantiate(floorButtonPrefab, floorScrollRect.content);
            floorButton.GetComponentInChildren<TextMeshProUGUI>().text = "Floor " + floorData.floorNumber;

            floorButton.GetComponent<RectTransform>().localPosition = buttonPosition;

            buttonPosition.y -= buttonHeight;

            int targetFloorIndex = floors.IndexOf(floorData);
            floorButton.GetComponent<Button>().onClick.AddListener(() => OnFloorButtonClicked(targetFloorIndex));
        }

        foreach (var floorData in floors)
        {
            GameObject callElevatorButton = Instantiate(callElevatorButtonPrefab);
            callElevatorButton.transform.position = floorData.callElevatorButtonPosition;
            callElevatorButton.transform.rotation = Quaternion.Euler(floorData.callElevatorButtonRotation);
            callElevatorButton.GetComponentInChildren<TextMeshProUGUI>().text = "Call Elevator";
            int targetFloorIndex = floors.IndexOf(floorData);
            callElevatorButton.GetComponentInChildren<Button>().onClick.AddListener(() => OnCallElevatorButtonClicked(targetFloorIndex));
        }

    }

    void FixedUpdate()
    {
        Button currentElevButton;
        if (currentElevButton = null)
        {
            currentElevButton = playerRayCast.currentButton;
        }
        if (isMoving)
        {
            if (teleportMode)
            {
                transform.position = targetPosition;
            }
            else
            {
                moveSpeed += accelerationSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }

            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
            if (isPlayerIn)
            {
                playerCameraShake.isCameraShake = true;
            }
            else
            {
                playerCameraShake.isCameraShake = false;
            }
            
        }
        else
        {
            currentElevButton = null;
            moveSpeed = accelerationMoveSpeed;
            playerCameraShake.isCameraShake = false;
            elevatorDoorsObject.OpenDoors();

        }
    }

    public void OnFloorButtonClicked(int targetFloorIndex)
    {
        if (targetFloorIndex >= 0 && targetFloorIndex < floors.Count)
        {
            targetPosition = floors[targetFloorIndex].floorPosition;
            isMoving = true;
            currentFloorIndex = targetFloorIndex;
            elevatorDoorsObject.CloseDoors();
            
        }
    }

    public void OnCallElevatorButtonClicked(int targetFloorIndex)
    {
        OnFloorButtonClicked(targetFloorIndex);
        elevatorDoorsObject.CloseDoors();
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerIn = true;
        other.transform.SetParent(transform);
    }
    private void OnTriggerExit(Collider other)
    {
        isPlayerIn = false;
        other.transform.SetParent(null);
    }
}

