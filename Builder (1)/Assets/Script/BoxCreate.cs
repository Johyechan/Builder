using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCreate : MonoBehaviour
{
    private Vector2 TouchPosition;
    private new Camera camera;
    private GameObject Box1;
    private GameManager gameManager;
    private UIManager ui;
    private float time;
    private float wait = 0.5f;

    public GameObject prefab;
    public Rigidbody2D rigid;
    public GameObject floor;
    public int Count = 0;
    public new AudioSource audio;

    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    void Update()
    {
        if (!ui.nottouch && Input.touchCount > 0 && !gameManager.line)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && !gameManager.grow)
            {
                audio.Play();
                ui.destroy = false;
                TouchPosition = touch.position;
                TouchPosition = camera.ScreenToWorldPoint(TouchPosition);

                Box1 = Instantiate(prefab) as GameObject;
                Box1.transform.SetParent(this.transform, false);
                Box1.transform.position = new Vector3(TouchPosition.x, gameManager.By, Box1.transform.position.z);
                rigid = Box1.GetComponent<Rigidbody2D>();
                rigid.gravityScale = 0;
                Count = 1;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                TouchPosition = touch.position;
                TouchPosition = camera.ScreenToWorldPoint(TouchPosition);
                Vector2 box = Box1.transform.position;
                box.x = TouchPosition.x;
                Box1.transform.position = box;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                rigid.gravityScale = 1;
                rigid.velocity = Vector3.zero;
            }
        }
        else
        {
            return;
        }
    }

    /*private void Score(int lenght)
    {
        int[] sco = new int[lenght];
        for(int i = 0; i < lenght; i++)
        {
            if(i == 0)
            {
                sco[i] = (int)Vector2.Distance(floor.transform.position, transform.GetChild(i).gameObject.transform.position);
            }
            else if(sco[i] > sco[0])
            {
                sco[0] = sco[i];
            }
            else
            {
                return;
            }
        }
        score = sco[0];
    }*/
}
