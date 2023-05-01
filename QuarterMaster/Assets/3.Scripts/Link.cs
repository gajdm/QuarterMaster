using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    public void LinkURL(string url)
    {
        Application.OpenURL(url);
    }
}
