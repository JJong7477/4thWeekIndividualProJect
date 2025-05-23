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
    
    private Camera _camera;
    public GameObject toolTipUI;
    public GameObject curInteractGameObject;
    public TextMeshProUGUI toolTipText;

    private IInteractable _curInteractable;

    private void Start()
    {
        _camera = Camera.main;
        toolTipUI = GameObject.Find("ToolTipBG");
        toolTipText = toolTipUI.GetComponentInChildren<TextMeshProUGUI>();
        toolTipUI.SetActive(false);
    }

    private void Update()
    {
        if (Time.time - _lastCheckTime > _checkRate)
        {
            _lastCheckTime = Time.time;
            
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    _curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetToolTipText();
                }
            }
            else
            {
                curInteractGameObject = null;
                _curInteractable = null;
                toolTipUI.SetActive(false);
            }
        }
    }

    private void SetToolTipText()
    {
        toolTipUI.SetActive(true);
        toolTipText.text = _curInteractable.GetInteractInfo();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.started && _curInteractable != null)
        {
            _curInteractable.OnInteract();
            curInteractGameObject = null;
            _curInteractable = null;
            toolTipUI.SetActive(false);
        }
    }
}
