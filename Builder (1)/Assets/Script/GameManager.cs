using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 카메라 위치와 사이즈 조절 관련 변수
    public float y;
    public float size;

    // 라인 생성 여부
    public bool line;

    // 라인 오브젝트
    public GameObject Line;

    // 박스 생성 y 위치
    public float By = 4.5f;

    // 카메라가 커지고 있는 중인지 여부
    public bool grow = false;

    // 현재 점수
    public int score = 0;

    // 카메라 x축 관련 변수 (현재 사용되지 않음)
    public int camX = 1;

    // 내부용 라인 위치 변수
    private float Ly = 0;

    // 라인의 x축 스케일
    private float Lsx = 1;

    // UI 매니저, 카메라, 박스 생성 스크립트 참조
    private UIManager ui;
    private new Camera camera;
    private BoxCreate boxCreate;

    private void Start()
    {
        // 해상도 설정
        SetScreen();

        // 초기 상태 설정
        grow = false;
        y = 0;
        size = 5;
        line = false;

        // 참조 초기화
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        boxCreate = GameObject.Find("BoxCreate").GetComponent<BoxCreate>();
    }

    private void Update()
    {
        // UI에서 리셋 명령이 들어오면 게임 상태 초기화
        if (ui.reset)
        {
            score = 0;
            By = 4.5f;
            y = 0;
            size = 5;
            line = false;
            Ly = 0;

            // 카메라 위치와 줌 초기화
            camera.transform.position = new Vector3(0, y, -10);
            camera.orthographicSize = size;

            // UI 플래그 초기화
            ui.reset = false;
            ui.once = false;
        }

        // 라인의 위치 및 크기 설정
        Line.transform.position = new Vector2(0, Ly);
        Line.transform.localScale = new Vector2(Lsx, 1);

        // 라인 생성 요청이 들어왔을 때 처리
        if (line)
        {
            Ly += 4;     // 라인 위치 위로 이동
            By += 4;     // 다음 박스 생성 위치도 위로 이동
            score++;     // 점수 증가

            ui.collider.enabled = false; // 콜라이더 비활성화 (중복 처리 방지?)
            line = false;                // 라인 플래그 초기화
        }
        else
        {
            // 라인이 없을 땐 콜라이더 다시 활성화
            ui.collider.enabled = true;
        }

        // 디버그용 점수 출력
        Debug.Log(score);
    }

    // 화면 해상도 고정 설정
    private void SetScreen()
    {
        int width = 1440;
        int height = 2560;

        // 전체 화면 모드로 해상도 설정
        Screen.SetResolution(width, height, true);
    }
}
