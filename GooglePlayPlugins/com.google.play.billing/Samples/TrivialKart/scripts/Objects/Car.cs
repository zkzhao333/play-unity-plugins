using System;
using UnityEngine;

// Car contains all info about a car.
public class Car
{
    private GameObject _storeItemCarGameObj;
    private GameObject _garageItemGameObj;
    private GameObject _playCarGameObj;

    public Car(string carName, int speed, float price, bool isPriceInDollar, string productId)
    {
        CarName = carName;
        Speed = speed;
        Price = price;
        IsPriceInDollar = isPriceInDollar;
        ProductId = productId;
    }

    public string CarName { get; }

    public int Speed { get; }

    public float Price { get; }

    public bool IsPriceInDollar { get; }
    
    public string ProductId { get; }

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