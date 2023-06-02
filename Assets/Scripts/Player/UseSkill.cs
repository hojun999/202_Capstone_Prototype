using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseSkill : MonoBehaviour
{
    Camera MainCamera;

    public GameObject gameManager;

    public GameObject enterFightArea;

    [Header("LineAttackObject")]
    public GameObject LineAttackObject;

    [Header("ExplosionObject")]
    public GameObject explosionAttackObj;
    public GameObject explosionOutline;

    [Header("CooltimeImage")]
    public Image LineAttackCoolTimeImage;
    public Image explosionCoolTimeImage;

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
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isLineAttackUsing)      // �ڷ�ƾ�� �����Ͽ� ��ų ����� ���� ���콺 Ŭ�� �� ���� �Է� ���� ���¸� ����
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
        {
            StartCoroutine(createAndDestroyLineAttackObj());        // ��update������ �Լ� �� ���� �ߵ���Ű�� ���� �� �޼ҵ庸�� bool�� �̿��Ͽ� �ڷ�ƾ���� �ۼ�
            LineAttackCoolTimeImage.fillAmount = 0;
            StartCoroutine(LineAttackCoolTime(3f));
        }
            #endregion

        // Explosion
        #region
        if (Input.GetMouseButtonDown(2) && !isExplosionUsing)
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
        {
            StartCoroutine(createAndDestroyExplosionObj());
            explosionCoolTimeImage.fillAmount = 0;
            StartCoroutine(ExplosionCoolTime(5f));
        }
        #endregion

        if (gameManager.GetComponent<GameManager>().isEnterFight)
            MainCamera = GameObject.FindGameObjectWithTag("FightCamera").GetComponent<Camera>();
        else
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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

        yield return new WaitForSeconds(2f);        // ��Ÿ�� ����
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

        yield return new WaitForSeconds(4f);        // ��Ÿ�� ����
        isExistExplosionObject = false;
    }
    #endregion


    IEnumerator LineAttackCoolTime(float coolTime)
    {
        while (LineAttackCoolTimeImage.fillAmount < 1)
        {
            LineAttackCoolTimeImage.fillAmount += 1 * Time.smoothDeltaTime / coolTime;
            yield return null;
        }
        LineAttackCoolTimeImage.fillAmount = 1;
        yield break;
    }

    IEnumerator ExplosionCoolTime(float coolTime)
    {
        while (explosionCoolTimeImage.fillAmount < 1)
        {
            explosionCoolTimeImage.fillAmount += 1 * Time.smoothDeltaTime / coolTime;
            yield return null;
        }
        explosionCoolTimeImage.fillAmount = 1;
        yield break;
    }





}
