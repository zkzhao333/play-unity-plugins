using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CarList
{
    public static readonly Car CarSedan = new Car("carSedan", 500, 0, true);
    public static readonly Car CarTruck = new Car("carTruck", 600, 20, false);
    public static readonly Car CarJeep = new Car("carJeep", 650, 2.99f, true);
    public static readonly Car CarKart = new Car("carKart", 1000, 4.99f, true);
    public static readonly List<Car> List = new List<Car>() {CarSedan, CarTruck, CarJeep, CarKart};

    // return car object by name
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

    // return the carIndex by carName
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