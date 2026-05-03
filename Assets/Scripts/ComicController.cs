using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ComicController : MonoBehaviour
{
    [Header("Pages")]
    public GameObject page1;
    public GameObject page2;

    [Header("Frames Page 1")]
    public GameObject[] page1Frames;
    public GameObject page1Arrow;
    public FrameAnimType[] page1AnimTypes;

    [Header("Frames Page 2")]
    public GameObject[] page2Frames;
    public GameObject page2Arrow;
    public FrameAnimType[] page2AnimTypes;

    int currentFrame = 0;
    int currentPage = 1;

    void Start()
    {
        ShowFirstFrame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Next();
        }
    }

    void Next()
    {
        if (currentPage == 1)
        {
            HandlePage(page1Frames, page1Arrow, page1AnimTypes);
        }
        else
        {
            HandlePage(page2Frames, page2Arrow, page2AnimTypes);
        }
    }

    void HandlePage(GameObject[] frames, GameObject arrow, FrameAnimType[] animTypes)
    {
        if (currentFrame < frames.Length)
        {
            StartCoroutine(ShowFrame(frames[currentFrame], animTypes[currentFrame]));
            currentFrame++;
        }
        else
        {
            arrow.SetActive(true);
        }
    }

    public void GoToPage2()
    {
        page1.SetActive(false);
        page2.SetActive(true);

        currentPage = 2;
        currentFrame = 0;

        foreach (var frame in page2Frames)
        {
            frame.SetActive(false);
        }

        page2Arrow.SetActive(false);
        StartCoroutine(ShowFrame(page2Frames[0], page2AnimTypes[0]));
        currentFrame = 1;
    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
    }

    IEnumerator ShowFrame(GameObject frame, FrameAnimType type)
    {
        frame.SetActive(true);

        RectTransform rt = frame.GetComponent<RectTransform>();
        CanvasGroup cg = frame.GetComponent<CanvasGroup>();

        Vector3 startPos = rt.anchoredPosition;
        Vector3 endPos = rt.anchoredPosition;

        Vector3 startScale = Vector3.one;
        float startRotation = 0f;

        cg.alpha = 0f;

        switch (type)
        {
            case FrameAnimType.Fade:
                break;

            case FrameAnimType.SlideRight:
                startPos += new Vector3(200f, 0, 0);
                break;

            case FrameAnimType.SlideUp:
                startPos += new Vector3(0, -200f, 0);
                break;

            case FrameAnimType.Scale:
                startScale = Vector3.one * 1.2f;
                break;

            case FrameAnimType.Rotate:
                startRotation = 10f;
                break;

            case FrameAnimType.Pop:
                startScale = Vector3.one * 0.8f;
                break;
        }

        rt.anchoredPosition = startPos;
        rt.localScale = startScale;
        rt.rotation = Quaternion.Euler(0, 0, startRotation);

        float t = 0f;
        float duration = 0.25f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float k = Mathf.SmoothStep(0f, 1f, t / duration);

            cg.alpha = Mathf.Lerp(0f, 1f, k);
            rt.anchoredPosition = Vector3.Lerp(startPos, endPos, k);
            rt.localScale = Vector3.Lerp(startScale, Vector3.one, k);
            rt.rotation = Quaternion.Lerp(
                Quaternion.Euler(0, 0, startRotation),
                Quaternion.identity,
                k
            );

            yield return null;
        }

        cg.alpha = 1f;
        rt.anchoredPosition = endPos;
        rt.localScale = Vector3.one;
        rt.rotation = Quaternion.identity;
    }

    public enum FrameAnimType
    {
        Fade,
        SlideRight,
        SlideUp,
        Scale,
        Rotate,
        Pop
    }

    void ShowFirstFrame()
    {
        if (currentPage == 1 && page1Frames.Length > 0)
        {
            StartCoroutine(ShowFrame(page1Frames[0], page1AnimTypes[0]));
            currentFrame = 1;
        }
    }
}