using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}

public enum ConsumableType
{
    Health,
    
    // Magic
}

[CreateAssetMenu(fileName =  "Item",menuName = "New Item")]
public class ItemData : ScriptableObject    //保存所有项目信息
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;   //使用对象

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable info")]
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public GameObject equipPrefab;
}

[System.Serializable]

public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}
