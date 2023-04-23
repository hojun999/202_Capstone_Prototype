using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    private Camera MainCamera;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;

    public SkillBloodLine sbl;

    public bool canFire;

    private float timer;
    public float timeBetweenFiring;     // ����ü �߻� ������ �ð�

    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        sbl = GetComponent<SkillBloodLine>();
    }

    void Update()
    {
        mousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }


        if (Input.GetMouseButtonDown(0) && canFire)
        {
            canFire = false;
            Vector3 dir = (Input.mousePosition - gameObject.transform.position).normalized;
            Instantiate(bullet, bulletTransform.position, Quaternion.LookRotation(dir));
        }

    }
}
