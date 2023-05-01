using UnityEngine;
using UnityEngine.UI;

public class RandomNumber : MonoBehaviour
{
    public Text numberText;
    public void Start()
    {
        int number = Random.Range(0,10000);
        numberText.text = number.ToString("d4");
    }
}
