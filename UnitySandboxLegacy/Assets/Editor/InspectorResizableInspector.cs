using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InspectorResizableBox), true)]
public class ResizableBoxEditor : UnityEditor.Editor {

    public override void OnInspectorGUI()
    {
        // Draw the resizable box
        Rect boxRect = GUILayoutUtility.GetRect(
            m_boxSize.x,
            m_boxSize.y,
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

        // Draw a "grip" for visual feedback (three horizontal lines)
        Handles.BeginGUI();
        Handles.color = Color.gray;
        for (int i = -1; i <= 1; i++)
        {
            float y = handleRect.center.y + i * 3f;
            Handles.DrawLine(
                new Vector3(handleRect.xMin, y),
                new Vector3(handleRect.xMax, y)
            );
        }
        Handles.EndGUI();

        // Handle mouse events
        Event e = Event.current;
        if (e.type == EventType.MouseDown && handleRect.Contains(e.mousePosition))
        {
            m_dragging = true;
            m_dragStartMousePos = e.mousePosition;
            m_dragStartBoxHeight = m_boxSize.y;
            e.Use();
        }
        else if (e.type == EventType.MouseUp)
        {
            m_dragging = false;
        }
        else if (e.type == EventType.MouseDrag && m_dragging)
        {
            float deltaY = e.mousePosition.y - m_dragStartMousePos.y;
            m_boxSize.y = Mathf.Max(50, m_dragStartBoxHeight + deltaY); // Only height changes
            e.Use();
            Repaint();
        }

        // Optionally, show the current size
        EditorGUILayout.LabelField("Box Size", m_boxSize.ToString());

        // Draw the rest of the inspector as usual
        base.OnInspectorGUI();
    }
    
//----------------------------------------------------------------------------------------------------------------------

    private Vector2 m_boxSize = new Vector2(200, 100); // Initial width and height
    private bool m_dragging = false;
    private Vector2 m_dragStartMousePos;
    private float m_dragStartBoxHeight;

}
