using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // 메인 카메라 참조
    private new Camera camera;

    // 게임 매니저와 UI 매니저 참조
    private GameManager gameManager;
    private UIManager ui;

    // 충돌 횟수 카운터
    [SerializeField] private int Count = 0;

    // 물리 및 충돌 컴포넌트
    public Rigidbody2D rigid;
    public new Collider2D collider;

    private void Start()
    {
        // 초기화
        Count = 0;

        // 참조 객체들 초기화
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();

        // 오브젝트의 크기를 랜덤하게 조절
        transform.localScale = new Vector2(Random.Range(1f, 1.5f), Random.Range(0.5f, 1.5f));
    }

    private void Update()
    {
        // UIManager에서 파괴 지시가 내려오면 이 오브젝트 삭제
        if (ui.destroy)
        {
            Destroy(gameObject);
        }

        // 카메라의 위치 및 줌 레벨을 게임 매니저 값에 따라 갱신
        camera.transform.position = new Vector3(0, gameManager.y, -10);
        camera.orthographicSize = gameManager.size;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 첫 번째 트리거 충돌 시 카운트 증가
        if (Count == 0)
        {
            Count++;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 특정 태그를 가진 오브젝트에 닿아 있고, 카운트가 2 이상일 경우
        if (collision.gameObject.CompareTag("Line") && Count >= 2)
        {
            // 라인 충돌 처리: 게임 매니저 값을 변경해 카메라 위치와 줌을 조절하게 함
            gameManager.line = true;
            gameManager.y += 2;
            gameManager.size += 2;

            // 카운트 초기화
            Count = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // SaveTag를 가진 오브젝트와 충돌 시 태그 설정 및 카운트 증가
        if (collision.gameObject.CompareTag("SaveTag"))
        {
            gameObject.tag = "SaveTag";
            Count++;
        }
    }
}