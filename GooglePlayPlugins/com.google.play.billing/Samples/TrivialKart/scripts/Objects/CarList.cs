using System.Collections.Generic;
using UnityEngine;

// Constant data of cars.
public static class CarList
{
    public static readonly Car CarSedan = new Car("carSedan", 500, 0, true, "car_sedan");
    public static readonly Car CarTruck = new Car("carTruck", 600, 20, false, "car_truck");
    public static readonly Car CarJeep = new Car("carJeep", 650, 2.99f, true, "car_jeep");
    public static readonly Car CarKart = new Car("carKart", 1000, 4.99f, true, "car_kart");
    public static readonly List<Car> List = new List<Car>() {CarSedan, CarTruck, CarJeep, CarKart};

    // Return car object by name.
    public static Car GetCarByName(string carName)
    {
        switch (carName)
        {
            case "carSedan":
                return CarSedan;
            case "carTruck":
                return CarTruck;
            case "carJeep":
                return CarJeep;
            case "carKart":
                return CarKart;
            default:
                Debug.Log("car " + carName + " doesn't exist");
                return CarSedan;
        }
    }

    // Return the carIndex by carName.
    public static int GetIndexByName(string carName)
    {
        switch (carName)
        {
            case "carSedan":
                return 0;
            case "carTruck":
                return 1;
            case "carJeep":
                return 2;
            case "carKart":
                return 3;
            default:
                Debug.Log("car " + carName + " doesn't exist");
                return 0;
        }
    }
}