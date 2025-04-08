using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // ���� ī�޶� ����
    private new Camera camera;

    // ���� �Ŵ����� UI �Ŵ��� ����
    private GameManager gameManager;
    private UIManager ui;

    // �浹 Ƚ�� ī����
    [SerializeField] private int Count = 0;

    // ���� �� �浹 ������Ʈ
    public Rigidbody2D rigid;
    public new Collider2D collider;

    private void Start()
    {
        // �ʱ�ȭ
        Count = 0;

        // ���� ��ü�� �ʱ�ȭ
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();

        // ������Ʈ�� ũ�⸦ �����ϰ� ����
        transform.localScale = new Vector2(Random.Range(1f, 1.5f), Random.Range(0.5f, 1.5f));
    }

    private void Update()
    {
        // UIManager���� �ı� ���ð� �������� �� ������Ʈ ����
        if (ui.destroy)
        {
            Destroy(gameObject);
        }

        // ī�޶��� ��ġ �� �� ������ ���� �Ŵ��� ���� ���� ����
        camera.transform.position = new Vector3(0, gameManager.y, -10);
        camera.orthographicSize = gameManager.size;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ù ��° Ʈ���� �浹 �� ī��Ʈ ����
        if (Count == 0)
        {
            Count++;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Ư�� �±׸� ���� ������Ʈ�� ��� �ְ�, ī��Ʈ�� 2 �̻��� ���
        if (collision.gameObject.CompareTag("Line") && Count >= 2)
        {
            // ���� �浹 ó��: ���� �Ŵ��� ���� ������ ī�޶� ��ġ�� ���� �����ϰ� ��
            gameManager.line = true;
            gameManager.y += 2;
            gameManager.size += 2;

            // ī��Ʈ �ʱ�ȭ
            Count = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // SaveTag�� ���� ������Ʈ�� �浹 �� �±� ���� �� ī��Ʈ ����
        if (collision.gameObject.CompareTag("SaveTag"))
        {
            gameObject.tag = "SaveTag";
            Count++;
        }
    }
}