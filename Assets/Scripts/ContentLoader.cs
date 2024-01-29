using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;


public class ContentLoader : MonoBehaviour
{
    // UI
    [SerializeField] private Button ScrollDownButton;
    [SerializeField] private Button ScrollUpButton;

    [SerializeField] private RectTransform contentPanelTransform;

    [SerializeField] private string[] tiktokFileNames;
    private List<int> tiktokScrollOrder = new List<int>();
    [SerializeField] private VideoPlayer tikTokScreen;

    [SerializeField] private RenderTexture texture;
    [SerializeField] private int nbOfGames = 0;
    private List<int> scrollOrder = new List<int>();
    private int scrollIndex = 0;


    private bool isScrolling = false;
    void Start()
    {
        scrollOrder.Add(Random.Range(1, nbOfGames + 1));
        tiktokScrollOrder.Add(Random.Range(0, tiktokFileNames.Length));
        scrollIndex = 0;
        LoadGame();
    }

    private void OnEnable()
    {
        ScrollDownButton.onClick.AddListener(ScrollDown);
        ScrollUpButton.onClick.AddListener(ScrollUp);
    }

    private void OnDisable()
    {
        ScrollDownButton.onClick.RemoveListener(ScrollDown);
        ScrollUpButton.onClick.RemoveListener(ScrollUp);
    }

    Vector2 mouseDelta = Vector2.zero;
    Vector2 mousePosLastFrame = Vector2.zero;
    private void Update()
    {
        mouseDelta = (Vector2)Input.mousePosition - mousePosLastFrame;
        mousePosLastFrame = Input.mousePosition;
    }

    float dragAmtY = 0;
    public void OnDrag()
    {
        if (isScrolling) return;
        dragAmtY += mouseDelta.y;
        contentPanelTransform.anchoredPosition = new Vector2(0, dragAmtY);

        if (Mathf.Abs(dragAmtY) > contentPanelTransform.rect.size.y / 4)
        {            
            if (dragAmtY > 0)
                ScrollDown();
            else
                ScrollUp();

            dragAmtY = 0;
        }

    }

    public async void OnDragEnd()
    {
        if (isScrolling) return;
        dragAmtY = 0;
        while (Vector2.Distance(contentPanelTransform.anchoredPosition, Vector2.zero) > 10)
        {
            contentPanelTransform.anchoredPosition = Vector2.Lerp(contentPanelTransform.anchoredPosition, Vector2.zero, Time.deltaTime * 5f);
            await Task.Yield();
        }

        contentPanelTransform.anchoredPosition = Vector2.zero;
    }

    private async Task LoadGame()
    {
        string dir = System.IO.Path.Combine(Application.streamingAssetsPath, tiktokFileNames[tiktokScrollOrder[scrollIndex]]);

        tikTokScreen.url = dir;         

        AsyncOperation op = SceneManager.LoadSceneAsync(scrollOrder[scrollIndex], LoadSceneMode.Additive);
        while (!op.isDone)
        {
            await Task.Yield();
        }

        Scene scene = SceneManager.GetSceneByBuildIndex(scrollOrder[scrollIndex]);
        foreach (GameObject go in scene.GetRootGameObjects())
        {
            EventSystem sys = go.GetComponentInChildren<EventSystem>();
            if (sys != null)
            {
                Destroy(sys.gameObject);
                break;
            }
        }

        foreach(GameObject go in scene.GetRootGameObjects())
        {
            if (go.GetComponent<Camera>() != null) {
                go.GetComponent<Camera>().targetTexture = texture;
                break;
            }           

            if (go.GetComponentInChildren<Camera>() != null)
            {
                go.GetComponentInChildren<Camera>().targetTexture = texture;
                break;
            }
        }

        
        tikTokScreen.Play();
    }

    private async Task UnLoadCurrentGame()
    {
        AsyncOperation op = SceneManager.UnloadSceneAsync(scrollOrder[scrollIndex]);
        while (!op.isDone)
        {
            await Task.Yield();
        }

        tikTokScreen.Stop();
        tikTokScreen.clip = null;
        tikTokScreen.url = null;
    }


    public async void ScrollDown()
    {
        if (isScrolling) return;
        isScrolling = true;

        Vector2 basePos = Vector2.zero;
        Vector2 topPos = basePos + Vector2.up * contentPanelTransform.rect.height;
        Vector2 botPos = basePos + Vector2.down * contentPanelTransform.rect.height;

        while (Vector2.Distance(contentPanelTransform.anchoredPosition, topPos) > 10)
        {
            contentPanelTransform.anchoredPosition = Vector2.Lerp(contentPanelTransform.anchoredPosition, topPos, Time.deltaTime * 5f);
            await Task.Yield();
        }

        await UnLoadCurrentGame();
        scrollIndex++;
        if (scrollIndex == scrollOrder.Count)
        {
            int res = Random.Range(1, nbOfGames + 1);
            int tries = 0;
            while ((scrollOrder.Contains(res) && scrollOrder.Count < nbOfGames) || res == scrollOrder[scrollIndex - 1])
            {
                res = Random.Range(1, nbOfGames + 1);
                tries++;
                if (tries > 1000) break;
            }
            scrollOrder.Add(res);

            int vid = Random.Range(0, tiktokFileNames.Length);
            tries = 0;
            while ((tiktokScrollOrder.Contains(vid) && scrollOrder.Count < tiktokFileNames.Length) || tiktokScrollOrder[scrollIndex - 1] == vid)
            {
                vid = Random.Range(0, tiktokFileNames.Length);    
                tries++;
                if (tries > 1000) break;
            }
            tiktokScrollOrder.Add(vid);
        }

        await LoadGame();
        contentPanelTransform.anchoredPosition = botPos;
        while (Vector2.Distance(contentPanelTransform.anchoredPosition, basePos) > 10)
        {
            contentPanelTransform.anchoredPosition = Vector2.Lerp(contentPanelTransform.anchoredPosition, basePos, Time.deltaTime * 5f);
            await Task.Yield();
        }
        contentPanelTransform.anchoredPosition = basePos;

        isScrolling = false;
    }

    public async void ScrollUp()
    {
        if (scrollIndex == 0) return;
        if (isScrolling) return;
        isScrolling = true;

        Vector2 basePos = Vector2.zero;
        Vector2 topPos = basePos + Vector2.up * contentPanelTransform.rect.height;
        Vector2 botPos = basePos + Vector2.down * contentPanelTransform.rect.height;

        while (Vector2.Distance(contentPanelTransform.anchoredPosition, botPos) > 10)
        {
            contentPanelTransform.anchoredPosition = Vector2.Lerp(contentPanelTransform.anchoredPosition, botPos, Time.deltaTime * 5f);
            await Task.Yield();
        }        

        await UnLoadCurrentGame();
        scrollIndex--;

        await LoadGame();
        contentPanelTransform.anchoredPosition = topPos;
        while (Vector2.Distance(contentPanelTransform.anchoredPosition, basePos) > 10)
        {
            contentPanelTransform.anchoredPosition = Vector2.Lerp(contentPanelTransform.anchoredPosition, basePos, Time.deltaTime * 5f);
            await Task.Yield();
        }
        contentPanelTransform.anchoredPosition = basePos;

        isScrolling = false;
    }
}
