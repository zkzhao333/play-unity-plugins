using System;
using System.Collections.Generic;
using UnityEngine;

// controller for car movement
public class PlayerController : MonoBehaviour
{
    public GameObject cam;
    public GameObject carSedan;
    public GameObject carTruck;
    public GameObject carJeep;
    public GameObject carKart;

    private GameObject _carInUse;
    private Animator _carInUseAnimator;
    private Gas _gas;
    private Vector3 _carStartPos;
    private Rigidbody2D _rigidbody2D;
    private int _circleCount;
    private static readonly int Speed = Animator.StringToHash("speed");
    private GameData _gameData;
    private Vector3 _camOffset;

    public void Start()
    {
        _gameData = FindObjectOfType<GameManager>().GetGameData();
        UpdateCarInUse();
        _circleCount = 0;
        _gas = GetComponent<Gas>();
        _carStartPos = _carInUse.transform.position;
        _camOffset = cam.transform.position - _carStartPos;
        Debug.Log("has started play controller.cs");
    }


    private void FixedUpdate()
    {
        // back to the start point when reach the end
        if (_carInUse.transform.position.x >= 25)
        {
            _circleCount++;
            _carInUse.transform.position = _carStartPos;
        }

        _carInUseAnimator.SetFloat(Speed, _rigidbody2D.velocity.magnitude);
        _gas.SetGasLevel(25 - _carStartPos.x, _circleCount,
            (float) Math.Round(_carInUse.transform.position.x - _carStartPos.x, 2));

        // update cam position
        var carPosition = _carInUse.transform.position;
        cam.transform.position = new Vector3(carPosition.x, carPosition.y, carPosition.z) + _camOffset;
    }

    // update the car in use in the play when player switch the car.
    public void UpdateCarInUse()
    {
        switch (_gameData.carInUse)
        {
            case "carSedan":
                SetUsingState(carSedan, new List<GameObject> {carTruck, carJeep, carKart});
                break;
            case "carTruck":
                SetUsingState(carTruck, new List<GameObject> {carSedan, carJeep, carKart});
                break;
            case "carJeep":
                SetUsingState(carJeep, new List<GameObject> {carSedan, carTruck, carKart});
                break;
            case "carKart":
                SetUsingState(carKart, new List<GameObject> {carSedan, carTruck, carJeep});
                break;
        }
    }

    private void SetUsingState(GameObject usingCarObj, List<GameObject> notUsingCarObjList)
    {
        usingCarObj.SetActive(true);
        if (!(_carInUse is null))
        {
            // sync the position of next use car
            usingCarObj.transform.position = _carInUse.transform.position;
        }

        _carInUse = usingCarObj;
        _carInUseAnimator = _carInUse.GetComponent<Animator>();
        _rigidbody2D = _carInUse.GetComponent<Rigidbody2D>();
        foreach (var carObj in notUsingCarObjList)
        {
            carObj.SetActive(false);
        }
    }
}