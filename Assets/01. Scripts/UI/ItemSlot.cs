using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public GameObject selectedObject;

    private void Start()
    {
        selectedObject = this.transform.Find("Selected").gameObject;
    }
}