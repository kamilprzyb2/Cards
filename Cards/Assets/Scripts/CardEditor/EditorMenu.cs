using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMenu : MonoBehaviour
{
    public RectTransform CardEditor;
    public RectTransform GroupEditor;
    public RectTransform IconEditor;

    void Start()
    {
        ChangeMenu(1);
    }

    public void ChangeMenu(int index)
    {
        switch (index)
        {
            case 1:
                {
                    CardEditor.localPosition = new Vector2(CardEditor.localPosition.x, 0);
                    GroupEditor.localPosition = new Vector2(GroupEditor.localPosition.x, 500);
                    IconEditor.localPosition = new Vector2(IconEditor.localPosition.x, 500);
                    break;
                }
            case 2:
                {
                    GroupEditor.localPosition = new Vector2(GroupEditor.localPosition.x, 0);
                    CardEditor.localPosition = new Vector2(CardEditor.localPosition.x, 500);
                    IconEditor.localPosition = new Vector2(IconEditor.localPosition.x, 500);
                    break;
                }
            case 3:
                {
                    IconEditor.localPosition = new Vector2(IconEditor.localPosition.x, 0);
                    CardEditor.localPosition = new Vector2(CardEditor.localPosition.x, 500);
                    GroupEditor.localPosition = new Vector2(GroupEditor.localPosition.x, 500);
                    break;
                }
            default:
                {
                    Debug.LogError("Unknown menu index " + index);
                    break;
                }
        }
    }
}
