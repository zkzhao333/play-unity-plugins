using UnityEngine;
using UnityEngine.UI;

// Controller for the garage page.
public class CarGaragePageController : MonoBehaviour
{
    
    // Refresh the page when enter the car garage page.
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
        // TODO: make the unavailable car in gray color.
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

        GameDataController.GetGameData().CarInUseObj.GarageItemGameObj.transform.Find("statusText").gameObject
            .SetActive(true);
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
        GameDataController.GetGameData().ChangeCar(targetCar);
        RefreshPage();
    }
}