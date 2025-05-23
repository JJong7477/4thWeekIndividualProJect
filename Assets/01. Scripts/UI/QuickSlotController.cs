using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuickSlotController : MonoBehaviour
{
    public int currentIndex = 0;
    public int beforeIndex;
    public int maxIndex = 5;
    private float _scroll = 0f;
    private ItemSlot[] _itemSlots = new ItemSlot[5];
    

    private void Start()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i] = GameManager.FindChild(this.transform, $"ItemSlot{i}").GetComponent<ItemSlot>();
        }
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        Vector3 scroll = context.ReadValue<Vector2>();
        beforeIndex = currentIndex;
        _scroll += scroll.y;
        Debug.Log(scroll.y);
        
        if (_scroll >= -1800f && _scroll < -1440f)
        {
            currentIndex = 4;
        }
        else if (_scroll >= -1440f && _scroll < -1080f)
        {
            currentIndex = 3;
        }
        else if (_scroll >= -1080f && _scroll < -720f)
        {
            currentIndex = 2;
        }
        else if (_scroll >= -720f && _scroll < -360f)
        {
            currentIndex = 1;
        }
        else if (_scroll >= -360f && _scroll < 0)
        {
            currentIndex = 0;
        }
        

        currentIndex = Mathf.Clamp(currentIndex, 0, maxIndex);
        _scroll = Mathf.Clamp(_scroll, -1800, 1);
        
        _itemSlots[beforeIndex].selectedObject.SetActive(false);
        _itemSlots[currentIndex].selectedObject.SetActive(true);
    }
}