using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private new Camera camera;
    private GameManager gameManager;
    private UIManager ui;
    [SerializeField] private int Count = 0;

    public Rigidbody2D rigid;
    public new Collider2D collider;

    private void Start()
    {
        Count = 0;
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        transform.localScale = new Vector2(Random.Range(1f, 1.5f), Random.Range(0.5f, 1.5f));
    }

    private void Update()
    {
        if (ui.destroy)
        {
            Destroy(gameObject);
        }
        camera.transform.position = new Vector3(0,  gameManager.y, -10);
        camera.orthographicSize = gameManager.size;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Count == 0)
        {
            Count++;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Line") && Count >= 2)
        {
            gameManager.line = true;
            gameManager.y += 2;
            gameManager.size += 2;
            Count = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("SaveTag"))
        {
            gameObject.tag = "SaveTag";
            Count++;
        }
    }
}
