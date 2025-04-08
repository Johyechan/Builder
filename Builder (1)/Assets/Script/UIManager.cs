using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // 점수 표시용 텍스트
    public TextMeshProUGUI txt;   // 게임 오버 시 표시되는 점수
    public TextMeshProUGUI txt2;  // 실시간 점수 표시

    // 배경 이미지 및 UI 오브젝트들
    public Image image;
    public GameObject play;
    public GameObject start;
    public GameObject exit;

    // 충돌 판정용 콜라이더 (라인 충돌용으로 사용됨)
    public new Collider2D collider;

    // 상태 플래그
    public bool reset;      // 게임 리셋 여부
    public bool destroy;    // 오브젝트 제거 여부
    public bool nottouch;   // 터치 입력 제한 여부
    public bool once = false; // 트리거 중복 진입 방지용

    [SerializeField] private FadeManager fadeManager;

    // 게임 매니저 참조
    private GameManager gameManager;

    private void Start()
    {
        // 참조 및 초기 UI 설정
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        txt.enabled = false;
        txt.text = 0.ToString();

        txt2.enabled = false;
        txt2.text = 0.ToString();

        image.enabled = false;
        play.SetActive(false);
        exit.SetActive(false);
        nottouch = true; // 처음에는 터치 비활성화
    }

    private void Update()
    {
        // 실시간 점수 표시 업데이트
        txt2.text = gameManager.score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어가 특정 영역에 진입했을 때 한 번만 실행
        if (!once)
        {
            txt.enabled = true;
            txt.text = gameManager.score.ToString() + "F"; // 점수 + "F" 표시
            txt2.enabled = false;
            image.enabled = true;
            destroy = false;
            nottouch = true; // 터치 막기
            exit.SetActive(true);

            // 페이드 애니메이션으로 버튼과 배경 나타내기
            StartCoroutine(fadeManager.ButtonFadeInCoroutine(play.GetComponent<Button>(), 1));
            StartCoroutine(fadeManager.ObjectFadeInCoroutine(image));
            StartCoroutine(fadeManager.ButtonFadeInCoroutine(exit.GetComponent<Button>(), 2));

            once = true; // 중복 실행 방지
        }
    }

    // 게임 다시 시작 (게임 오버 후 Play 버튼 눌렀을 때)
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

    // 첫 게임 시작 (Start 버튼 눌렀을 때)
    public void FristPlay()
    {
        Time.timeScale = 1;
        txt2.enabled = true;
        reset = true;
        nottouch = false;
    }

    // 게임 종료 버튼
    public void Exit()
    {
        Application.Quit();
    }
}
