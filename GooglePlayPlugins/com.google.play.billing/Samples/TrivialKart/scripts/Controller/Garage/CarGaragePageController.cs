using UnityEngine;
using UnityEngine.UI;

// Controller for the garage page.
public class CarGaragePageController : MonoBehaviour
{
    public GameObject playCarGameObject;
    public Text coinsCountText;
    
    private PlayerController _playerController;


    private void Awake()
    {
        _playerController = playCarGameObject.GetComponent<PlayerController>();
    }


    // Refresh the page when on enable.
    private void OnEnable()
    {
        RefreshPage();
    }

    private void RefreshPage()
    {
        CheckCarOwnership();
        CheckUsingStatus();
    }
    
    // Check if player owns the car.
    private void CheckCarOwnership()
    {
        foreach (var car in CarList.List)
        {
            var isCarOwned = GameDataController.GetGameData().CheckCarOwnership(car);
            car.GarageItemGameObj.SetActive(isCarOwned);
        }
    }

    private void CheckUsingStatus()
    {
        foreach (var carObj in CarList.List)
        {
            carObj.GarageItemGameObj.transform.Find("statusText").gameObject.SetActive(false);
        }

        GameDataController.GetGameData().CarInUseObj.GarageItemGameObj.transform.Find("statusText").gameObject.SetActive(true);
    }


    public void OnItemSedanClicked()
    {
        SwitchCarInUse(CarList.CarSedan);
    }

    public void OnItemTruckClicked()
    {
        SwitchCarInUse(CarList.CarTruck);
    }

    public void OnItemJeepClicked()
    {
        SwitchCarInUse(CarList.CarJeep);
    }

    public void OnItemKartClicked()
    {
        SwitchCarInUse(CarList.CarKart);
    }

    private void SwitchCarInUse(CarList.Car targetCar)
    {
        // TODO: Combine the save game data into the _gameData.
        GameDataController.GetGameData().ChangeCar(targetCar);
        RefreshPage();
        _playerController.UpdateCarInUse();
    }
    
}
