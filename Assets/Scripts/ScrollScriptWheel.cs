using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScriptWheel : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 1.0f;
    public Camera playerCamera;

    public bool isGazing = false;

    void Update()
    {

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider.CompareTag("ScrollView"))
            {
                if (!isGazing)
                {
                    isGazing = true;
                }

                float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

                if (scrollDelta != 0f)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.GetComponent<RectTransform>());

                    scrollRect.verticalNormalizedPosition += scrollDelta * scrollSpeed * Time.deltaTime;

                    LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.GetComponent<RectTransform>());

                    //scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
                }
            }
        }
        else
        {
            isGazing = false;
        }
    }
}
