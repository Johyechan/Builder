using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float y;
    public float size;
    public bool line;
    public GameObject Line;
    public float By = 4.5f;
    public bool grow = false;
    public int score = 0;
    public int camX = 1;

    private float Ly = 0;
    private float Lsx = 1;
    private UIManager ui;
    private new Camera camera;
    private BoxCreate boxCreate;
    

    private void Start()
    {
        SetScreen();
        grow = false;
        y = 0;
        size = 5;
        line = false;
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        boxCreate = GameObject.Find("BoxCreate").GetComponent<BoxCreate>();
    }
    private void Update()
    {
        if(ui.reset)
        {
            score = 0;
            By = 4.5f;
            y = 0;
            size = 5;
            line = false;
            Ly = 0;
            camera.transform.position = new Vector3(0, y, -10);
            camera.orthographicSize = size;
            ui.reset = false;
            ui.once = false;
        }
        Line.transform.position = new Vector2(0, Ly);
        Line.transform.localScale = new Vector2(Lsx, 1);
        if (line)
        {
            Ly += 4;
            By += 4;
            score++;
            ui.collider.enabled = false;
            line = false;
        }
        else
        {
            ui.collider.enabled = true;
        }
        Debug.Log(score);
    }

    private void SetScreen()
    {
        int width = 1440;
        int height = 2560 ;

        Screen.SetResolution(width, height, true);
    }
}
