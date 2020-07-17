﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A component script for the car game object.
/// It updates the gas level of the car and
/// make changes to the gas indicator image accordingly. 
/// </summary>
public class Gas : MonoBehaviour
{
    public GameObject noGasText;
    public GameObject gasLevelImageObj;

    private const float FullGasolineLevel = 5.0f;
    private const float Mpg = 0.1f;
    private const float LowVolumeCoefficient = 0.2f;
    private const float MediumVolumeCoefficient = 0.4f;
    private const float HighVolumeCoefficient = 0.6f;
    private readonly Color32 _darkRedColor = new Color32(196, 92, 29, 255);
    private readonly Color32 _orangeColor = new Color32(255, 196, 0, 255);
    private readonly Color32 _lightGreenColor = new Color32(125, 210, 76, 255);
    private readonly Color32 _greenColor = new Color32(94, 201, 93, 255);
    private float _gasLevel = 5.0f;
    private float _totalDistanceDriven = 0f;
    private Image _gasLevelImage;

    public float GasLevel => _gasLevel;

    public static float FullGasLevel => FullGasolineLevel;


    // Start is called before the first frame update.
    private void Start()
    {
        _gasLevelImage = gasLevelImageObj.GetComponent<Image>();
    }

    // Check if there is gas left in the tank.
    public bool HasGas()
    {
        if (GasLevel > 0)
        {
            return true;
        }
        else
        {
            noGasText.SetActive(true);
            return false;
        }
    }

    // Reset the gas level when fill the gas.
    public void FilledGas()
    {
        _gasLevel = FullGasLevel;
        noGasText.SetActive(false);
    }
    

    // Set the gas level bar length and color according to the distance the car has traveled.
    public void SetGasLevel(float curTotalDistanceDriven)
    {
        // Return if no gas left.
        if (GasLevel <= 0) return;
        var consumedGas = (curTotalDistanceDriven - _totalDistanceDriven) * Mpg;
        _gasLevel = GasLevel - consumedGas;
        SetGasLevelHelper(_gasLevelImage, gasLevelImageObj);
        // Update the total distance driven.
        _totalDistanceDriven = curTotalDistanceDriven;
    }


    public void SetGasLevelHelper(Image gasLevelImage, GameObject gasLevelImageObject)
    {
        gasLevelImageObject.transform.localScale = new Vector3(GasLevel / FullGasLevel, 1, 1);

        // Change the gas bar color according to the bar length.
        if (GasLevel < LowVolumeCoefficient * FullGasLevel)
        {
            gasLevelImage.color = _darkRedColor;
        }
        else if (GasLevel < MediumVolumeCoefficient * FullGasLevel)
        {
            gasLevelImage.color = _orangeColor;
        }
        else if (GasLevel < HighVolumeCoefficient * FullGasLevel)
        {
            gasLevelImage.color = _lightGreenColor;
        }
        else
        {
            gasLevelImage.color = _greenColor;
        }
    }
}