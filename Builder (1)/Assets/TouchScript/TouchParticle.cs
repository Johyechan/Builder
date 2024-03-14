using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem touchEff;
    Vector2 touchTransform;
    float timer;
    float maxtime;
    void Update()
    {
        TouchCount();
    }
    void TouchCount()
    {

        Touch touch = Input.GetTouch(0);
        
        if (touch.phase == TouchPhase.Began)
        {
            timer = 0.17f;
        }
        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {         
            timer += Time.deltaTime;
        }
        touchTransform = Camera.main.ScreenToWorldPoint(touch.position);
        if (timer >= 0.17f)
        {
            ParticleSystem prefab = Instantiate(touchEff, touchTransform, touchEff.transform.rotation);
            Destroy(prefab.gameObject, 0.9f);
            timer = 0;
        }
    }
}
