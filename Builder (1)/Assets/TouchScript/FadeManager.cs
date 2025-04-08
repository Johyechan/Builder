using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeManager : MonoBehaviour
{
    // ���̵� ��/�ƿ� ��ų �ؽ�Ʈ, ��ư, �г�
    [SerializeField] TextMeshProUGUI[] textFade; // �ؽ�Ʈ ���̵�� �迭
    [SerializeField] Button[] buttonFade;        // ��ư ���̵�� �迭
    [SerializeField] Image panelImage;           // ��� �г�
    [SerializeField] UIManager uiManager;

    private void Start()
    {
        // ���� �� �ؽ�Ʈ�� ��ư�� ���̵� �� ó��
        TextFadeIn();
        ButtonFadeIn();
    }

    // ��� �ؽ�Ʈ�� ���� ���̵� ��
    public void TextFadeIn()
    {
        foreach (TextMeshProUGUI a in textFade)
        {
            StartCoroutine(TextFadeInCoroutine(a));
        }
    }

    // ��� �ؽ�Ʈ�� ���� ���̵� �ƿ�
    public void TextFadeOut()
    {
        foreach (TextMeshProUGUI a in textFade)
        {
            StartCoroutine(TextFadeOutCoroutine(a));
        }
    }

    // ù ��° ��ư ���̵� ��
    public void ButtonFadeIn()
    {
        StartCoroutine(ButtonFadeInCoroutine(buttonFade[0], 0));
    }

    // ��� ��ư ���̵� �ƿ� (���� ù ��° ��ư�� �����)
    public void ButtonFadeOut()
    {
        foreach (Button a in buttonFade)
        {
            StartCoroutine(ButtonFadeOutCoroutine(buttonFade[0], 0)); // ��� ��ư�� ���� buttonFade[0]�� ����� (���� �ʿ�)
        }
    }

    // �� ��° ��ư ���� ���̵� �ƿ�
    public void PlayButtonFadeOut()
    {
        StartCoroutine(ButtonFadeOutCoroutine(buttonFade[1], 1));
    }

    // ��� �г� ���̵� �ƿ�
    public void PanelFadeOut()
    {
        StartCoroutine(ObjectFadeOutCoroutine(panelImage));
    }

    // �ؽ�Ʈ ���̵� �� �ڷ�ƾ
    public IEnumerator TextFadeInCoroutine(TextMeshProUGUI a)
    {
        float fade = 0;
        while (fade < 1)
        {
            a.color = new Color(1, 1, 1, fade); // ���� ���� ����
            fade += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    // �ؽ�Ʈ ���̵� �ƿ� �ڷ�ƾ
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

    // ��ư ���̵� �� �ڷ�ƾ
    public IEnumerator ButtonFadeInCoroutine(Button a, int b)
    {
        float fade = 0;
        buttonFade[b].gameObject.SetActive(true); // ��ư ���̱�
        while (fade < 1)
        {
            a.GetComponent<Image>().color = new Color(1, 1, 1, fade); // ��� �̹���
            a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, fade); // �ؽ�Ʈ
            fade += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        a.interactable = true;
    }

    // ��ư ���̵� �ƿ� �ڷ�ƾ
    public IEnumerator ButtonFadeOutCoroutine(Button a, int b)
    {
        float fade = 1;
        a.interactable = false; // ���� �� ����
        while (fade > 0)
        {
            a.GetComponent<Image>().color = new Color(1, 1, 1, fade);
            a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, fade);
            fade -= 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        buttonFade[b].gameObject.SetActive(false); // ���̵� �ƿ� �� ��Ȱ��ȭ
    }

    // �̹���(��: �г�) ���̵� ��
    public IEnumerator ObjectFadeInCoroutine(Image a)
    {
        float fade = 0;
        a.enabled = true; // �̹��� Ȱ��ȭ
        while (fade < 1)
        {
            a.color = new Color(1, 1, 1, fade);
            fade += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    // �̹��� ���̵� �ƿ�
    public IEnumerator ObjectFadeOutCoroutine(Image a)
    {
        float fade = 1;
        while (fade > 0)
        {
            a.color = new Color(1, 1, 1, fade);
            fade -= 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        a.enabled = false; // ������ ������� ��Ȱ��ȭ
    }
}
