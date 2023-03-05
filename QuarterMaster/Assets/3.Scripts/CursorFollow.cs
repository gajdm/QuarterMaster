using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorFollow : MonoBehaviour
{
    void Update()
    {
        this.gameObject.transform.position = Input.mousePosition.normalized*1000;
    }
}
