using System;
using UnityEngine;

// controller for car movement
public class PlayerController : MonoBehaviour
{
    public GameObject cam;

    private const int EndOfRoadPositionX = 25;
    private static readonly int Speed = Animator.StringToHash("speed");
    private GameObject _carInUseGameObj;
    private Animator _carInUseAnimator;
    private Gas _gas;
    private Vector3 _carStartPos;
    private Rigidbody2D _rigidbody2D;
    private int _circleCount;
    private GameData _gameData;
    private Vector3 _camOffset;

    public void Start()
    {
        _gameData = FindObjectOfType<GameManager>().GetGameData();
        UpdateCarInUse();
        _circleCount = 0;
        _gas = GetComponent<Gas>();
        _carStartPos = _carInUseGameObj.transform.position;
        _camOffset = cam.transform.position - _carStartPos;
    }


    private void FixedUpdate()
    {
        // back to the start point when reach the end
        if (_carInUseGameObj.transform.position.x >= EndOfRoadPositionX)
        {
            _circleCount++;
            _carInUseGameObj.transform.position = _carStartPos;
        }
        
        _carInUseAnimator.SetFloat(Speed, _rigidbody2D.velocity.magnitude);
        
        // update gas level
        var distancePerCircle = EndOfRoadPositionX - _carStartPos.x;
        var distanceTraveledInCurCircle =  (float) Math.Round(_carInUseGameObj.transform.position.x - _carStartPos.x, 1);
        var totalDistanceTraveled = distancePerCircle * _circleCount + distanceTraveledInCurCircle;
        _gas.SetGasLevel(totalDistanceTraveled);

        // update cam position
        var carPosition = _carInUseGameObj.transform.position;
        cam.transform.position = new Vector3(carPosition.x, carPosition.y, carPosition.z) + _camOffset;
    }

    // update the car in use in the play when player switch the car.
    public void UpdateCarInUse()
    {
        // set all car to be inactive
        foreach (var car in CarList.List)
        {
            car.PlayCarGameObj.SetActive(false);
        }

        // set the car in use game object to be active
        var carInUseGameObj = _gameData.CarInUseObj.PlayCarGameObj;
        SetUsingState(carInUseGameObj);
    }

    private void SetUsingState(GameObject carInUseGameObj)
    {
        carInUseGameObj.SetActive(true);
        if (!(_carInUseGameObj is null))
        {
            // sync the position of next use car
            carInUseGameObj.transform.position = _carInUseGameObj.transform.position;
        }

        _carInUseGameObj = carInUseGameObj;
        _carInUseAnimator = _carInUseGameObj.GetComponent<Animator>();
        _rigidbody2D = _carInUseGameObj.GetComponent<Rigidbody2D>();
    }
}