using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    protected static SceneLoader _instance;
    public static SceneLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneLoader>();
                
                if (_instance == null)
                {
                    _instance = Create();
                    _instance.gameObject.SetActive(false);
                }
            }
            return _instance;
        }

        private set
        {
            _instance = value;
        }
    }

    string _loadSceneName;
    CanvasGroup _canvasGroup;  
    [SerializeField] Image _progressBar;

    public static SceneLoader Create()
    {
        var SceneLoaderPrefab = Resources.Load<SceneLoader>("SceneLoader/SceneLoader");
        return Instantiate(SceneLoaderPrefab);
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += LoadSceneEnd;
        _loadSceneName = sceneName;
        StartCoroutine(Load(sceneName));
    }

    private IEnumerator Load(string sceneName)
    {
        _progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (op.isDone == false)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;

            if (op.progress < 0.9f)
            {
                _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, op.progress, timer);
                if (_progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, 1f, timer);

                if (_progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == _loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;

        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 2;
            _canvasGroup.alpha = Mathf.Lerp(isFadeIn ? 0 : 1, isFadeIn ? 1 : 0, timer);
        }

        if (isFadeIn == false)
        {
            gameObject.SetActive(false);
        }
    }
}
