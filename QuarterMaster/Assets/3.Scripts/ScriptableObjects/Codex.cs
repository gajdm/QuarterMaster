using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Codex", menuName = "Codex")]
public class Codex : ScriptableObject
{
    [SerializeField] private string header;
    [SerializeField] private Sprite visual;
    [TextArea(5,10)]
    [SerializeField] private string mainText;

    public string GetHeader()
    { return header; }
    public string GetMainText()
    { return mainText; }
    public Sprite GetImage()
    { return visual; }

}
