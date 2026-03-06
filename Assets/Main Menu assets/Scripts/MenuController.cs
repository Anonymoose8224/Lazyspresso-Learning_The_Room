
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();
    public bool useArrowNavigation = false;
    private int selectedIndex = 0;
    private int previousIndex = -1;
    private Dictionary<Button, Vector2> buttonOriginalSizes = new Dictionary<Button, Vector2>();
    private Coroutine currentAnimation;

    private void Start()
    {
        StartCoroutine(InitializeMenu());
    }

    private IEnumerator InitializeMenu()
    {
        yield return null; // Ensures buttons are assigned before proceeding

        if (buttons.Count == 0)
        {
            buttons.AddRange(GetComponentsInChildren<Button>());
        }

        StoreOriginalSizes();

        if (buttons.Count > 0)
        {
            SelectFirstButton();
            SetupMouseHover();
        }
        else
        {
            Debug.LogError("MenuController: No buttons found in the list!");
        }
    }

    private void Update()
    {
        if (useArrowNavigation)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SelectButton(-1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SelectButton(1);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Button clicked: " + buttons[selectedIndex].name);
            }
        }
    }

    private void StoreOriginalSizes()
    {
        buttonOriginalSizes.Clear();
        foreach (Button button in buttons)
        {
            if (button != null)
            {
                buttonOriginalSizes[button] = button.GetComponent<RectTransform>().sizeDelta;
            }
        }
    }

    public void SelectFirstButton()
    {
        if (buttons == null || buttons.Count == 0)
        {
            Debug.LogError("MenuController: No buttons available for selection!");
            return;
        }

        previousIndex = -1;
        selectedIndex = 0;

        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(buttons[selectedIndex].gameObject);
        else return;

        StartCoroutine(AnimateButtonTransition(null, buttons[selectedIndex]));
    }

    private void SelectButton(int direction)
    {
        if (buttons.Count == 0) return;

        previousIndex = selectedIndex;
        selectedIndex = Mathf.Clamp(selectedIndex + direction, 0, buttons.Count - 1);

        if (previousIndex != selectedIndex)
        {
            if (currentAnimation != null)
                StopCoroutine(currentAnimation);

            currentAnimation = StartCoroutine(AnimateButtonTransition(buttons[previousIndex], buttons[selectedIndex]));

            EventSystem.current.SetSelectedGameObject(buttons[selectedIndex].gameObject);
        }
    }

    private IEnumerator AnimateButtonTransition(Button oldButton, Button newButton)
    {
        if (newButton == null) yield break;

        RectTransform newRect = newButton.GetComponent<RectTransform>();
        RectTransform oldRect = oldButton != null ? oldButton.GetComponent<RectTransform>() : null;

        Vector2 originalNewSize = buttonOriginalSizes.ContainsKey(newButton) ? buttonOriginalSizes[newButton] : newRect.sizeDelta;
        Vector2 expandedSize = originalNewSize * 1.1f;

        Vector2 originalOldSize = oldRect != null && buttonOriginalSizes.ContainsKey(oldButton) ? buttonOriginalSizes[oldButton] : Vector2.zero;
        Vector2 shrinkSize = originalOldSize;

        float duration = 0.2f;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            newRect.sizeDelta = Vector2.Lerp(originalNewSize, expandedSize, elapsedTime / duration);
            if (oldRect != null)
                oldRect.sizeDelta = Vector2.Lerp(originalOldSize, shrinkSize, elapsedTime / duration);
            yield return null;
        }

        newRect.sizeDelta = expandedSize;
        if (oldRect != null)
            oldRect.sizeDelta = originalOldSize;
    }

    private void SetupMouseHover()
    {
        foreach (var button in buttons)
        {
            EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = button.gameObject.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => { OnButtonHover(button); });

            trigger.triggers.Add(entry);
        }
    }

    private void OnButtonHover(Button hoveredButton)
    {
        int index = buttons.IndexOf(hoveredButton);
        if (index != selectedIndex)
        {
            previousIndex = selectedIndex;
            selectedIndex = index;

            EventSystem.current.SetSelectedGameObject(null);

            StartCoroutine(AnimateButtonTransition(buttons[previousIndex], buttons[selectedIndex]));

            EventSystem.current.SetSelectedGameObject(buttons[selectedIndex].gameObject);
        }
    }
}
