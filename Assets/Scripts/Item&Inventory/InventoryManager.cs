using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 4;
    public Inventory_Slot[] inventory_slots;
    public GameObject inventoryItemPrefab;
    public GameObject Player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))   // ������ ���
            UseItem(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            UseItem(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            UseItem(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            UseItem(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            UseItem(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            UseItem(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            UseItem(6);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            UseItem(7);
    }

    public bool AddItem(Item item)
    {
        //  ������ ���� �˻�
        for (int i = 0; i < inventory_slots.Length; i++)
        {
            Inventory_Slot slot = inventory_slots[i];
            Inventory_Item itemInSlot = slot.GetComponentInChildren<Inventory_Item>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < maxStackedItems &&
                item.stackable)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // �� ���� ã��
        for (int i = 0; i < inventory_slots.Length; i++)
        {
            Inventory_Slot slot = inventory_slots[i];
            Inventory_Item itemInSlot = slot.GetComponentInChildren<Inventory_Item>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, Inventory_Slot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        Inventory_Item inventory_Item = newItemGo.GetComponent<Inventory_Item>();
        inventory_Item.InitialiseItem(item);
    }

    void UseItem(int index)
    {
        // �Һ� ������ ���� ī��Ʈ ����
        Inventory_Slot slot = inventory_slots[index];
        Inventory_Item itemInSlot = slot.GetComponentInChildren<Inventory_Item>();
        if(itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if(item.consumable == true)
            {
                itemInSlot.count--;
                if(itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }

                // ��� ȿ��
                if (item.actionType == ActionType.Healing)        // ü�� ����
                {
                    Player.GetComponent<PlayerController>().hp += 10;
                }
                else if (item.actionType == ActionType.speedUp)
                {
                    float useTime = 0;
                    float endTime = 10f;
                    bool beingBuffed = true;

                    useTime += Time.deltaTime;
                    if(useTime >= endTime)
                    {
                        useTime = 0;
                        beingBuffed = false;
                    }

                    if (beingBuffed)
                        Player.GetComponent<PlayerController>().moveSpeed = 4.5f;
                    else
                        Player.GetComponent<PlayerController>().moveSpeed = 3f;     // 10�� �� ���� �̵��ӵ���. (movespeed�� �ν�����â���� �����ϹǷ� �� Ȯ��)
                }
            }       
            
        }

        

    }

}