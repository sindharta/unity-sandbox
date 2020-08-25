using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class IconViewerWindow : EditorWindow
{
    //読み込んだアイコンのキャッシュ
    Texture2D[] icons;

    //アイコン幅の最大値（動的計算）
    float maxIconWidth = -1;

    //アイコン名ラベル幅の最大値（動的計算）
    float maxLabelWidth = -1;

    //スクロールビューの位置
    Vector2 scrollpos;

    //フィルタ
    string filter;

    //各アイコンごとのリスト内Foldout状況
    Dictionary<string, bool> foldouts = new Dictionary<string, bool>();

    [MenuItem("Window/IconViewer")]
    public static void Open()
    {
        GetWindow<IconViewerWindow>(true, "IconViewer");
    }

    private void OnEnable()
    {
        //built-inアイコンを全て読み込み
        icons = Resources.FindObjectsOfTypeAll(typeof(Texture2D))
            .Where(x => AssetDatabase.GetAssetPath(x) == "Library/unity editor resources") //アイコンのAssetPathを取得すると全てこれ
            .Select(x => x.name)    //同一名で複数のアイコンが存在する場合があるので
            .Distinct()             //重複を除去
            .OrderBy(x => x)
            .Select(x => EditorGUIUtility.Load(x) as Texture2D)
            .Where(x => x)          //FontTextureなど、ロードできないものを除外
            .ToArray();

        //各項目のfoldoutを初期化
        foreach (var icon in icons)
        {
            foldouts[icon.name] = false;
        }
    }

    //GUIStyles
    GUIStyle blackLabel;
    GUIStyle whiteLabel;

    //Colors
    Color headerColor = new Color(0.11765f, 0.11765f, 0.11765f);
    Color elementBGColor0 = new Color(0.9f, 0.9f, 0.9f);
    Color elementBGColor1 = new Color(0.95f, 0.95f, 0.95f);
    Color elementBGColorConfig = new Color(0, 0, 0, 0.8f);
    Color headerSepalaterColor = new Color(1, 1, 1, 0.6f);
    Color elementSepalaterColor = new Color(0, 0, 0, 0.2f);
    Color foldedRectColor = new Color(0f, 0.2f, 0.2f);

    private void OnGUI()
    {
        var evt = Event.current;

        //GUIStyleのキャッシュ
        if(evt.type == EventType.Repaint)
        {
            if (blackLabel == null)
            {
                blackLabel = new GUIStyle(GUI.skin.label) { richText = true, alignment = TextAnchor.MiddleLeft, padding = new RectOffset(5, 0, 0, 0) };
            }
            if (whiteLabel == null)
            {
                whiteLabel = new GUIStyle(blackLabel) { fontSize = 14 };
                whiteLabel.normal.textColor = Color.white;
            }
        }

        //ラベル描画領域に必要な幅を計算
        if (maxLabelWidth <= 0)
        {
            maxLabelWidth = icons.Max(x => GUI.skin.label.CalcSize(new GUIContent(x.name)).x);
        }
        var labelWidth = maxLabelWidth + 10;

        //アイコン描画領域に必要な幅を計算
        RectOffset iconPadding = new RectOffset(10, 10, 4, 4);
        if (maxIconWidth <= 0)
        {
            maxIconWidth = icons.Max(x => x.width);
        }
        var iconRectWidth = maxIconWidth + iconPadding.left + iconPadding.right;

        //フィルタ
        filter = FilterField(filter, Repaint);

        //ヘッダ
        var headerHeight = 24;
        var headerRect = EditorGUILayout.GetControlRect(GUILayout.Height(headerHeight));
        var labelHeaderRect = new Rect(headerRect) { x = headerRect.x, width = labelWidth };
        var headerSepalaterRect = new Rect(labelHeaderRect.xMax, headerRect.y, 1, headerRect.height);
        var iconHeaderRect = new Rect(headerRect) { x = labelHeaderRect.xMax, width = iconRectWidth };
        var colorPickerRect = new Rect(iconHeaderRect.xMax + iconRectWidth - 100, headerRect.y + (headerHeight - EditorGUIUtility.singleLineHeight)/2, 100, EditorGUIUtility.singleLineHeight);
        var colorPickerLabelRect = new Rect(colorPickerRect) { x = colorPickerRect.x - 65 };
        EditorGUI.DrawRect(headerRect, headerColor);
        GUI.Label(labelHeaderRect, "Name", whiteLabel);
        EditorGUI.DrawRect(headerSepalaterRect, headerSepalaterColor);
        GUI.Label(iconHeaderRect, "Icon", whiteLabel);

        elementBGColorConfig = EditorGUI.ColorField(colorPickerRect, elementBGColorConfig);
        GUI.Label(colorPickerLabelRect, "BGColor", whiteLabel);

        //リスト
        var elementMinHeight = 20;
        var foldedRectLineHeight = 20;
        var foldedRectLinePaddingHeight = 2;
        var copyRectHeight = (foldedRectLineHeight + foldedRectLinePaddingHeight * 2) * 2;
        var copyRectPadding = 6;
        scrollpos = EditorGUILayout.BeginScrollView(scrollpos);
        int i = 0;
        foreach (var icon in icons)
        {
            //フィルタ内容に応じてスキップ
            if (!string.IsNullOrEmpty(filter) && !icon.name.ToLower().Contains(filter.ToLower())) continue;

            var iconRectHeight = Mathf.Max(elementMinHeight, icon.height + iconPadding.top + iconPadding.bottom);
            var elementRect = EditorGUILayout.GetControlRect(GUILayout.Width(labelWidth + iconRectWidth * 2), GUILayout.Height(iconRectHeight + (foldouts[icon.name] ? copyRectHeight+copyRectPadding : 0)));
            var elementMainRect = new Rect(elementRect) { height = iconRectHeight };
            var labelRect = new Rect(elementRect) { width = labelWidth, height = iconRectHeight };
            var sepalaterRect = new Rect(labelRect.xMax, elementRect.y, 1, elementRect.height);
            var iconRect = new Rect(labelRect.xMax + iconPadding.left, elementRect.y + iconPadding.top, icon.width, icon.height);
            var darkRect = new Rect(labelRect.xMax + iconRectWidth, elementRect.y, iconRectWidth, elementRect.height);
            var darkIconRect = new Rect(darkRect.x + iconPadding.left, elementRect.y + iconPadding.top, icon.width, icon.height);

            if (foldouts[icon.name])
            {
                EditorGUI.DrawRect(new Rect(elementRect.position - Vector2.one, elementRect.size + Vector2.one * 2), new Color(0, 0.7f, 0.7f));
            }
            EditorGUI.DrawRect(elementRect, i % 2 == 0 ? elementBGColor0 : elementBGColor1);
            EditorGUI.LabelField(labelRect, icon.name, blackLabel);
            EditorGUI.DrawRect(sepalaterRect, elementSepalaterColor);
            EditorGUI.DrawRect(darkRect, elementBGColorConfig);
            GUI.DrawTexture(iconRect, icon);
            GUI.DrawTexture(darkIconRect, icon);

            //コピー用フォーム・詳細情報
            if (foldouts[icon.name])
            {
                var foldedRect = new Rect(elementRect.x + copyRectPadding, elementRect.y + iconRectHeight, elementRect.width - copyRectPadding * 2, copyRectHeight);
                var copyButton1Rect = new Rect(foldedRect.x + 2, foldedRect.y + foldedRectLinePaddingHeight, 60, foldedRectLineHeight);
                var copyContent1Rect = new Rect(foldedRect.x + 70, foldedRect.y + foldedRectLinePaddingHeight, foldedRect.width - 80, foldedRectLineHeight);
                var copyButton2Rect = new Rect(foldedRect.x + 2, foldedRect.y + foldedRectLineHeight + foldedRectLinePaddingHeight * 3, 60, foldedRectLineHeight);
                var copyContent2Rect = new Rect(foldedRect.x + 70, foldedRect.y + foldedRectLineHeight + foldedRectLinePaddingHeight * 3, foldedRect.width - 80, foldedRectLineHeight);
                var widthRect = new Rect(foldedRect.xMax - 190, foldedRect.y + foldedRectLinePaddingHeight, 90, foldedRectLineHeight);
                var heightRect = new Rect(foldedRect.xMax - 190, foldedRect.y + foldedRectLineHeight + foldedRectLinePaddingHeight * 3, 90, foldedRectLineHeight);
                var exportRect = new Rect(foldedRect.xMax - 80, foldedRect.y + foldedRectLineHeight/2 + foldedRectLinePaddingHeight * 2, 70, foldedRectLineHeight);
                EditorGUI.DrawRect(new Rect(foldedRect.position - Vector2.one, foldedRect.size + Vector2.one * 2), new Color(1,1,1,0.5f));
                EditorGUI.DrawRect(foldedRect, foldedRectColor);
                if (GUI.Button(copyButton1Rect, "Copy"))
                {
                    EditorGUIUtility.systemCopyBuffer = icon.name;
                }
                GUI.Label(copyContent1Rect, icon.name, whiteLabel);
                if (GUI.Button(copyButton2Rect, "Copy"))
                {
                    EditorGUIUtility.systemCopyBuffer = $"(Texture2D)EditorGUIUtility.Load(\"{icon.name}\")";
                }
                GUI.Label(copyContent2Rect, $"(<color=#4ec9b0>Texture2D</color>)<color=#4ec9b0>EditorGUIUtility</color>.<color=#dcdcaa>Load</color>(<color=#d69d85>\"{icon.name}\"</color>)", whiteLabel);
                whiteLabel.alignment = TextAnchor.MiddleRight;
                GUI.Label(widthRect, $"width = {icon.width}", whiteLabel);
                GUI.Label(heightRect, $"height = {icon.height}", whiteLabel);
                if(GUI.Button(exportRect, "Export"))
                {
                    var path = EditorUtility.SaveFilePanel($"Export the icon [{icon.name}]", Application.dataPath, icon.name, "png");
                    if (!string.IsNullOrEmpty(path))
                    {
                        var output = new Texture2D(icon.width, icon.height, icon.format, icon.mipmapCount > 1);
                        Graphics.CopyTexture(icon, output);
                        System.IO.File.WriteAllBytes(path, output.EncodeToPNG());
                        if (path.StartsWith(Application.dataPath))
                        {
                            AssetDatabase.Refresh();
                            var assetPath = path.Replace(Application.dataPath, "Assets");
                            var importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                            importer.alphaIsTransparency = true;
                            AssetDatabase.ImportAsset(assetPath);
                        }
                    }
                }

                whiteLabel.alignment = TextAnchor.MiddleLeft;
            }

            //クリックでコピー用フォームを開閉
            if (evt.type == EventType.MouseDown && evt.button == 0 && elementMainRect.Contains(evt.mousePosition))
            {
                foldouts[icon.name] = !foldouts[icon.name];
                Repaint();
            }
            i++;
        }
        EditorGUILayout.EndScrollView();
    }

    /// <summary>
    /// フィルタ入力用フィールド。repaintCallbackにはEditorWindowのRepaintやそれに準ずるものを渡すこと。
    /// </summary>
    public static string FilterField(string filter, System.Action repaintCallback, string controlName = "__FilterField__")
    {
        var evt = Event.current;
        using (new EditorGUILayout.HorizontalScope())
        {
            //入力中にEnterキーでフォーカスを外す
            if (GUI.GetNameOfFocusedControl() == controlName && evt.type == EventType.KeyDown && evt.keyCode == KeyCode.Return)
            {
                EditorGUI.FocusTextInControl("");
                repaintCallback?.Invoke();
            }

            //入力欄
            GUI.SetNextControlName(controlName);
            filter = GUILayout.TextField(filter, "SearchTextField");
            var lastrect = GUILayoutUtility.GetLastRect();

            //入力欄以外でクリックされたらフォーカスを外す
            if (evt.type == EventType.MouseDown && evt.button == 0 && !lastrect.Contains(evt.mousePosition))
            {
                EditorGUI.FocusTextInControl("");
                repaintCallback?.Invoke();
            }

            //クリアボタン
            using (new EditorGUI.DisabledGroupScope(string.IsNullOrEmpty(filter)))
            {
                if (GUILayout.Button("Clear", "SearchCancelButton"))
                {
                    filter = "";
                }
            }
        }
        return filter;
    }
}