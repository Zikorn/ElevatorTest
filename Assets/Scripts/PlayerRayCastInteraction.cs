using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRayCastInteraction : MonoBehaviour
{
    public Camera playerCamera;

    public Color highlightedColorStart;
    public Color highlightedColorPush;
    public Color highlightedColorEnd;

    public Button currentButton;

    private void Start()
    {
        
    }

    void Update()
    {

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            Button button = hit.collider.GetComponent<Button>();
            if (hit.collider.CompareTag("ElevatorButton"))
            {
                button = hit.collider.GetComponent<Button>();
                currentButton = button;
                ChangeButtonColor(currentButton, highlightedColorStart);
                if (Input.GetMouseButtonDown(0))
                {      
                    if (button != null)
                    {
                        button.onClick.Invoke();
                        ChangeButtonColor(currentButton, highlightedColorPush);
                    } 
                }
            }
            else
            {
                if (currentButton != null)
                {
                    ChangeButtonColor(currentButton, highlightedColorEnd);
                    currentButton = null;
                }
            }

            if (hit.collider.CompareTag("CallElevatorButton"))
            {
                button = hit.collider.GetComponent<Button>();
                currentButton = button;
                ChangeButtonColor(currentButton, highlightedColorStart);
                if (Input.GetMouseButtonDown(0))
                {
                    //Button buttonFloor = hit.collider.GetComponent<Button>();
                    if (button != null)
                    {
                        button.onClick.Invoke();
                        ChangeButtonColor(currentButton, highlightedColorPush);
                    }
                }
            }
            else
            {
                if (currentButton != null)
                {
                    ChangeButtonColor(currentButton, highlightedColorEnd);
                    currentButton = null;
                }
            }
        }

    }

    public void ChangeButtonColor(Button button, Color color)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = color;
        button.colors = colors;
    }
}
