using System;
using UnityEngine;

// controller for car movement
public class PlayerController : MonoBehaviour
{
    public GameObject cam;

    private GameObject _carInUseGameObj;
    private Animator _carInUseAnimator;
    private Gas _gas;
    private Vector3 _carStartPos;
    private Rigidbody2D _rigidbody2D;
    private int _circleCount;
    private static readonly int Speed = Animator.StringToHash("speed");
    private const int EndOfRoadPositionX = 25;
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
        var currentCircleTravelDistance = EndOfRoadPositionX - _carStartPos.x;
        _gas.SetGasLevel(currentCircleTravelDistance, _circleCount,
            (float) Math.Round(_carInUseGameObj.transform.position.x - _carStartPos.x, 1));

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
            car.playCarGameObj.SetActive(false);
        }
        
        // set the car in use game object to be active
        var carInUseGameObj = _gameData.GetCarInUseObj().playCarGameObj;
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