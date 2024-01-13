using UnityEngine;
using TMPro;

/*

    Create Class for Longer Texts and All Localization

*/


[System.Serializable]
public class Localization
{

    public string name = "default";
    public string text = "default_text";

    public TMP_FontAsset font_asset;
    public FontStyles font_style;
    public float font_size = 11f;

    public VerticalAlignmentOptions vertical_alignment;
    public HorizontalAlignmentOptions horizontal_alignment;

}
