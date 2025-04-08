using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeManager : MonoBehaviour
{
    // 페이드 인/아웃 시킬 텍스트, 버튼, 패널
    [SerializeField] TextMeshProUGUI[] textFade; // 텍스트 페이드용 배열
    [SerializeField] Button[] buttonFade;        // 버튼 페이드용 배열
    [SerializeField] Image panelImage;           // 배경 패널
    [SerializeField] UIManager uiManager;

    private void Start()
    {
        // 시작 시 텍스트와 버튼을 페이드 인 처리
        TextFadeIn();
        ButtonFadeIn();
    }

    // 모든 텍스트에 대해 페이드 인
    public void TextFadeIn()
    {
        foreach (TextMeshProUGUI a in textFade)
        {
            StartCoroutine(TextFadeInCoroutine(a));
        }
    }

    // 모든 텍스트에 대해 페이드 아웃
    public void TextFadeOut()
    {
        foreach (TextMeshProUGUI a in textFade)
        {
            StartCoroutine(TextFadeOutCoroutine(a));
        }
    }

    // 첫 번째 버튼 페이드 인
    public void ButtonFadeIn()
    {
        StartCoroutine(ButtonFadeInCoroutine(buttonFade[0], 0));
    }

    // 모든 버튼 페이드 아웃 (현재 첫 번째 버튼만 적용됨)
    public void ButtonFadeOut()
    {
        foreach (Button a in buttonFade)
        {
            StartCoroutine(ButtonFadeOutCoroutine(buttonFade[0], 0)); // 모든 버튼에 대해 buttonFade[0]만 실행됨 (수정 필요)
        }
    }

    // 두 번째 버튼 전용 페이드 아웃
    public void PlayButtonFadeOut()
    {
        StartCoroutine(ButtonFadeOutCoroutine(buttonFade[1], 1));
    }

    // 배경 패널 페이드 아웃
    public void PanelFadeOut()
    {
        StartCoroutine(ObjectFadeOutCoroutine(panelImage));
    }

    // 텍스트 페이드 인 코루틴
    public IEnumerator TextFadeInCoroutine(TextMeshProUGUI a)
    {
        float fade = 0;
        while (fade < 1)
        {
            a.color = new Color(1, 1, 1, fade); // 알파 값만 조절
            fade += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    // 텍스트 페이드 아웃 코루틴
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

    // 버튼 페이드 인 코루틴
    public IEnumerator ButtonFadeInCoroutine(Button a, int b)
    {
        float fade = 0;
        buttonFade[b].gameObject.SetActive(true); // 버튼 보이기
        while (fade < 1)
        {
            a.GetComponent<Image>().color = new Color(1, 1, 1, fade); // 배경 이미지
            a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, fade); // 텍스트
            fade += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        a.interactable = true;
    }

    // 버튼 페이드 아웃 코루틴
    public IEnumerator ButtonFadeOutCoroutine(Button a, int b)
    {
        float fade = 1;
        a.interactable = false; // 누를 수 없게
        while (fade > 0)
        {
            a.GetComponent<Image>().color = new Color(1, 1, 1, fade);
            a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, fade);
            fade -= 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        buttonFade[b].gameObject.SetActive(false); // 페이드 아웃 후 비활성화
    }

    // 이미지(예: 패널) 페이드 인
    public IEnumerator ObjectFadeInCoroutine(Image a)
    {
        float fade = 0;
        a.enabled = true; // 이미지 활성화
        while (fade < 1)
        {
            a.color = new Color(1, 1, 1, fade);
            fade += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    // 이미지 페이드 아웃
    public IEnumerator ObjectFadeOutCoroutine(Image a)
    {
        float fade = 1;
        while (fade > 0)
        {
            a.color = new Color(1, 1, 1, fade);
            fade -= 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        a.enabled = false; // 완전히 사라지면 비활성화
    }
}
