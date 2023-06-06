using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTextBlink : MonoBehaviour
{
    public GameObject introTextManager;

    Text text;
    float time;      // introtextManager time�̶� ��ġ ���ƾߵ�.
    float cooltime;


    private void Awake()
    {
        text = GetComponent<Text>();
        time = introTextManager.GetComponent<IntroTextManager>().time;
        StartCoroutine(FadeTextToZero());
    }


    private void Update()
    {
        cooltime += Time.deltaTime;

        if (cooltime >= time)
        {
            gameObject.SetActive(false);
            cooltime = 0;
        }
    }


    public IEnumerator FadeTextToZero()
    {
        yield return new WaitForSeconds(3.5f);

        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 3));

            yield return null;
        }
    }

    

    // endIntroScene / ���� : ������ �ؽ�Ʈ ���� �� Camp �ε�

    // intro skip �߰�
}