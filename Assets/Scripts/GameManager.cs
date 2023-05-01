using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject Player;

    [Header("UI")]
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public GameObject inventoryPanel;
    public GameObject LoadWoodsUIPannel;

    [Header("Manager")]
    public TalkManager talkManager;
    public QuestManager questManager;

    [Header("Camera")]
    public GameObject CampCamera;
    public GameObject WoodsCamera;

    [Header("SpawnArea")]
    public Transform CampSpawnArea;
    public Transform WestSpawnArea;
    public Transform EastSpawnArea;
    public Transform NorthSpawnArea;

    [Header("Talk")]
    public int talkIndex;

    public bool isAction;
    public bool activeInventory = false;
    

    public void talkAction(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        if(talkData == null)        // �� ĳ������ ��ȭ�� ������ ��
        {
            isAction = false;
            talkIndex = 0;
            return;         // -void �Լ����� return�� �Լ��� ���Ḧ �ǹ�-
        }

        if (isNpc)          // ��ȭ �б���(npc, ������ ���� ���� ����)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }

        isAction = true;
        talkIndex++;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }

    public void LoadCame()
    {
        SceneManager.LoadScene("Camp");
    }

    public void LocatePlayerAtCamp()      // ��ȸ�ҷ� �̵�
    {
        CampCamera.GetComponent<CameraController>().center = new Vector2(0, 0);
        CampCamera.GetComponent<CameraController>().size = new Vector2(18, 10);
        Player.transform.position = CampSpawnArea.position;
        Player.GetComponent<PlayerController>().isPlayerInWoods = false;
    }

    public void LocatePlayerAtWoods()     // ������ �̵�, ���Ŀ� ����, ���� ���� �����ϰ� �����ϱ�
    {
        Player.transform.position = WestSpawnArea.position;
        CampCamera.GetComponent<CameraController>().center = new Vector2(43, 9);
        CampCamera.GetComponent<CameraController>().size = new Vector2(54, 28);
        LoadWoodsUIPannel.SetActive(false);
        Player.GetComponent<PlayerController>().isPlayerInWoods = true;
        Time.timeScale = 1f;
    }

    public void CloseLoadWoodsUIPannelandResume()
    {
        LoadWoodsUIPannel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ResumeGame()        // ���� �Ͻ����� ����
    {
        Time.timeScale = 1f;
    }


}
