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
    public GameObject infoPanel;
    public Button closeButton;


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

        infoPanel = panel.gameObject;
        closeButton = panel.Find("CloseButton").GetComponent<Button>();

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseInfoPanel);
            renderer.material.color = Color.white;

        }

        // Initially hide the info panel
        infoPanel.SetActive(false);

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
                    OpenInfoPanel();
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
            infoText.text = "Floor: 1\n\nRoom: Lecture room\n\nTemperature: 22°C\n\nISO quality: High";
        }
        else if (gameObject.name.Contains("Sphere"))
        {
            infoText.text = "Floor: 2\n\nRoom: Meeting room\n\nTemperature: 20°C\n\nISO quality: Medium";
        }
        else
        {
            infoText.text = "Floor: Unknown\n\nRoom: Unknown\n\nTemperature: Unknown\n\nISO quality: Unknown";
        }
    }
    private void OpenInfoPanel()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(true);
        }
    }

    private void CloseInfoPanel()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }
}

