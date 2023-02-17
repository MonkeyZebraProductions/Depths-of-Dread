using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameaSensitivity : MonoBehaviour
{
    private Slider slider;

    public float Sensitivity;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        Sensitivity = slider.value/2;
    }
}
