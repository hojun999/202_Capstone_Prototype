using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    public GameObject EnemyBullet;

    public Transform playerPos;
    public Transform LeftShootPos;
    public Transform RightShootPos;

    private Vector3 ShootPos;

    public float moveSpeed;

    private float xMove;
    private float yMove;
    private float attackAngle;
    private float DistOfXPositionPlayerAndEnemy;


    private float ActionTime;
    private float MoveTime;
    private float AttackTime = 1.2f;
    private float rayLength = 1f;

    // ���Ŀ� ����Ʈ�� �ٲٰ� �ο� ������ �����ϸ� true ȣ���ϰ� ����
    private bool isStartFight = true;      // �÷��̾ �ο� ������ �� �����ϸ� true, ����Ʈ 3Ŭ����� false, �÷��̾� ��� �� false, ����Ʈ 4 Ŭ���� �� false

    private bool doAttack;

    private Vector2 movePos;
    private Vector3 attackAngleVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Action();
    }


    private void FixedUpdate()
    {
        rb.velocity = movePos;
        movePos = new Vector2(xMove, yMove) * moveSpeed;
    }

    private void Update()
    {
        // �ִϸ��̼�        // enemyDir > 0 �����ʺ��� enemyDir < 0 ���ʺ���
        if (anim.GetFloat("enemyDir") != DistOfXPositionPlayerAndEnemy)
            anim.SetFloat("enemyDir", DistOfXPositionPlayerAndEnemy);

        if (anim.GetFloat("yMove") != yMove)
            anim.SetFloat("yMove", yMove);

        if (doAttack)
            anim.SetBool("doAttack", true);
        else
            anim.SetBool("doAttack", false);

        DistOfXPositionPlayerAndEnemy = playerPos.position.x - gameObject.GetComponent<Transform>().position.x;

        // �ѱ� ��ġ ����
        if (DistOfXPositionPlayerAndEnemy <= 0)
            ShootPos = LeftShootPos.position;
        else if (DistOfXPositionPlayerAndEnemy > 0)
            ShootPos = RightShootPos.position;




    }

    public void RandomMove()
    {
        xMove = Random.Range(-1f, 1f);
        yMove = Random.Range(-1f, 1f);
    }

    public void Attack()
    {
        doAttack = true;
    }

    public void ShootBullet()    // ĳ���� x���� +-1 ���� ������ ���� �Ѿ� �߻�
    {
        attackAngle = Random.Range(playerPos.position.x - 1f, playerPos.position.x + 1f);
        attackAngleVector = new Vector3(attackAngle, playerPos.position.y, 0);
        Vector3 dir = (attackAngleVector - ShootPos).normalized;
        Instantiate(EnemyBullet, ShootPos, Quaternion.LookRotation(dir));
    }

    void Action()
    {
        doAttack = false;
        MoveTime = Random.Range(0.8f, 1.5f);
        ActionTime = MoveTime + AttackTime + 0.12f;

        RandomMove();

        Invoke("Attack", MoveTime);
        Invoke("Action", ActionTime); 
    }



}
