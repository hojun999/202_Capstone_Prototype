using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkill : MonoBehaviour
{
    Camera MainCamera;

    [Header("LineAttackObject")]
    public GameObject LineAttackObject;

    [Header("ExplosionObject")]
    public GameObject explosionAttackObj;
    public GameObject explosionOutline;

    Vector2 firstMousePosOfLineAttack, secondMousePosOfLineAttack, mousePosOfExplosion;
    public int mouseClickScoreOfLineAttack;
    public int mouseclickScoreOfExplosion;

    public float axis;

    public bool isLineAttackUsing;    // ��ų ������ ������ �ð� ���� true
    public bool isExplosionUsing;
    public bool isExistLineObject;      // ��ų�� ���������
    public bool isExistExplosionObject;


    private void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        // LineAttack
        #region
        if (Input.GetKeyDown(KeyCode.Q) && !isLineAttackUsing)      // �ڷ�ƾ�� �����Ͽ� ��ų ����� ���� ���콺 Ŭ�� �� ���� �Է� ���� ���¸� ����
        {
            StartCoroutine(usingTimeOfLineAttack(2.5f));
        }

        if (Input.GetMouseButtonDown(0) && mouseClickScoreOfLineAttack == 0 && isLineAttackUsing)
        {
            firstMousePosOfLineAttack = Input.mousePosition;
            firstMousePosOfLineAttack = MainCamera.ScreenToWorldPoint(firstMousePosOfLineAttack);
            mouseClickScoreOfLineAttack++;
        }
        else if (Input.GetMouseButtonDown(0) && mouseClickScoreOfLineAttack == 1 && isLineAttackUsing)
        {
            secondMousePosOfLineAttack = Input.mousePosition;
            secondMousePosOfLineAttack = MainCamera.ScreenToWorldPoint(secondMousePosOfLineAttack);
            mouseClickScoreOfLineAttack++;
        }


        if (mouseClickScoreOfLineAttack == 2 && !isExistLineObject)    // ���콺 Ŭ�� �� ��°�� �� ����obj ����
            StartCoroutine(createAndDestroyLineAttackObj());        // ��update������ �Լ� �� ���� �ߵ���Ű�� ���� �� �޼ҵ庸�� bool�� �̿��Ͽ� �ڷ�ƾ���� �ۼ�
        #endregion

        // Explosion
        #region
        if (Input.GetKeyDown(KeyCode.Z) && !isExplosionUsing)
        {
            StartCoroutine(usingTimeOfExplosion(2.5f));
        }

        if (Input.GetMouseButtonDown(0) && mouseclickScoreOfExplosion == 0 && isExplosionUsing)
        {
            mousePosOfExplosion = Input.mousePosition;
            mousePosOfExplosion = MainCamera.ScreenToWorldPoint(mousePosOfExplosion);
            mouseclickScoreOfExplosion++;
        }

        if (mouseclickScoreOfExplosion == 1 && !isExistExplosionObject)
            StartCoroutine(createAndDestroyExplosionObj());
        #endregion
    }

    // LineAttack Coroutine
    #region
    IEnumerator usingTimeOfLineAttack(float endTime)     // ��ų ��� �� ���� ���� ���� �ð�(���� ���� �ð�)
    {
        isLineAttackUsing = true;
        float startTime = 0f;

        while (true)
        {
            startTime += Time.deltaTime;
            if (startTime >= endTime)
                break;
            else if (isExistLineObject)     // ��ų�� ����ߴٸ� ��� ���ð� ������(����ϰ� ��� �ð� �ȿ� ���콺 Ŭ�� �� ���� �� ���� ��, ��ų �ٽ� ����ϴ� �� ����)
                startTime = endTime;
            yield return null;
        }
        mouseClickScoreOfLineAttack = 0;
        isLineAttackUsing = false;
    }

    IEnumerator createAndDestroyLineAttackObj()
    {
        mouseClickScoreOfLineAttack = 0;            // ��ų ������Ʈ�� �ߺ� �������� �ʱ� ����
        isExistLineObject = true;       // ������ ��ų ���� �� ������Ʈ ������ ���� ������

        Vector2 attackDir = secondMousePosOfLineAttack - firstMousePosOfLineAttack;
        Vector3 quaternionToTarget = Quaternion.Euler(0, 0, 90) * attackDir;        // ��ų ������Ʈ�� secondMousePos ���������� �����ϴ� �ڵ�

        var attackObjectCopy = Instantiate(LineAttackObject, firstMousePosOfLineAttack, Quaternion.LookRotation(forward: Vector3.forward, upwards: quaternionToTarget));
        yield return new WaitForSeconds(1.2f);        // WaitForSeconds(a) >> a�ð��� ��ų ������Ʈ ���� �ð����ȸ� �Ҵ�

        Destroy(attackObjectCopy);
        isExistLineObject = false;
    }
    #endregion

    // Explosion Coroutine
    #region
    IEnumerator usingTimeOfExplosion(float endTime)
    {
        isExplosionUsing = true;
        float startTime = 0f;

        while (true)
        {
            startTime += Time.deltaTime;
            if (startTime >= endTime)
                break;
            else if (isExistExplosionObject)       // ��ų ���� ��� ��Ÿ�� ������
                startTime = endTime;
            yield return null;
        }
        mouseclickScoreOfExplosion = 0;
        isExplosionUsing = false;
    }

    IEnumerator createAndDestroyExplosionObj()
    {
        isExistExplosionObject = true;

        var attackObjectCopy = Instantiate(explosionAttackObj, mousePosOfExplosion, Quaternion.LookRotation(forward: Vector3.forward));
        var outlineObjectCopy = Instantiate(explosionOutline, mousePosOfExplosion, Quaternion.LookRotation(forward: Vector3.forward));

        yield return new WaitForSeconds(1.2f);

        Destroy(attackObjectCopy);
        Destroy(outlineObjectCopy);
        isExistExplosionObject = false;
    }
    #endregion




}
