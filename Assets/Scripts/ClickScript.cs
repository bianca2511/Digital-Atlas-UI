using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickScript : MonoBehaviour
{
    private new Renderer renderer;
    private bool isRed = false;
    private bool isHovering = false;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI infoText;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.color = Color.white;

        // Try to find canvas first
        Transform canvas = GameObject.Find("Canvas").transform;
        if (canvas == null)
        {
            Debug.LogError("Canvas not found!");
            return;
        }
        else
        {
            Debug.Log("Canvas found.");
        }

        // Try to find the panel second
        Transform panel = canvas.Find("Panel");
        if (panel == null)
        {
            Debug.LogError("Panel not found!");
            return;
        }
        else
        {
            Debug.Log("Panel found.");
        }

        // finally try to find the text fields

        // Find the object name field
        titleText = panel.Find("Name").GetComponent<TextMeshProUGUI>();
        if (titleText == null)
        {
            Debug.LogError("Title Text component not found at Canvas/Panel/Name!");
        }
        else
        {
            Debug.Log("Title Text component found.");
        }

        // Find the object info field
        infoText = panel.Find("Info").GetComponent<TextMeshProUGUI>();
        if (infoText == null)
        {
            Debug.LogError("Info Text component not found at Canvas/Panel/Info!");
        }
        else
        {
            Debug.Log("Info Text component found.");
        }


    }

    // Update is called once per frame
    void Update()
    {
        onMouseHover();
        onMouseClick();

    }
    private void onMouseHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                if (!isHovering)
                {
                    isHovering = true;
                    renderer.material.color = Color.yellow;
                }
            }
            else
            {
                if (isHovering)
                {
                    isHovering = false;
                    renderer.material.color = isRed ? Color.red : Color.white;
                }
            }
        }
        else
        {
            if (isHovering)
            {
                isHovering = false;
                renderer.material.color = isRed ? Color.red : Color.white;
            }
        }
    }

    private void onMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    ToggleColor();
                    UpdateText();
                }
            }
        }
    }

    private void ToggleColor()
    {
        if (isRed)
        {
            renderer.material.color = isHovering ? Color.yellow : Color.white;
        }
        else
        {
            renderer.material.color = Color.red;
        }
        isRed = !isRed;
    }

    private void UpdateText()
    {
        if (titleText == null || infoText == null)
        {
            Debug.LogError("Text components are not there :(!");
            return;
        }

        // Update the text based on the clicked object
        titleText.text = gameObject.name + " sensor";

        // Customize the info text based on the type of object
        if (gameObject.name.Contains("Cube"))
        {
            infoText.text = "Floor: 1\nRoom: Lecture room\nTemperature: 22°C\nISO quality: High";
        }
        else if (gameObject.name.Contains("Sphere"))
        {
            infoText.text = "Floor: 2\nRoom: Meeting room\nTemperature: 20°C\nISO quality: Medium";
        }
        else
        {
            infoText.text = "Floor: Unknown\nRoom: Unknown\nTemperature: Unknown\nISO quality: Unknown";
        }
    }
}

