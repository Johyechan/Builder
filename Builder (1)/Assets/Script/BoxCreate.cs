using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCreate : MonoBehaviour
{
    // ��ġ ��ġ ���� ����
    private Vector2 TouchPosition;

    // ī�޶� ����
    private new Camera camera;

    // ������ �ڽ��� ��� ����
    private GameObject Box1;

    // ���� �Ŵ����� UI �Ŵ��� ����
    private GameManager gameManager;
    private UIManager ui;

    // �ð� ���� ���� (���� �ڵ忡���� ������ ����)
    private float time;
    private float wait = 0.5f;

    // �ڽ� �����հ� ������Ʈ��
    public GameObject prefab;
    public Rigidbody2D rigid;
    public GameObject floor;

    // ���� üũ�� ����
    public int Count = 0;

    // ����� �ҽ�
    public new AudioSource audio;

    void Start()
    {
        // ���� �ʱ�ȭ
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    void Update()
    {
        // ��ġ ��� �����̰�, ȭ�鿡 ��ġ�� �����Ǹ�, ������ �������� ���� ������ ���� ����
        if (!ui.nottouch && Input.touchCount > 0 && !gameManager.line)
        {
            Touch touch = Input.GetTouch(0);

            // ��ġ ���� ��
            if (touch.phase == TouchPhase.Began && !gameManager.grow)
            {
                audio.Play(); // ȿ���� ���
                ui.destroy = false;

                // ��ġ ��ġ�� ���� ��ǥ�� ��ȯ
                TouchPosition = touch.position;
                TouchPosition = camera.ScreenToWorldPoint(TouchPosition);

                // �ڽ� ���� �� ����
                Box1 = Instantiate(prefab) as GameObject;
                Box1.transform.SetParent(this.transform, false);
                Box1.transform.position = new Vector3(TouchPosition.x, gameManager.By, Box1.transform.position.z);

                // �߷� ��Ȱ��ȭ (�������� �ʵ���)
                rigid = Box1.GetComponent<Rigidbody2D>();
                rigid.gravityScale = 0;

                Count = 1; // �ڽ� ���� ���� ǥ��
            }

            // ��ġ �� �̵��� �� �ڽ��� ���� ������
            if (touch.phase == TouchPhase.Moved)
            {
                TouchPosition = touch.position;
                TouchPosition = camera.ScreenToWorldPoint(TouchPosition);

                Vector2 box = Box1.transform.position;
                box.x = TouchPosition.x; // x�ุ ���� ������
                Box1.transform.position = box;
            }

            // ��ġ ���� �� �߷��� �ٽ� Ȱ��ȭ�� �ڽ��� ����߸�
            if (touch.phase == TouchPhase.Ended)
            {
                rigid.gravityScale = 1;
                rigid.velocity = Vector3.zero; // �̵� �ӵ� �ʱ�ȭ
            }
        }
        else
        {
            return; // ������ �������� ������ �ƹ� ���۵� ���� ����
        }
    }
}
