using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void changeText(float speed, bool isTruck)
    {
        float s = isTruck ? speed * 3f : speed * 7f;
        text.text = Mathf.Clamp(Mathf.Round(s), 0f, 100000f) + " km/h";
    }

}
