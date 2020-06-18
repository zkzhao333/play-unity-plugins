using System;

[Serializable]
public class Car
{
    public string carName;
    public int speed;
    public float price;
    public bool priceInDollar;
    public bool owned;

    public Car(string carName, int speed, float price, bool priceInDollar, bool owned)
    {
        this.carName = carName;
        this.speed = speed;
        this.price = price;
        this.priceInDollar = priceInDollar;
        this.owned = owned;
    }


}
