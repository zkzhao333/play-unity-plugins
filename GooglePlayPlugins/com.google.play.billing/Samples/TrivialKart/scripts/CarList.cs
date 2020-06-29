
public static class CarList
{
     public static Car CarSedan = new Car("carSedan", 500, 0, false);
     public static Car CarTruck = new Car("carTruck", 600, 0, false );
     public static Car CarJeep = new Car("carJeep", 650, 0, true);
     public static Car CarKart =  new Car("carKart", 1000, 0, true);

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
