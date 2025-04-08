using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchParticle : MonoBehaviour
{
    // ��ġ ȿ���� ����� ��ƼŬ ������
    [SerializeField] private ParticleSystem touchEff;

    // ��ġ ��ġ �����
    Vector2 touchTransform;

    // Ÿ�̸� ���� ����
    float timer;
    float maxtime;

    void Update()
    {
        // �� ������ ��ġ üũ
        TouchCount();
    }

    void TouchCount()
    {
        // ��ġ�� �ϳ��� ���� ���� ó��
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        // ��ġ�� ���۵��� �� Ÿ�̸� �ʱ�ȭ
        if (touch.phase == TouchPhase.Began)
        {
            timer = 0.17f;
        }

        // ��ġ ���̰ų� ���� ���¸� Ÿ�̸� ����
        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            timer += Time.deltaTime;
        }

        // ���� ��ġ ��ġ�� ���� ��ǥ�� ��ȯ
        touchTransform = Camera.main.ScreenToWorldPoint(touch.position);

        // 0.17�ʰ� ������ ��ƼŬ ����
        if (timer >= 0.17f)
        {
            // ��ƼŬ ���� �� 0.9�� �ڿ� ����
            ParticleSystem prefab = Instantiate(touchEff, touchTransform, touchEff.transform.rotation);
            Destroy(prefab.gameObject, 0.9f);

            // Ÿ�̸� �ʱ�ȭ
            timer = 0;
        }
    }
}
