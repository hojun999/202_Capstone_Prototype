using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenchmanFightTrigger : MonoBehaviour
{
    //public GameObject Henchman;     // �ڱ� �ڽ��� ������ �������� ����( HenchmanAI�� �������� �ָ� �ִ� �ֵ鵵 �� �������� �ɰ� ����)
    //�θ�component�������� �Լ��� ��ü ��������.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<HenchmanAI_Quest3>().enabled = true;
            GetComponentInParent<HenchmanAI_Quest3>().isStartFight = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

    }
}
