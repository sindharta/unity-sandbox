using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UIElementsWindow : EditorWindow
{
    [MenuItem("UIElements/Show Window _%#T")] //Shortcut for CTRL + SHIFT + T
    public static void ShowWindow() {
        // Opens the window, otherwise focuses it if it’s already open.
        var window = GetWindow<UIElementsWindow>();

        // Adds a title to the window.
        window.titleContent = new GUIContent("UIElements Window");

        // Sets a minimum size to the window.
        window.minSize = new Vector2(250, 50);
    }
    
//---------------------------------------------------------------------------------------------------------------------

    void OnEnable() {        
        m_root = rootVisualElement;

        // Associates a stylesheet to our root. Thanks to inheritance, all root’s
        // children will have access to it.
        m_root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/USS/UIElementsStyle.uss"));

        // Loads and clones our VisualTree (eg. our UXML structure) inside the root.
        VisualTreeAsset quickToolVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UXML/UIElementsWindow.uxml");
        quickToolVisualTree.CloneTree(m_root);

        // Queries all the buttons (via type) in our root and passes them
        // in the SetupButton method.
        UQueryBuilder<Button> toolButtons = m_root.Query<Button>();
        toolButtons.ForEach(SetupButton);

    }

//---------------------------------------------------------------------------------------------------------------------

    void SetupButton(Button button)  {
        // Reference to the VisualElement inside the button that serves
        // as the button’s icon.
        VisualElement buttonIcon = button.Q(className: "uielement-button-icon");

        // Icon’s path in our project.
        string iconPath = "Assets/Editor/Icons/" + button.parent.name + ".png";

        // Loads the actual asset from the above path.
        Texture2D iconAsset = AssetDatabase.LoadAssetAtPath<Texture2D>(iconPath);

        // Applies the above asset as a background image for the icon.
        buttonIcon.style.backgroundImage = iconAsset;

        // Instantiates our primitive object on a left click.
        button.clickable.clicked += () => CreatePrimitiveObject(button.parent.name);

        // Sets a basic tooltip to the button itself.
        button.tooltip = button.parent.name;
    }

//---------------------------------------------------------------------------------------------------------------------

    //[TODO-sin: 2019-10-24] Move to another package
    private void CreatePrimitiveObject(string primitiveTypeName) {    
        PrimitiveType pt = (PrimitiveType) Enum.Parse (typeof(PrimitiveType), primitiveTypeName, true);
        GameObject go = ObjectFactory.CreatePrimitive(pt);
        go.transform.position = Vector3.zero;
    }

//---------------------------------------------------------------------------------------------------------------------

    VisualElement m_root = null;

}
