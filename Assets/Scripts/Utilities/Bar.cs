using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

    public Transform loadingBar;
    public Transform textPercent;


    [Range(0, 100)] public float currentPercent = 10;

    
    void Update()
    {
        loadingBar.GetComponent<Image>().fillAmount = currentPercent / 100;
        textPercent.GetComponent<Text>().text = ((int)currentPercent).ToString("F0") + "%";
    }

    public void SetPercent(int percent)
    {
        currentPercent = percent;
    }
}
