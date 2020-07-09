using System;
using UnityEngine;

// Car stores all info about a car.
public class Car
{
    private GameObject _storeItemCarGameObj;
    private GameObject _garageItemGameObj;
    private GameObject _playCarGameObj;

    public Car(string carName, int speed, float price, bool isPriceInDollar)
    {
        this.CarName = carName;
        this.Speed = speed;
        this.Price = price;
        this.IsPriceInDollar = isPriceInDollar;
    }

    public string CarName { get; }

    public int Speed { get; }

    public float Price { get; }

    public bool IsPriceInDollar { get; }

    public GameObject StoreItemCarGameObj
    {
        get => _storeItemCarGameObj;
        set => _storeItemCarGameObj = value;
    }

    public GameObject GarageItemGameObj
    {
        get => _garageItemGameObj;
        set => _garageItemGameObj = value;
    }

    public GameObject PlayCarGameObj
    {
        get => _playCarGameObj;
        set => _playCarGameObj = value;
    }
}