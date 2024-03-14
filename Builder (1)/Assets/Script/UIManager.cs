using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public TextMeshProUGUI txt2;
    public Image image;
    public GameObject play;
    public GameObject start;
    public GameObject exit;
    public new Collider2D collider;

    public bool reset;
    public bool destroy;
    public bool nottouch;
    public bool once = false;

    [SerializeField] private FadeManager fadeManager;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        txt.enabled = false;
        txt.text = 0.ToString();
        txt2.enabled = false;
        txt2.text = 0.ToString();
        image.enabled = false;
        play.SetActive(false);
        exit.SetActive(false);
        nottouch = true;
    }
    private void Update()
    {
        txt2.text = gameManager.score.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!once)
        {
            txt.enabled = true;
            txt.text = gameManager.score.ToString() + "F";
            txt2.enabled = false;
            image.enabled = true;
            destroy = false;
            nottouch = true;
            exit.SetActive(true);
            StartCoroutine(fadeManager.ButtonFadeInCoroutine(play.GetComponent<Button>(), 1));
            StartCoroutine(fadeManager.ObjectFadeInCoroutine(image));
            StartCoroutine(fadeManager.ButtonFadeInCoroutine(exit.GetComponent<Button>(), 2));
            once = true;
        }
    }
    public void Play()
    {
        Time.timeScale = 1;
        txt.enabled = false;
        txt.text = 0.ToString() + "F";
        txt2.enabled = true;
        txt2.text = 0.ToString();
        play.SetActive(false);
        exit.SetActive(false);
        reset = true;
        destroy = true;
        nottouch = false;
        StartCoroutine(fadeManager.ButtonFadeOutCoroutine(play.GetComponent<Button>(),1));
        StartCoroutine(fadeManager.ObjectFadeOutCoroutine(image));
    }

    public void FristPlay()
    {
        Time.timeScale = 1;
        txt2.enabled = true;
        reset = true;
        nottouch = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
