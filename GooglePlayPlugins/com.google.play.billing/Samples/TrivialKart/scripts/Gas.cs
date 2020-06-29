using UnityEngine;
using UnityEngine.UI;

public class Gas : MonoBehaviour
{
    public GameObject noGasText;
    public GameObject gasLevelImageObj;

    private const float Mpg = 0.1f;
    private const float FullGasLevel = 5.0f;
    private float _gasLevel = 5.0f;
    private float _droveDistanceBeforeLastFill = 0f;
    private float _totalDistanceDriven = 0f;
    private Image _gasLevelImage;
    private readonly Color32 _darkRedColor = new Color32(196, 92, 29, 255);
    private readonly Color32 _orangeColor = new Color32(255, 196, 0, 255);
    private readonly Color32 _lightGreenColor = new Color32(125, 210, 76, 255);
    private readonly Color32 _greenColor = new Color32(94, 201, 93, 255);

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
        _gasLevel = FullGasLevel;
        // set current distance to previous distance
        _droveDistanceBeforeLastFill = _totalDistanceDriven;
        noGasText.SetActive(false);
    }

    public float GetFullGasLevel()
    {
        return FullGasLevel;
    }

    public float GetGasLevel()
    {
        return _gasLevel;
    }

    // Set the gas level bar length and color according to the distance the car has traveled
    public void SetGasLevel(float lengthPerCircle, int circleCount, float distance)
    {
        _totalDistanceDriven = (lengthPerCircle * circleCount + distance);
        if (_gasLevel > 0)
        {
            // TODO use the real mpg of each car
            var consumedGas = (_totalDistanceDriven - _droveDistanceBeforeLastFill) * Mpg;
            _gasLevel = FullGasLevel - consumedGas;
            SetGasLevelHelper(_gasLevelImage, gasLevelImageObj);
        }
    }


    public void SetGasLevelHelper(Image image, GameObject obj)
    {
        obj.transform.localScale = new Vector3(_gasLevel / FullGasLevel, 1, 1);

        // set color change according to the bar length
        if (_gasLevel < 0.2 * FullGasLevel)
        {
            image.color = _darkRedColor;
        }
        else if (_gasLevel < 0.4 * FullGasLevel)
        {
            image.color = _orangeColor;
        }
        else if (_gasLevel < 0.6 * FullGasLevel)
        {
            image.color = _lightGreenColor;
        }
        else
        {
            image.color = _greenColor;
        }
    }
}