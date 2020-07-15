using System;
using UnityEngine;

// Controller for car movement.
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
    private Vector3 _camOffset;

    
    private void Start()
    {
        UpdateCarInUse();
        InitValues();
    }

    private void FixedUpdate()
    {
        // Back to the start point when the car reaches the end.
        if (_carInUseGameObj.transform.position.x >= EndOfRoadPositionX)
        {
            _circleCount++;
            _carInUseGameObj.transform.position = _carStartPos;
        }
        
        _carInUseAnimator.SetFloat(Speed, _rigidbody2D.velocity.magnitude);
        
        // Update gas level.
        var distancePerCircle = EndOfRoadPositionX - _carStartPos.x;
        var distanceTraveledInCurCircle =  (float) Math.Round(_carInUseGameObj.transform.position.x - _carStartPos.x, 1);
        var totalDistanceTraveled = distancePerCircle * _circleCount + distanceTraveledInCurCircle;
        _gas.SetGasLevel(totalDistanceTraveled);

        // Update cam position.
        var carPosition = _carInUseGameObj.transform.position;
        cam.transform.position = new Vector3(carPosition.x, carPosition.y, carPosition.z) + _camOffset;
    }
    
    private void InitValues()
    {
        _circleCount = 0;
        _gas = GetComponent<Gas>();
        _carStartPos = _carInUseGameObj.transform.position;
        _camOffset = cam.transform.position - _carStartPos;
    }

    // TODO: Put this method into gameData.cs/gameDataController.cs.
    // Update the car in use in the play page when player switch the car.
    public void UpdateCarInUse()
    {
        // Set all cars to be inactive.
        foreach (var car in CarList.List)
        {
            car.PlayCarGameObj.SetActive(false);
        }
        
        // Set the car in use game object to be active.
        var carInUseGameObj = GameDataController.GetGameData().CarInUseObj.PlayCarGameObj;
        SetUsingState(carInUseGameObj);
    }

    private void SetUsingState(GameObject carInUseGameObj)
    {
        carInUseGameObj.SetActive(true);
        if (!(_carInUseGameObj is null))
        {
            // Sync next use car position with current position.
            carInUseGameObj.transform.position = _carInUseGameObj.transform.position;
        }

        _carInUseGameObj = carInUseGameObj;
        _carInUseAnimator = _carInUseGameObj.GetComponent<Animator>();
        _rigidbody2D = _carInUseGameObj.GetComponent<Rigidbody2D>();
    }
}
