using UnityEngine;
using UnityEngine.UI;

// control gas change of the car
public class Gas : MonoBehaviour
{
    public GameObject noGasText;
    public GameObject gasLevelImageObj;

    private const float FullGasLevel = 5.0f;
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

    // reset the gas level and distance before last fill when fill the gas
    public void FilledGas()
    {
        _gasLevel = FullGasLevel;
        noGasText.SetActive(false);
    }


    public static float GetFullGasLevel()
    {
        return FullGasLevel;
    }

    public float GetGasLevel()
    {
        return _gasLevel;
    }

    // Set the gas level bar length and color according to the distance the car has traveled
    public void SetGasLevel(float curTotalDistanceDriven)
    {
        // return if no gas left
        if (_gasLevel <= 0) return;
        var consumedGas = (curTotalDistanceDriven - _totalDistanceDriven) * Mpg;
        _gasLevel -= consumedGas;
        SetGasLevelHelper(_gasLevelImage, gasLevelImageObj);
        // update the total distance driven
        _totalDistanceDriven = curTotalDistanceDriven;
    }


    public void SetGasLevelHelper(Image gasLevelImage, GameObject gasLevelImageObject)
    {
        gasLevelImageObject.transform.localScale = new Vector3(_gasLevel / FullGasLevel, 1, 1);

        // set color change according to the bar length
        if (_gasLevel < LowVolumeCoefficient * FullGasLevel)
        {
            gasLevelImage.color = _darkRedColor;
        }
        else if (_gasLevel < MediumVolumeCoefficient * FullGasLevel)
        {
            gasLevelImage.color = _orangeColor;
        }
        else if (_gasLevel < HighVolumeCoefficient * FullGasLevel)
        {
            gasLevelImage.color = _lightGreenColor;
        }
        else
        {
            gasLevelImage.color = _greenColor;
        }
    }
}