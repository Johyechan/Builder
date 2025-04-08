using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ī�޶� ��ġ�� ������ ���� ���� ����
    public float y;
    public float size;

    // ���� ���� ����
    public bool line;

    // ���� ������Ʈ
    public GameObject Line;

    // �ڽ� ���� y ��ġ
    public float By = 4.5f;

    // ī�޶� Ŀ���� �ִ� ������ ����
    public bool grow = false;

    // ���� ����
    public int score = 0;

    // ī�޶� x�� ���� ���� (���� ������ ����)
    public int camX = 1;

    // ���ο� ���� ��ġ ����
    private float Ly = 0;

    // ������ x�� ������
    private float Lsx = 1;

    // UI �Ŵ���, ī�޶�, �ڽ� ���� ��ũ��Ʈ ����
    private UIManager ui;
    private new Camera camera;
    private BoxCreate boxCreate;

    private void Start()
    {
        // �ػ� ����
        SetScreen();

        // �ʱ� ���� ����
        grow = false;
        y = 0;
        size = 5;
        line = false;

        // ���� �ʱ�ȭ
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        boxCreate = GameObject.Find("BoxCreate").GetComponent<BoxCreate>();
    }

    private void Update()
    {
        // UI���� ���� ����� ������ ���� ���� �ʱ�ȭ
        if (ui.reset)
        {
            score = 0;
            By = 4.5f;
            y = 0;
            size = 5;
            line = false;
            Ly = 0;

            // ī�޶� ��ġ�� �� �ʱ�ȭ
            camera.transform.position = new Vector3(0, y, -10);
            camera.orthographicSize = size;

            // UI �÷��� �ʱ�ȭ
            ui.reset = false;
            ui.once = false;
        }

        // ������ ��ġ �� ũ�� ����
        Line.transform.position = new Vector2(0, Ly);
        Line.transform.localScale = new Vector2(Lsx, 1);

        // ���� ���� ��û�� ������ �� ó��
        if (line)
        {
            Ly += 4;     // ���� ��ġ ���� �̵�
            By += 4;     // ���� �ڽ� ���� ��ġ�� ���� �̵�
            score++;     // ���� ����

            ui.collider.enabled = false; // �ݶ��̴� ��Ȱ��ȭ (�ߺ� ó�� ����?)
            line = false;                // ���� �÷��� �ʱ�ȭ
        }
        else
        {
            // ������ ���� �� �ݶ��̴� �ٽ� Ȱ��ȭ
            ui.collider.enabled = true;
        }

        // ����׿� ���� ���
        Debug.Log(score);
    }

    // ȭ�� �ػ� ���� ����
    private void SetScreen()
    {
        int width = 1440;
        int height = 2560;

        // ��ü ȭ�� ���� �ػ� ����
        Screen.SetResolution(width, height, true);
    }
}
