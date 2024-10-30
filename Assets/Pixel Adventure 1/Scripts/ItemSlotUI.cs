using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Image icon;  // 图标显示组件
    public Button button;    // 按钮组件
    public TextMeshProUGUI quantityText;    // 数量文本显示组件
    private ItemSlot currentSlot;    // 当前物品槽对象

    public int index;    // 物品槽的索引

    public bool equipped;    // 是否装备标志

    public GameObject checkIcon;    // 勾选图标对象

    // 在物品槽启用时显示装备标志
    public void OnEneable()
    {
        checkIcon.gameObject.SetActive(equipped);
    }

    // 设置物品槽的显示信息
    public void Set(ItemSlot slot)
    {
        // 设置当前物品槽
        currentSlot = slot;
        // 激活图标并设置图标图片
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;

        // 如果数量大于1则显示数量，否则为空
        quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;

        // 如果存在勾选图标，根据是否装备设置其显示状态
        if (checkIcon != null)
        {
            checkIcon.SetActive(equipped);
        }
    }

    // 清除物品槽显示内容
    public void Clear()
    {
        // 清空当前物品槽
        currentSlot = null;
        // 隐藏图标
        icon.gameObject.SetActive(false);
        // 清空数量文本
        quantityText.text = string.Empty;
    }

    // 按钮点击事件，调用物品收集器选择物品
    public void OnclickButton()
    {
        item_collector.instance.SelectItem(index);
    }
}
