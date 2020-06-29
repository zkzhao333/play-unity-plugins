public static class CarList
{
    public static readonly Car CarSedan = new Car("carSedan", 500, 0, false);
    public static readonly Car CarTruck = new Car("carTruck", 600, 0, false);
    public static readonly Car CarJeep = new Car("carJeep", 650, 0, true);
    public static readonly Car CarKart = new Car("carKart", 1000, 0, true);

    // return car object by name
    public static Car GetCarByName(string carName)
    {
        switch (carName)
        {
            case "carTruck":
                return CarTruck;
            case "carJeep":
                return CarJeep;
            case "carKart":
                return CarKart;
            default:
                return CarSedan;
        }
    }

    // return the carIndex by carName
    public static int GetIndexByName(string carName)
    {
        switch (carName)
        {
            case "carTruck":
                return 1;
            case "carJeep":
                return 2;
            case "carKart":
                return 3;
            default:
                return 0;
        }
    }
}