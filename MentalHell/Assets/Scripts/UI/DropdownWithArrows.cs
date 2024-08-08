using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownWithArrows : MonoBehaviour
{
    public Button leftArrow;
    public Button rightArrow;
    public TMP_Dropdown dropdown;
    private int currentIndex;

    void Start()
    {
        currentIndex = 0;
        UpdateDropdown();

        leftArrow.onClick.AddListener(PreviousOption);
        rightArrow.onClick.AddListener(NextOption);
    }

    void PreviousOption()
    {
        currentIndex = (currentIndex - 1 + dropdown.options.Count) % dropdown.options.Count;
        UpdateDropdown();
    }

    void NextOption()
    {
        currentIndex = (currentIndex + 1) % dropdown.options.Count;
        UpdateDropdown();
    }

    void UpdateDropdown()
    {
        dropdown.value = currentIndex;
    }
}
