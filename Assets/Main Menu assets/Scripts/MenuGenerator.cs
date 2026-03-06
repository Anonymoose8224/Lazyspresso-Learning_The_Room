
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;

public class MenuGenerator : EditorWindow
{
    private string menuName = "Menu";
    private GameObject targetCanvas;
    private List<string> menuItems = new List<string>();
    private string newMenuItem = "";
    private bool useTextMeshPro = false;
    private bool enableArrowNavigation = false;
    private Sprite buttonImage;
    private float buttonWidth = 200f;
    private float buttonHeight = 50f;
    private float textSize = 24f;
    private float spacing = 10f;
    private bool showAdjustments = false;
    private Color buttonNormalColor = Color.white;
    private float menuX = 0f;
    private float menuY = 0f;

    private enum MenuAnchor { Left, Right, TopLeft, TopRight, BottomLeft, BottomRight, Center }
    private MenuAnchor menuAnchor = MenuAnchor.Left;

    [MenuItem("Tools/Menu Generator")]
    public static void ShowWindow()
    {
        GetWindow<MenuGenerator>("Menu Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Menu Settings", EditorStyles.boldLabel);
        menuName = EditorGUILayout.TextField("Menu Name", menuName);
        targetCanvas = (GameObject)EditorGUILayout.ObjectField("Target Canvas", targetCanvas, typeof(GameObject), true);
        buttonImage = (Sprite)EditorGUILayout.ObjectField("Button Image", buttonImage, typeof(Sprite), false);

        useTextMeshPro = EditorGUILayout.Toggle("Use TextMeshPro", useTextMeshPro);
        enableArrowNavigation = EditorGUILayout.Toggle("Enable Arrow Navigation", enableArrowNavigation);

        GUILayout.Label("Button Settings", EditorStyles.boldLabel);
        buttonNormalColor = EditorGUILayout.ColorField("Button Normal Color", buttonNormalColor);

        GUILayout.Label("Menu Items", EditorStyles.boldLabel);
        for (int i = 0; i < menuItems.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            menuItems[i] = EditorGUILayout.TextField(menuItems[i]);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                menuItems.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }

        newMenuItem = EditorGUILayout.TextField("New Item", newMenuItem);
        if (GUILayout.Button("Add Item") && !string.IsNullOrEmpty(newMenuItem))
        {
            menuItems.Add(newMenuItem);
            newMenuItem = "";
            Repaint();
        }

        GUILayout.Label("Menu Position", EditorStyles.boldLabel);
        menuAnchor = (MenuAnchor)EditorGUILayout.EnumPopup("Anchor Position", menuAnchor);

        if (GUILayout.Button("Generate Menu"))
        {
            GenerateMenu();
            showAdjustments = true;
        }

        if (showAdjustments)
        {
            GUILayout.Label("Adjustments", EditorStyles.boldLabel);
            EditorGUI.BeginChangeCheck();
            buttonWidth = EditorGUILayout.FloatField("Button Width", buttonWidth);
            buttonHeight = EditorGUILayout.FloatField("Button Height", buttonHeight);
            textSize = EditorGUILayout.FloatField("Text Size", textSize);
            spacing = EditorGUILayout.FloatField("Spacing", spacing);
            menuX = EditorGUILayout.FloatField("Menu X Position", menuX);
            menuY = EditorGUILayout.FloatField("Menu Y Position", menuY);
            if (EditorGUI.EndChangeCheck())
            {
                GenerateMenu();
            }
        }
    }

    private void GenerateMenu()
    {
        if (targetCanvas == null)
        {
            Debug.LogError("Assign a Target Canvas to generate the menu.");
            return;
        }

        Transform existingPanel = targetCanvas.transform.Find(menuName);
        if (existingPanel)
        {
            DestroyImmediate(existingPanel.gameObject);
        }

        GameObject panel = new GameObject(menuName);
        panel.transform.SetParent(targetCanvas.transform, false);
        RectTransform panelRect = panel.AddComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(buttonWidth, menuItems.Count * (buttonHeight + spacing));

        MenuController menuController = panel.AddComponent<MenuController>();
        menuController.buttons = new List<Button>();
        menuController.useArrowNavigation = enableArrowNavigation;

        SetPanelPosition(panelRect);

        for (int i = 0; i < menuItems.Count; i++)
        {
            GameObject buttonObj = new GameObject(menuItems[i], typeof(RectTransform));
            buttonObj.transform.SetParent(panel.transform, false);

            Button button = buttonObj.AddComponent<Button>();
            menuController.buttons.Add(button);
            RectTransform rect = button.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(buttonWidth, buttonHeight);
            rect.anchoredPosition = new Vector2(0, -i * (buttonHeight + spacing));
            ColorBlock cb = button.colors;
            cb.normalColor = buttonNormalColor;
            button.colors = cb;

            Image img = buttonObj.AddComponent<Image>();
            img.sprite = buttonImage;
            img.color = Color.white;
            img.raycastTarget = true;

            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(buttonObj.transform, false);
            RectTransform textRect = textObj.AddComponent<RectTransform>();
            textRect.sizeDelta = rect.sizeDelta;

            if (useTextMeshPro)
            {
                TMP_Text tmpText = textObj.AddComponent<TextMeshProUGUI>();
                tmpText.text = menuItems[i];
                tmpText.fontSize = textSize;
                tmpText.alignment = TextAlignmentOptions.Center;
            }
            else
            {
                Text text = textObj.AddComponent<Text>();
                text.text = menuItems[i];
                text.fontSize = (int)textSize;
                text.alignment = TextAnchor.MiddleCenter;
            }

            panelRect.anchoredPosition = new Vector2(menuX, menuY);
        }

        if (menuController.buttons.Count > 0)
        {
            menuController.SelectFirstButton();
        }
    }

    private void SetPanelPosition(RectTransform panelRect)
    {
        Vector2 anchorMin = Vector2.zero;
        Vector2 anchorMax = Vector2.zero;
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        Vector2 positionOffset = Vector2.zero;

        switch (menuAnchor)
        {
            case MenuAnchor.Left:
                anchorMin = anchorMax = new Vector2(0, 0.5f);
                pivot = new Vector2(0, 0.5f);
                positionOffset = new Vector2(10, 0);
                break;
            case MenuAnchor.Right:
                anchorMin = anchorMax = new Vector2(1, 0.5f);
                pivot = new Vector2(1, 0.5f);
                positionOffset = new Vector2(-10, 0);
                break;
        }

        panelRect.anchorMin = anchorMin;
        panelRect.anchorMax = anchorMax;
        panelRect.pivot = pivot;
        panelRect.anchoredPosition = positionOffset;
    }
}

