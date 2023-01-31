using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ListOfIcons", menuName = "List of icons")]
public class ListOfIcons : ScriptableObject
{
    [Tooltip("One collum of icons.")]
    public Sprite[] icons;
}
