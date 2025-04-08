using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchParticle : MonoBehaviour
{
    // 터치 효과로 사용할 파티클 프리팹
    [SerializeField] private ParticleSystem touchEff;

    // 터치 위치 저장용
    Vector2 touchTransform;

    // 타이머 관련 변수
    float timer;
    float maxtime;

    void Update()
    {
        // 매 프레임 터치 체크
        TouchCount();
    }

    void TouchCount()
    {
        // 터치가 하나라도 있을 때만 처리
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        // 터치가 시작됐을 때 타이머 초기화
        if (touch.phase == TouchPhase.Began)
        {
            timer = 0.17f;
        }

        // 터치 중이거나 고정 상태면 타이머 증가
        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            timer += Time.deltaTime;
        }

        // 현재 터치 위치를 월드 좌표로 변환
        touchTransform = Camera.main.ScreenToWorldPoint(touch.position);

        // 0.17초가 지나면 파티클 생성
        if (timer >= 0.17f)
        {
            // 파티클 생성 후 0.9초 뒤에 제거
            ParticleSystem prefab = Instantiate(touchEff, touchTransform, touchEff.transform.rotation);
            Destroy(prefab.gameObject, 0.9f);

            // 타이머 초기화
            timer = 0;
        }
    }
}
