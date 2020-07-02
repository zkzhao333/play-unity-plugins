using System;

[Serializable]
public class GameData
{
    public string carInUseName;
    public bool[] carIndexToOwnership;
    public int coinsOwned;

    private Car _carInUseObj;
    private const int InitialCoinAmount = 20;
    private const int TotalCarCount = 4;
    private const bool Owned = true;
    private const bool NotOwned = false;

    public GameData()
    {
        coinsOwned = InitialCoinAmount;
        carIndexToOwnership = new bool[TotalCarCount];
        carIndexToOwnership[CarList.GetIndexByName("carSedan")] = Owned;
        carIndexToOwnership[CarList.GetIndexByName("carTruck")] = NotOwned;
        carIndexToOwnership[CarList.GetIndexByName("carJeep")] = NotOwned;
        carIndexToOwnership[CarList.GetIndexByName("carKart")] = NotOwned;
        carInUseName = "carSedan";
    }

    // update car object in use
    public void SetCarInUseObj()
    {
        _carInUseObj = CarList.GetCarByName(carInUseName);
    }

    // get the car object in use
    public Car GetCarInUseObj()
    {
        return _carInUseObj;
    }


    // reduce coins owned 
    public void ReduceCoinsOwned(int reduceAmount)
    {
        coinsOwned -= reduceAmount;
    }

    // increase coins owned
    public void IncreaseCoinsOwned(int increaseAmount)
    {
        coinsOwned += increaseAmount;
    }

    // own a car
    public void PurchaseCar(string carName)
    {
        carIndexToOwnership[CarList.GetIndexByName(carName)] = Owned;
    }

    // check if user owns a car with carName
    // return true if the user owns it; otherwise return false
    public bool CheckOwnership(string carName)
    {
        return carIndexToOwnership[CarList.GetIndexByName(carName)];
    }
}