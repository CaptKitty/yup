using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputMenuScript : MonoBehaviour
{

    public InputField mainInputField;

    public InputField nameInputField;

    public GameObject weaponsmenuscript;

    public int numbers;
    
    public string names;

    public void Start()
    {
        numbers = weaponsmenuscript.GetComponent<WeaponMenuScript>().Numbers;
        names = weaponsmenuscript.GetComponent<WeaponMenuScript>().names;
    }
    public void OnValueChangeNumbers()
    {
        numbers = Convert.ToInt32(mainInputField.text);
        if(numbers <= 0)
        {
            numbers = 0;
        }
        weaponsmenuscript.GetComponent<WeaponMenuScript>().Numbers = numbers;
    }

    public void OnValueChangeName()
    {
        names = nameInputField.text;
        weaponsmenuscript.GetComponent<WeaponMenuScript>().name = name;
    }
}
