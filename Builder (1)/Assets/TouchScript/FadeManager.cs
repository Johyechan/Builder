using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FadeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] textFade;
    [SerializeField] Button[] buttonFade;
    [SerializeField] Image panelImage;
    [SerializeField] UIManager uiManager;
    private void Start()
    {
        TextFadeIn();
        ButtonFadeIn();
    }
    public void TextFadeIn()
    {
        foreach (TextMeshProUGUI a in textFade)
        {
            StartCoroutine(TextFadeInCoroutine(a));
        }
    }
    public void TextFadeOut()
    {
        foreach (TextMeshProUGUI a in textFade)
        {
            StartCoroutine(TextFadeOutCoroutine(a));
        }
    }
    public void ButtonFadeIn()
    {
            StartCoroutine(ButtonFadeInCoroutine(buttonFade[0],0));
    }
    public void ButtonFadeOut()
    {
        foreach (Button a in buttonFade)
        {
            StartCoroutine(ButtonFadeOutCoroutine(buttonFade[0], 0));
        }
    }
    public void PlayButtonFadeOut()
    {
        StartCoroutine(ButtonFadeOutCoroutine(buttonFade[1],1));
    }
    public void PanelFadeOut()
    {
        StartCoroutine(ObjectFadeOutCoroutine(panelImage));
    }
    public IEnumerator TextFadeInCoroutine(TextMeshProUGUI a)
    {
        float fade = 0;
        while (fade < 1)
        {
            a.color = new Color(1, 1, 1, fade);
            fade += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    public IEnumerator TextFadeOutCoroutine(TextMeshProUGUI a)
    {
        float fade = 1;
        while (fade > 0)
        {
            a.color = new Color(1, 1, 1, fade);
            fade -= 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator ButtonFadeInCoroutine(Button a, int b)
    {
        float fade = 0;
        buttonFade[b].gameObject.SetActive(true);
        while (fade < 1)
        {
            a.GetComponent<Image>().color = new Color(1, 1, 1, fade);
            a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, fade);
            fade += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        a.interactable = true;
    }
    public IEnumerator ButtonFadeOutCoroutine(Button a, int b)
    {
        float fade = 1;
        a.interactable = false;
        while (fade > 0)
        {
            a.GetComponent<Image>().color = new Color(1, 1, 1, fade);
            a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, fade);
            fade -= 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        buttonFade[b].gameObject.SetActive(false);

    }
    public IEnumerator ObjectFadeInCoroutine(Image a)
    {
        float fade = 0;
        a.enabled = true;
        while (fade < 1)
        {
            a.color = new Color(1, 1, 1, fade);
            fade += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    public IEnumerator ObjectFadeOutCoroutine(Image a)
    {
        float fade = 1;
        while (fade > 0)
        {
            a.color = new Color(1, 1, 1, fade);
            fade -= 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        a.enabled = false;
    }
}
