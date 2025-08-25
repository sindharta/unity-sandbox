using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InspectorResizableBox), true)]
public class ResizableBoxEditor : UnityEditor.Editor
{
    // Change these to static if you want the size shared across all inspectors of this type
    private Vector2 boxSize = new Vector2(200, 100);
    private bool dragging = false;
    private Vector2 dragStartMousePos;
    private Vector2 dragStartBoxSize;

    public override void OnInspectorGUI()
    {
        // Draw the resizable box
        Rect boxRect = GUILayoutUtility.GetRect(
            boxSize.x,
            boxSize.y,
            GUILayout.ExpandWidth(false),
            GUILayout.ExpandHeight(false)
        );
        GUI.Box(boxRect, "Resizable Box");

        // Draw the resize handle along the entire bottom edge
        float handleHeight = 8f;
        Rect handleRect = new Rect(
            boxRect.xMin,
            boxRect.yMax - handleHeight / 2f,
            boxRect.width,
            handleHeight
        );
        EditorGUIUtility.AddCursorRect(handleRect, MouseCursor.ResizeVertical);

        // Handle mouse events
        Event e = Event.current;
        if (e.type == EventType.MouseDown && handleRect.Contains(e.mousePosition))
        {
            dragging = true;
            dragStartMousePos = e.mousePosition;
            dragStartBoxSize = boxSize;
            e.Use();
        }
        else if (e.type == EventType.MouseUp)
        {
            dragging = false;
        }
        else if (e.type == EventType.MouseDrag && dragging)
        {
            Vector2 delta = e.mousePosition - dragStartMousePos;
            boxSize = new Vector2(
//                Mathf.Max(50, dragStartBoxSize.x + delta.x),
                Mathf.Max(50, dragStartBoxSize.x),
                Mathf.Max(50, dragStartBoxSize.y + delta.y)
            );
            e.Use();
            Repaint();
        }

        // Optionally, show the current size
        EditorGUILayout.LabelField("Box Size", boxSize.ToString());

        // Draw the rest of the inspector as usual
        base.OnInspectorGUI();
    }
}
