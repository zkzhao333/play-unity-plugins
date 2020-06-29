using System;

// Car stores all info about a car.
[Serializable]
public class Car
{
    public string carName;
    public int speed;
    public float price;
    public bool isPriceInDollar;
    public bool owned;
    public float mpg;

    public Car(string carName, int speed, float price, bool isPriceInDollar, bool owned, float mpg)
    {
        this.carName = carName;
        this.speed = speed;
        this.price = price;
        this.isPriceInDollar = isPriceInDollar;
        this.owned = owned;
        this.mpg = mpg;
    }
}