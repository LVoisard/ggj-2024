using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContentLoader : MonoBehaviour
{
    private GameObject currentGame = null;

    [SerializeField] private RenderTexture text;
    // Start is called before the first frame update
    async void Start()
    {
        print("joe 0");
        AsyncOperation op = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        while (!op.isDone)
        {
            await Task.Yield();
        }

        print("joe 1");
        currentGame = GameObject.FindGameObjectsWithTag("EndlessDriver").First();
        print("joe 2");
        currentGame.transform.parent = transform;

        currentGame.GetComponentInChildren<Camera>().targetTexture = text;
        print("joe 3");

    }
}
