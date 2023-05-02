using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float hp;
    public float energy;

    public GameManager gameManager;
    public InventoryManager inventoryManager;

    public GameObject shootObject;

    public Item[] fieldItems;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    Color halfA = new Color(1, 1, 1, 0);
    Color fullA = new Color(1, 1, 1, 1);

    private float h, v;

    public float moveSpeed;

    private float timer;
    [SerializeField]
    private float dodgeCooltime;        // dodge ��Ÿ��

    public bool isPlayerInWoods;
   
    private bool isWalk;
    private bool isHurt;
    private bool isDashButtonDown;
    private bool isReadyDash;

    [HideInInspector]public Vector3 moveDir;
    GameObject scanObject;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        isReadyDash = true;     // ���ۺ��� �뽬 ����

    }

    private void Update()
    {
        h = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = gameManager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        // �ִϸ��̼�
        #region
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false);
        #endregion


        // �÷��̾� ������ ����
        #region
        if (v == 1)
            moveDir = Vector2.up;
        else if (v == -1)
            moveDir = Vector2.down;
        else if (h == 1)
            moveDir = Vector2.right;
        else if (h == -1)
            moveDir = Vector2.left;
        #endregion

        // ������Ʈ ��ĵ
        if (Input.GetButtonDown("Jump") && scanObject != null)
            gameManager.talkAction(scanObject);

        // Ray - Object Layer�� scanObject�� �Ҵ�
        #region
        Debug.DrawRay(rb.position, moveDir * 0.6f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, moveDir, 1f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
        #endregion

        // dash
        if (Input.GetKeyDown(KeyCode.Space) && !gameManager.isAction && isReadyDash)
            isDashButtonDown = true;


        // �������� �ν��ϰ� E�� ���� ȹ���ϸ�, �ʵ� �������� �κ��丮 ���������� ��ȯ
        if (Input.GetKeyDown(KeyCode.E) && scanObject.gameObject.CompareTag("Item"))
        {
            if (scanObject.name == "Clothes_With_Blood")
            {
                GetItem(0);
                scanObject.SetActive(false);
            }
            else if(scanObject.name == "HpPosion")
            {
                GetItem(1);
                scanObject.SetActive(false);
            }
            else if (scanObject.name == "Injector")
            {
                GetItem(2);
                scanObject.SetActive(false);
            }
            else if (scanObject.name == "MalfunctionedGun")
            {
                GetItem(3);
                scanObject.SetActive(false);
            }
            else if (scanObject.name == "SpeedUpPosion")
            {
                GetItem(4);
                scanObject.SetActive(false);
            }
        }

        if (gameManager.isAction || gameManager.activeInventory || !isPlayerInWoods)    // �κ��丮 Ȱ��ȭ, ��ȭ ��, ķ������ ���� �Ұ���
            shootObject.SetActive(false);
        else
            shootObject.SetActive(true);

            
    }

    private void FixedUpdate()
    {
        //move
        #region

        if (h != 0 || v != 0)
            isWalk = true;
        else // ������ �Է��� ���� �� rb.velocity ���� ���� ����
        {
            isWalk = false;
            rb.velocity = new Vector2(0, 0);
        }

        if (isWalk)     // ������ �Է��� ���� ���� �̵�
            rb.velocity = new Vector2(h, v).normalized * moveSpeed;
        #endregion

        //dash
        #region
        if (!isReadyDash && !gameManager.isAction)
        {
            timer += Time.deltaTime;
            if (timer > dodgeCooltime)
            {
                isReadyDash = true;
                timer = 0;
            }
        }

        if (isDashButtonDown && isReadyDash)
        {
            StartCoroutine(Dash());
            isDashButtonDown = false;
        }
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAtk"))
        {
            Hurt(collision.GetComponentInParent<Enemy>().damage);
        }
    }

    public void Hurt(int damage)
    {
        if (!isHurt)
        {
            isHurt = true;
            hp = hp - damage;
            if (hp <= 0)
            {
                //dead
            }
            else
            {
                Debug.Log("hp : " + hp);
                StartCoroutine(HurtRoutine());
                StartCoroutine(alphablink());
            }
        }
    }

    IEnumerator Dash()
    {
        float dodgePower = 90f;
        rb.AddForce(new Vector2(h, v).normalized * dodgePower, ForceMode2D.Impulse);     // ������ ���� ���� addforce ���� ����
        isReadyDash = false;
        yield return new WaitForSeconds(2f);        // �뽬 ��Ÿ��
        //isDashButtonDown = false;
    }

    public void GetItem(int id)
    {
        inventoryManager.AddItem(fieldItems[id]);
    }

    IEnumerator HurtRoutine()   // ���� ���� ����
    {
        yield return new WaitForSeconds(2.5f);
        isHurt = false;
    }

    IEnumerator alphablink()    // �ǰ� �� �����̴� ȿ��
    {
        while (isHurt)
        {
            yield return new WaitForSeconds(0.1f);
            sr.color = halfA;
            yield return new WaitForSeconds(0.1f);
            sr.color = fullA;
        }
    }
}


