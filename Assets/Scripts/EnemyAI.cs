using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Rigidbody rb;

    public Transform playerPos;
    public Transform LeftShootPos;
    public Transform RightShootPos;

    private int stateNum;

    public float moveSpeed;

    private float xMove;
    private float yMove;
    private float movingTime;
    private float movingStandardTime;
    private float attackAngle;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //1~3�ʸ��� ����/������ state ����, state �߿��� �ٸ� �ൿ ȣ�� ���ϸ� �ش� �ൿ�� ������ �ٷ� �ٸ� �ൿ ȣ��
    }

    //public void GetStateNum()
    //{
    //    stateNum = Random.Range(1, 3);      // 1 attack 2 move
    //}

    

    //public void GetState()
    //{
    //    switch (stateNum)
    //    {
    //        case 1:
    //            break;
    //    }
    //}

    public void RandomMove()        // ��Ʈ ������Ʈ�� ������ �� ��� ������ �� �ݴ� �������� �̵��ϰ� ����
    {
        xMove = Random.Range(-1, 1);
        yMove = Random.Range(-1, 1);
        movingTime = Random.Range(0.5f, 1.5f);      // �����̴� �ð�
        movingStandardTime += Time.timeScale;

        while (movingTime >= movingStandardTime)
        {
            Vector2 movePos = new Vector2(xMove, yMove) * moveSpeed;
            rb.velocity = movePos;
        }
    }

    public void Attack()    // ĳ���� x���� +-1 ���� ������ ���� �Ѿ� �߻�
    {
        attackAngle = Random.Range(playerPos.position.x - 1, playerPos.position.x + 1);
    }

}
