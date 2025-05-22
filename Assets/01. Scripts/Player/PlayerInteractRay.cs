using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractRay : MonoBehaviour
{
    private float _checkRate = 0.05f;
    private float _lastCheckTime;
    private float _maxCheckDistance = 5f;
    public LayerMask layerMask;
    
    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public GameObject toolTipUI;
    public TextMeshProUGUI toolTipText;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        toolTipUI = GameObject.Find("ToolTipBG");
        toolTipText = toolTipUI.GetComponentInChildren<TextMeshProUGUI>();
        toolTipUI.SetActive(false);
    }

    private void Update()
    {
        if (Time.time - _lastCheckTime > _checkRate)
        {
            _lastCheckTime = Time.time;
            
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                toolTipUI.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        toolTipUI.SetActive(true);
        toolTipText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            toolTipUI.SetActive(false);
        }
    }
}
