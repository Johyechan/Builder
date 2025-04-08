using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // ���� ǥ�ÿ� �ؽ�Ʈ
    public TextMeshProUGUI txt;   // ���� ���� �� ǥ�õǴ� ����
    public TextMeshProUGUI txt2;  // �ǽð� ���� ǥ��

    // ��� �̹��� �� UI ������Ʈ��
    public Image image;
    public GameObject play;
    public GameObject start;
    public GameObject exit;

    // �浹 ������ �ݶ��̴� (���� �浹������ ����)
    public new Collider2D collider;

    // ���� �÷���
    public bool reset;      // ���� ���� ����
    public bool destroy;    // ������Ʈ ���� ����
    public bool nottouch;   // ��ġ �Է� ���� ����
    public bool once = false; // Ʈ���� �ߺ� ���� ������

    [SerializeField] private FadeManager fadeManager;

    // ���� �Ŵ��� ����
    private GameManager gameManager;

    private void Start()
    {
        // ���� �� �ʱ� UI ����
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        txt.enabled = false;
        txt.text = 0.ToString();

        txt2.enabled = false;
        txt2.text = 0.ToString();

        image.enabled = false;
        play.SetActive(false);
        exit.SetActive(false);
        nottouch = true; // ó������ ��ġ ��Ȱ��ȭ
    }

    private void Update()
    {
        // �ǽð� ���� ǥ�� ������Ʈ
        txt2.text = gameManager.score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾ Ư�� ������ �������� �� �� ���� ����
        if (!once)
        {
            txt.enabled = true;
            txt.text = gameManager.score.ToString() + "F"; // ���� + "F" ǥ��
            txt2.enabled = false;
            image.enabled = true;
            destroy = false;
            nottouch = true; // ��ġ ����
            exit.SetActive(true);

            // ���̵� �ִϸ��̼����� ��ư�� ��� ��Ÿ����
            StartCoroutine(fadeManager.ButtonFadeInCoroutine(play.GetComponent<Button>(), 1));
            StartCoroutine(fadeManager.ObjectFadeInCoroutine(image));
            StartCoroutine(fadeManager.ButtonFadeInCoroutine(exit.GetComponent<Button>(), 2));

            once = true; // �ߺ� ���� ����
        }
    }

    // ���� �ٽ� ���� (���� ���� �� Play ��ư ������ ��)
    public void Play()
    {
        Time.timeScale = 1;

        txt.enabled = false;
        txt.text = "0F";

        txt2.enabled = true;
        txt2.text = "0";

        play.SetActive(false);
        exit.SetActive(false);

        reset = true;
        destroy = true;
        nottouch = false;

        StartCoroutine(fadeManager.ButtonFadeOutCoroutine(play.GetComponent<Button>(), 1));
        StartCoroutine(fadeManager.ObjectFadeOutCoroutine(image));
    }

    // ù ���� ���� (Start ��ư ������ ��)
    public void FristPlay()
    {
        Time.timeScale = 1;
        txt2.enabled = true;
        reset = true;
        nottouch = false;
    }

    // ���� ���� ��ư
    public void Exit()
    {
        Application.Quit();
    }
}
