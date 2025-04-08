using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCreate : MonoBehaviour
{
    // 터치 위치 저장 변수
    private Vector2 TouchPosition;

    // 카메라 참조
    private new Camera camera;

    // 생성된 박스를 담는 변수
    private GameObject Box1;

    // 게임 매니저와 UI 매니저 참조
    private GameManager gameManager;
    private UIManager ui;

    // 시간 관련 변수 (현재 코드에서는 사용되지 않음)
    private float time;
    private float wait = 0.5f;

    // 박스 프리팹과 컴포넌트들
    public GameObject prefab;
    public Rigidbody2D rigid;
    public GameObject floor;

    // 상태 체크용 변수
    public int Count = 0;

    // 오디오 소스
    public new AudioSource audio;

    void Start()
    {
        // 참조 초기화
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    void Update()
    {
        // 터치 허용 상태이고, 화면에 터치가 감지되며, 라인이 생성되지 않은 상태일 때만 실행
        if (!ui.nottouch && Input.touchCount > 0 && !gameManager.line)
        {
            Touch touch = Input.GetTouch(0);

            // 터치 시작 시
            if (touch.phase == TouchPhase.Began && !gameManager.grow)
            {
                audio.Play(); // 효과음 재생
                ui.destroy = false;

                // 터치 위치를 월드 좌표로 변환
                TouchPosition = touch.position;
                TouchPosition = camera.ScreenToWorldPoint(TouchPosition);

                // 박스 생성 및 설정
                Box1 = Instantiate(prefab) as GameObject;
                Box1.transform.SetParent(this.transform, false);
                Box1.transform.position = new Vector3(TouchPosition.x, gameManager.By, Box1.transform.position.z);

                // 중력 비활성화 (떨어지지 않도록)
                rigid = Box1.GetComponent<Rigidbody2D>();
                rigid.gravityScale = 0;

                Count = 1; // 박스 생성 상태 표시
            }

            // 터치 중 이동일 때 박스를 따라 움직임
            if (touch.phase == TouchPhase.Moved)
            {
                TouchPosition = touch.position;
                TouchPosition = camera.ScreenToWorldPoint(TouchPosition);

                Vector2 box = Box1.transform.position;
                box.x = TouchPosition.x; // x축만 따라 움직임
                Box1.transform.position = box;
            }

            // 터치 종료 시 중력을 다시 활성화해 박스를 떨어뜨림
            if (touch.phase == TouchPhase.Ended)
            {
                rigid.gravityScale = 1;
                rigid.velocity = Vector3.zero; // 이동 속도 초기화
            }
        }
        else
        {
            return; // 조건을 만족하지 않으면 아무 동작도 하지 않음
        }
    }
}
