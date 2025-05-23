using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public int quantity;
    public Image icon;
    public ItemData item;
    public GameObject selectedObject;
    public TextMeshProUGUI quantityText;
    
    private void Start()
    {
        selectedObject = GameManager.FindChild(this.transform, "Selected").gameObject;
        quantityText = GameManager.FindChild(this.transform, "QuantityText").GetComponent<TextMeshProUGUI>();
        icon = GameManager.FindChild(this.transform, "Icon").GetComponent<Image>();
        Clear();
    }
    
    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty;
    }
    
    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }
}