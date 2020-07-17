using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Constant data for cars.
/// </summary>
public static class CarList
{
    public class Car
    {
        public Car(CarName name, int speed, float price, bool isRealMoneyPurchase, string productId)
        {
            Name = name;
            Speed = speed;
            Price = price;
            IsRealMoneyPurchase = isRealMoneyPurchase;
            ProductId = productId;
        }

        public CarName Name { get; }

        public int Speed { get; }

        public float Price { get; }

        public bool IsRealMoneyPurchase { get; }

        public string ProductId { get; }

        public GameObject StoreItemCarGameObj { get; set; }

        public GameObject GarageItemGameObj { get; set; }

        public GameObject PlayCarGameObj { get; set; }
    }

    public static readonly Car CarSedan = new Car(CarName.Sedan, 500, 0, true, "car_sedan");
    public static readonly Car CarTruck = new Car(CarName.Truck, 600, 20, false, "car_truck");
    public static readonly Car CarJeep = new Car(CarName.Jeep, 650, 2.99f, true, "car_jeep");
    public static readonly Car CarKart = new Car(CarName.Kart, 1000, 4.99f, true, "car_kart");
    public static readonly List<Car> List = new List<Car>() {CarSedan, CarTruck, CarJeep, CarKart};

    // Return car object by name.
    public static Car GetCarByName(CarName carName)
    {
        return List[(int) carName];
    }
}