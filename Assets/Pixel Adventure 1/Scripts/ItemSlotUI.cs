using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Image icon;  // ͼ����ʾ���
    public Button button;    // ��ť���
    public TextMeshProUGUI quantityText;    // �����ı���ʾ���
    private ItemSlot currentSlot;    // ��ǰ��Ʒ�۶���

    public int index;    // ��Ʒ�۵�����

    public bool equipped;    // �Ƿ�װ����־

    public GameObject checkIcon;    // ��ѡͼ�����

    // ����Ʒ������ʱ��ʾװ����־
    public void OnEneable()
    {
        checkIcon.gameObject.SetActive(equipped);
    }

    // ������Ʒ�۵���ʾ��Ϣ
    public void Set(ItemSlot slot)
    {
        // ���õ�ǰ��Ʒ��
        currentSlot = slot;
        // ����ͼ�겢����ͼ��ͼƬ
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;

        // �����������1����ʾ����������Ϊ��
        quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;

        // ������ڹ�ѡͼ�꣬�����Ƿ�װ����������ʾ״̬
        if (checkIcon != null)
        {
            checkIcon.SetActive(equipped);
        }
    }

    // �����Ʒ����ʾ����
    public void Clear()
    {
        // ��յ�ǰ��Ʒ��
        currentSlot = null;
        // ����ͼ��
        icon.gameObject.SetActive(false);
        // ��������ı�
        quantityText.text = string.Empty;
    }

    // ��ť����¼���������Ʒ�ռ���ѡ����Ʒ
    public void OnclickButton()
    {
        item_collector.instance.SelectItem(index);
    }
}
