using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
    public bool isClicked = false;

    public void OnClick()
    {
        isClicked = true;
    }
    public void OnNotClick()
    {
        isClicked = false;
    }
}
