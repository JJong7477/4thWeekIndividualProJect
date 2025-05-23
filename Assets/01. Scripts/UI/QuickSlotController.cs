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
    private int _selectedItemIndex = 0;
    private float _scroll = 0f;
    private ItemSlot[] _itemSlots = new ItemSlot[5];
    private ItemData _selectedItem;
    private PlayerCondition _condition;
    

    private void Start()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i] = GameManager.FindChild(this.transform, $"ItemSlot{i}").GetComponent<ItemSlot>();
        }
        
        GameManager.Player.AddItem += AddItem;
        _condition = GameManager.Player.Condition;
    }

    private void Update()
    {
        _selectedItem = _itemSlots[currentIndex].item;
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        Vector3 scroll = context.ReadValue<Vector2>();
        beforeIndex = currentIndex;
        _scroll += scroll.y;
        
        //Debug.Log(scroll.y);
        
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
        
        _scroll = Mathf.Clamp(_scroll, -1800, 1);

        _itemSlots[beforeIndex].selectedObject.SetActive(false);
        _itemSlots[currentIndex].selectedObject.SetActive(true);
    }
    
    private void AddItem()
    {
        ItemData data = GameManager.Player.itemData;

        if (data.canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.quantity++;
                UIUpdate();
                GameManager.Player.itemData = null;
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UIUpdate();
            GameManager.Player.itemData = null;
            return;
        }

        GameManager.Player.itemData = null;
    }
    
    private ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].item == data && _itemSlots[i].quantity < data.maxStackAmount)
            {
                return _itemSlots[i];
            }
        }

        return null;
    }
    
    private void UIUpdate()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].item != null)
            {
                _itemSlots[i].Set();
            }
            else _itemSlots[i].Clear();
        }
    }
    
    private ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].item == null)
            {
                return _itemSlots[i];
            }
        }

        return null;
    }
    
    public void OnUseButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_selectedItem.type == ItemType.Eatable)
            {
                for (int i = 0; i < _selectedItem.eatables.Length; i++)
                {
                    switch (_selectedItem.eatables[i].type)
                    {
                        case EatableType.Health:
                            _condition.Heal(_selectedItem.eatables[i].value);
                            break;
                    }
                }

                RemoveSelectedItem();
            }
        }
    }
    
    private void RemoveSelectedItem()
    {
        _itemSlots[currentIndex].quantity--;

        if (_itemSlots[currentIndex].quantity <= 0)
        {
            _selectedItem = null;
            _itemSlots[currentIndex].item = null;
            _selectedItemIndex = -1;
        }
        
        UIUpdate();
    }
}