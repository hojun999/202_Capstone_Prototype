using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBloodLine : MonoBehaviour
{
    Camera MainCamera;

    public GameObject BloodLine;

    Vector2 firstMousePos, secondMousePos;
    public int mouseClickScore;

    public float axis;

    public bool isUsingTime;
    public bool isExistLineObject;


    private void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mouseClickScore = 0;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isUsingTime)      // �ڷ�ƾ�� �����Ͽ� ��ų ����� ���� ���콺 Ŭ�� �� ���� �Է� ���� ���¸� ����
        {
            StartCoroutine(usingTimeOfBloodLine(2.5f));
        }

        if (Input.GetMouseButtonDown(0) && mouseClickScore == 0 && isUsingTime)
        {
            firstMousePos = Input.mousePosition;
            firstMousePos = MainCamera.ScreenToWorldPoint(firstMousePos);
            mouseClickScore++;
        }
        else if (Input.GetMouseButtonDown(0) && mouseClickScore == 1 && isUsingTime)
        {
            secondMousePos = Input.mousePosition;
            secondMousePos = MainCamera.ScreenToWorldPoint(secondMousePos);
            mouseClickScore++;
        }


        if (mouseClickScore == 2 && !isExistLineObject)    // ���콺 Ŭ�� �� ��°�� �� ����obj ����
            StartCoroutine(createAndDestroyAttackObj());        // ��update������ �Լ� �� ���� �ߵ���Ű�� ���� �� �޼ҵ庸�� bool�� �̿��Ͽ� �ڷ�ƾ���� �ۼ�
            
    }

    IEnumerator usingTimeOfBloodLine(float endTime)     // ��ų ��� �� ���� ���� ���� �ð�(���� ���� �ð�)
    {
        // linerenderer, ���콺 ������ ���󰡴� �ڵ� �ۼ��ؼ� ���� UI ǥ���ϱ�
        // �ڷ�ƾ ������ �Լ��� �ٸ� ��ũ��Ʈ���� ������ �� ���� �� ����..
        isUsingTime = true;
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
        mouseClickScore = 0;
        isUsingTime = false;
    }

    private void designateRange()       // ���콺 Ŭ�� �� ������ ���� ����
    {   
        if (Input.GetMouseButtonDown(0) && mouseClickScore == 0 && isUsingTime)
        {
            firstMousePos = Input.mousePosition;
            mouseClickScore++;
        }
        else if (Input.GetMouseButtonDown(0) && mouseClickScore == 1 && isUsingTime)
        {
            secondMousePos = Input.mousePosition;
            mouseClickScore++;
        }
    }

    IEnumerator createAndDestroyAttackObj()
    {
        mouseClickScore = 0;            // ��ų ������Ʈ�� �ߺ� �������� �ʱ� ����
        isExistLineObject = true;       // ������ ��ų ���� �� ������Ʈ ������ ���� ������

        Vector2 attackDir = secondMousePos - firstMousePos;
        Vector3 quaternionToTarget = Quaternion.Euler(0, 0, 90) * attackDir;        // ��ų ������Ʈ�� secondMousePos ���������� �����ϴ� �ڵ�

        var bloodLineCopy = Instantiate(BloodLine, firstMousePos, Quaternion.LookRotation(forward: Vector3.forward, upwards: quaternionToTarget));
        yield return new WaitForSeconds(1.2f);        // WaitForSeconds(a) >> a�ð��� ��ų ������Ʈ ���� �ð����ȸ� �Ҵ�

        Destroy(bloodLineCopy);
        isExistLineObject = false;
    }






}
