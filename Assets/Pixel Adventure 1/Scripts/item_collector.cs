using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class item_collector : MonoBehaviour
{
    // ������
    public GameObject inventoryPanel;
    // ��Ʒ����λ��
    public Transform dropPosition;
    // UI��Ʒ������
    public ItemSlotUI[] uiSlots;
    // ��Ʒ������
    public ItemSlot[] slots;

    [Header("ѡ����Ʒ")]
    private ItemSlot selectedItem;
    // ��ǰѡ�е���Ʒ������
    private int selectedItemIndex;
    // ѡ����Ʒ�����ơ�������������ʾ
    public TextMeshProUGUI selectedItemName, selectedItemDescription, selectedItemstatName, selectedItemsStatValue;
    // ���ְ�ť��ʹ�á�װ����ȡ��װ��������
    public GameObject useButton, equipButton, unequipButton, dropButton;

    // ��ǰװ������Ʒ����
    private int currentequipIndex;

    // ����һ����̬ʵ��������ȫ�ַ���
    public static item_collector instance;

    // ��Awake�����г�ʼ��ʵ��
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // ��Ϸ��ʼʱ�رտ�����
        inventoryPanel.SetActive(false);

        // ��ʼ����Ʒ�ۣ���Ϊû��ֱ�Ӹ��ӽű�����Ʒ��
        slots = new ItemSlot[uiSlots.Length];

        // ��ʼ��ÿ����Ʒ��
        for (int x = 0; x < slots.Length; x++)
        {
            slots[x] = new ItemSlot();  // Ϊÿ����Ʒ�۷����µĶ���
            uiSlots[x].index = x;   // ������Ʒ������
            uiSlots[x].Clear(); // �����Ʒ�ۣ���ʾδ�����κ���Ʒ
        }

        // ���ѡ�е���Ʒ����
        ClearSelectItemWindow();
    }

    void Update()
    {
        // ����'I'����/�رտ��
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory();
        }
    }

    // �����Ʒ�����
    public void AddItem(ItemData item)
    {
        // �����Ʒ���Զѵ�
        if (item.canStack)
        {
            // ��ȡ���Զѵ�����Ʒ��
            ItemSlot slotToStackTo = GetItemStack(item);

            if (slotToStackTo != null)
            {
                // ������Ʒ����������UI
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        // ��ȡ�յ���Ʒ��
        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            // ����пղۣ������Ʒ
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }

        // ���û�пղۣ�������Ʒ
        ThrowItem(item);
    }

    // ������Ʒ
    public void ThrowItem(ItemData item)
    {
        // �ڶ���λ��������Ʒ
        Instantiate(item.dropPrefab, dropPosition.position, dropPosition.rotation);
    }

    // ����UI����ʾ����е���Ʒ
    void UpdateUI()
    {
        // ����������Ʒ��
        for (int x = 0; x < slots.Length; x++)
        {
            // �����������Ʒ
            if (slots[x].item != null)
            {
                // ��ʾ����Ʒ
                uiSlots[x].Set(slots[x]);
            }
            else
            {
                // �����Ʒ��
                uiSlots[x].Clear();
            }
        }
    }

    // ��ȡ���Զѵ�����Ʒ��
    ItemSlot GetItemStack(ItemData item)
    {
        for (int x = 0; x < slots.Length; x++)
        {
            // �����Ʒ�Ƿ���Լ����ѵ�
            if (slots[x].item == item && slots[x].quantity < item.maxStackAmount)
            {
                // ���ؿ��Զѵ�����Ʒ��
                return slots[x];
            }
        }
        // ���û�п��Զѵ�����Ʒ�ۣ��򷵻�null
        return null;
    }

    // ��ȡ�յ���Ʒ��
    ItemSlot GetEmptySlot()
    {
        for (int x = 0; x < slots.Length; x++)
        {
            // ������ǿյ�
            if (slots[x].item == null)
            {
                // ���ؿյ���Ʒ��
                return slots[x];
            }
        }
        // ���û�пղۣ��򷵻�null
        return null;
    }

    // ѡ����Ʒ
    public void SelectItem(int index)
    {
        // ���ѡ��Ĳ�Ϊ�գ�ֱ�ӷ���
        if (slots[index].item == null)
        {
            return;
        }

        // ����ѡ�е���Ʒ������
        selectedItem = slots[index];
        selectedItemIndex = index;

        // ��ʾ��Ʒ�����ƺ�����
        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;

        // ������Ʒ������ʾ��Ӧ�İ�ť
        useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && !uiSlots[index].equipped);
        unequipButton.SetActive(selectedItem.item.type == ItemType.Equipable && uiSlots[index].equipped);
        dropButton.SetActive(true);
    }

    // ���ѡ�е���Ʒ����
    void ClearSelectItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemName.text = string.Empty;
        selectedItemsStatValue.text = string.Empty;
        useButton.SetActive(false);
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    // ʹ����Ʒ��ť����¼�
    public void ONUseButton()
    {

    }

    // װ����Ʒ��ť����¼�
    public void ONEquipButton()
    {

    }

    // ȡ��װ����Ʒ
    void UnEquip(int Index)
    {

    }

    // ȡ��װ����ť����¼�
    public void ONUnequipButton()
    {

    }

    // ������Ʒ��ť����¼�
    public void OnDropButton()
    {
        // ���ѡ�е���Ʒ�Ƿ�Ϊ��
        if (selectedItem != null && selectedItem.item != null)
        {
            // ����ѡ�е���Ʒ
            ThrowItem(selectedItem.item);
            // �Ƴ�ѡ�е���Ʒ
            RemoveSelectedItem();
        }
        else
        {
            Debug.LogWarning("No item selected to drop!");
        }
        //// ����ѡ�е���Ʒ
        //ThrowItem(selectedItem.item);
        //// �Ƴ�ѡ�е���Ʒ
        //RemoveSelectedItem();
    }

    // �Ƴ�ѡ�е���Ʒ
    void RemoveSelectedItem()
    {
        selectedItem.quantity -= 1;

        // �����Ʒ����Ϊ0
        if (selectedItem.quantity == 0)
        {
            // �����Ʒ�Ѿ���װ����ȡ��װ��
            if (uiSlots[selectedItemIndex].equipped == true)
            {
                UnEquip(selectedItemIndex);
            }
            selectedItem.item = null;
            // �����Ʒ����
            ClearSelectItemWindow();
        }

        // ����UI
        UpdateUI();
    }

    // �Ƴ�ָ����Ʒ
    public void RemoveItem(ItemData item)
    {

    }

    // ����Ƿ�ӵ��ָ����������Ʒ
    public bool HasItem(ItemData item, int quantity)
    {
        return false;
    }

    // �򿪿��
    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        // ��ͣ��Ϸʱ��
        Time.timeScale = 0;
        ClearSelectItemWindow();
    }

    // �رտ��
    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        // �ָ���Ϸʱ��
        Time.timeScale = 1;
    }
}

// ������Ʒ����
public class ItemSlot
{
    // ��Ʒ����
    public ItemData item;
    // ��Ʒ����
    public int quantity;
}
