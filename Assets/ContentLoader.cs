using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContentLoader : MonoBehaviour
{
    // UI
    [SerializeField] private Button ScrollDownButton;
    [SerializeField] private Button ScrollUpButton;

    [SerializeField] private RectTransform contentPanelTransform;


    [SerializeField] private RenderTexture texture;
    [SerializeField] private int nbOfGames = 0;
    private List<int> scrollOrder = new List<int>();
    private int scrollIndex = 0;

    void Start()
    {
        scrollOrder.Add(Random.Range(1, nbOfGames + 1));
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnLoadCurrentGame();
        }
    }

    private async Task LoadGame()
    {

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
                return;
            }

            

            if (go.GetComponentInChildren<Camera>() != null)
            {
                go.GetComponentInChildren<Camera>().targetTexture = texture;
                return;
            }
        }   
    }

    private async Task UnLoadCurrentGame()
    {
        AsyncOperation op = SceneManager.UnloadSceneAsync(scrollOrder[scrollIndex]);
        while (!op.isDone)
        {
            await Task.Yield();
        }
    }


    public async void ScrollDown()
    {
        Vector2 basePos = Vector2.zero;
        Vector2 topPos = basePos + Vector2.up * contentPanelTransform.rect.height;
        Vector2 botPos = basePos + Vector2.down * contentPanelTransform.rect.height;

        while (Vector2.Distance(contentPanelTransform.anchoredPosition, topPos) > 50)
        {
            contentPanelTransform.anchoredPosition = Vector2.Lerp(contentPanelTransform.anchoredPosition, topPos, Time.deltaTime * 5f);
            await Task.Yield();
        }   

        await UnLoadCurrentGame();
        scrollIndex++;
        if (scrollIndex == scrollOrder.Count)
        {
            scrollOrder.Add(Random.Range(1, nbOfGames + 1));
        }

        contentPanelTransform.anchoredPosition = botPos;
        while (Vector2.Distance(contentPanelTransform.anchoredPosition, basePos) > 50)
        {
            contentPanelTransform.anchoredPosition = Vector2.Lerp(contentPanelTransform.anchoredPosition, basePos, Time.deltaTime * 5f);
            await Task.Yield();
        }
        contentPanelTransform.anchoredPosition = basePos;

        await LoadGame();
    }

    public async void ScrollUp()
    {
        if (scrollIndex == 0) return;

        Vector2 basePos = Vector2.zero;
        Vector2 topPos = basePos + Vector2.up * contentPanelTransform.rect.height;
        Vector2 botPos = basePos + Vector2.down * contentPanelTransform.rect.height;

        while (Vector2.Distance(contentPanelTransform.anchoredPosition, botPos) > 50)
        {
            contentPanelTransform.anchoredPosition = Vector2.Lerp(contentPanelTransform.anchoredPosition, botPos, Time.deltaTime * 5f);
            await Task.Yield();
        }        

        await UnLoadCurrentGame();
        scrollIndex--;

        contentPanelTransform.anchoredPosition = topPos;
        while (Vector2.Distance(contentPanelTransform.anchoredPosition, basePos) > 50)
        {
            contentPanelTransform.anchoredPosition = Vector2.Lerp(contentPanelTransform.anchoredPosition, basePos, Time.deltaTime * 5f);
            await Task.Yield();
        }
        contentPanelTransform.anchoredPosition = basePos;
        await LoadGame();
    }
}
