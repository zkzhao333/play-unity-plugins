using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public string carInUse;
    public bool[] carIndexToOwnership;
    public int coinOwned;

    private string _dataPath;
    private const int InitialCoin = 20;
    private const int TotalCarCount = 4;
    private const bool Owned = true;
    private const bool NotOwned = false;
    public GameData(string dataPath)
    {
        _dataPath = dataPath;
        coinOwned = InitialCoin;
        carIndexToOwnership = new bool[TotalCarCount];
        carIndexToOwnership[CarList.GetIndexByName("carSedan")] = Owned;
        carIndexToOwnership[CarList.GetIndexByName("carTruck")] = NotOwned;
        carIndexToOwnership[CarList.GetIndexByName("carJeep")] = NotOwned;
        carIndexToOwnership[CarList.GetIndexByName("carKart")] = NotOwned;
        carInUse = "carSedan";
    }

    // save init game data when the user first time playing this game
    public void SaveInitialGameData()
    {
        
    }
    
    // reduce coins owned 
    public void ReduceCoinsOwned(int reduceAmount)
    {
        coinOwned -= reduceAmount;
    }
    
    // increase coins owned
    public void IncreaseCoinsOwned(int increaseAmount)
    {
        coinOwned += increaseAmount;
    }
    
    // own a car
    public void PurchaseCar(string carName)
    {
        carIndexToOwnership[CarList.GetIndexByName(carName)] = Owned;
    }

    // check if user owns a car with carName
    public bool CheckOwnership(string carName)
    {
        return carIndexToOwnership[CarList.GetIndexByName(carName)];
    }
}

//我觉得就 放gamemanegr然后 get gamedata吧