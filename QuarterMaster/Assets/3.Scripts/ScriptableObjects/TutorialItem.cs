using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tutorial Item", menuName = "Tutorial item")]
public class TutorialItem : ScriptableObject
{
    public string itemToHighlight;
    [TextArea(5,10)]public string text;
}
