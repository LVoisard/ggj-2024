using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        canvas.transform as RectTransform, Input.mousePosition,
        Camera.main,
        out Vector2 pos);
        transform.position = canvas.transform.TransformPoint(pos);
    }
}
