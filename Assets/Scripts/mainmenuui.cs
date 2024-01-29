using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainmenuui : MonoBehaviour
{
    public GameObject captionPanel;
    public void ExitGame()
    {
        Application.Quit();
    }
    public void CaptionToggle()
    {
        captionPanel.SetActive(!captionPanel.activeInHierarchy);
    }
}
