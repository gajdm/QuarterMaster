using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoSuchStudio.UI.Highlight;
[CreateAssetMenu(fileName = "New Tutorial Item", menuName = "Tutorial item")]
public class TutorialItem : ScriptableObject
{
    public bool uiBool;
    public string itemToHighlight;
    [TextArea(5,10)]public string description;
    public Options itemOptions;
}
