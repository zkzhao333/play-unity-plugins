using System;
using UnityEngine;

// Car stores all info about a car.
[Serializable]
public class Car
{
    public string carName;
    public int speed;
    public float price;
    public bool isPriceInDollar;
    public GameObject storeItemCarGameObj;
    public GameObject garageItemGameObj;
    public GameObject playCarGameObj;

    public Car(string carName, int speed, float price, bool isPriceInDollar)
    {
        this.carName = carName;
        this.speed = speed;
        this.price = price;
        this.isPriceInDollar = isPriceInDollar;
    }
}