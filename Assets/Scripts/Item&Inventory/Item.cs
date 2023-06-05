using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{

    [Header("Only gameplay")]
    public ItemType itemType;
    public ActionType actionType;

    [Header("Only UI")]
    public bool stackable;       // ���� ��ø�� ������ ����������.
    public bool consumable;      // �Һ� ����������.

    [Header("Both")]
    public Sprite image;


}
public enum ItemType
{
    Quest,
    posion,
    etc_HpPosion,
    etc_EnergyPosion,
    etc_Stone
}
public enum ActionType
{
    Healing,
    enegyUp,
    required_in_Quest2
}
