using System;
using UnityEngine;
using UnityEngine.UI;

public class Gas : MonoBehaviour
{
    public GameObject noGasText;
    public GameObject gasLevelImageObj;

    private float _mpg = 0.1f;
    private float _gasLevel = 5.0f;
    private float _fullGasLevel = 5.0f;
    private float _drivedDistanceBeforeLastFill = 0f;
    private float _totalDistanceDriven = 0f;
    private Image _gasLevelImage;

    // Start is called before the first frame update
    private void Start()
    {
        _gasLevelImage = gasLevelImageObj.GetComponent<Image>();
    }

    // check if there is gas left in the tank
    public bool HasGas()
    {
        if (_gasLevel > 0)
        {
            return true;
        }
        else
        {
            noGasText.SetActive(true);
            return false;
        }
    }

    // reset the gas level and distance when fill the gas
    public void FilledGas()
    {
        _gasLevel = _fullGasLevel;
        // set current distance to previous distance
        _drivedDistanceBeforeLastFill = _totalDistanceDriven;
        noGasText.SetActive(false);
    }

    public float GetFullGasLevel()
    {
        return _fullGasLevel;
    }

    public float GetGasLevel()
    {
        return _gasLevel;
    }

    // set the gas level bar length and color according to the distance the car has traveled
    public void SetGasLevel(float lengthPerCircle, int circleCount, float distance)
    {
        if (_gasLevel > 0)
        {
            _totalDistanceDriven = (lengthPerCircle * circleCount + distance);
            var consumedGas = (_totalDistanceDriven - _drivedDistanceBeforeLastFill)  * _mpg;
            _gasLevel = _fullGasLevel - consumedGas;
           SetGasLevelHelper(_gasLevelImage, gasLevelImageObj);
        }
    }

    
    public void SetGasLevelHelper(Image image, GameObject obj)
    {
        obj.transform.localScale = new Vector3(_gasLevel/_fullGasLevel, 1, 1);
            
        // set color change according to the bar length
        if (_gasLevel < 0.2 * _fullGasLevel)
        {
            image.color = new Color32(196,92,29,255 );
        } else if (_gasLevel < 0.4 * _fullGasLevel)
        {
            image.color = new Color32(255,196,0,255);
        } else if (_gasLevel < 0.6 * _fullGasLevel)
        {
            image.color = new Color32(125,210,76, 255);
        } else
        {
            image.color = new Color32(94, 201, 93, 255);
        }

    }
    
}
