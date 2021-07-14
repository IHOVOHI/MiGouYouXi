using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuanMianPing : MonoBehaviour
{

    public RectTransform rectTransform1;
    public RectTransform rectTransform2;
    public Toggle toggle;

    public void ShiPei() {
        if (toggle.isOn)
        {
            rectTransform1.position += new Vector3(100, 0, 0);
            rectTransform2.position += new Vector3(100, 0, 0);
        }
        else
        {
            rectTransform1.position -= new Vector3(100, 0, 0);
            rectTransform2.position -= new Vector3(100, 0, 0);
        }
    }
}
