using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Resource,
    Eatable,
}

public enum EatableType
{
    Health,
    Stamina,
}

[Serializable]
public class ItemDataEatable
{
    public EatableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "NewItem")]

public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;
    
    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;
    
    [Header("Eatable")]
    public ItemDataEatable[] eatables;
    
    [Header("Equip")]
    public GameObject equipPrefab;
}
