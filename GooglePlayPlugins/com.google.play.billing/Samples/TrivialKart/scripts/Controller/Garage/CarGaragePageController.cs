using UnityEngine;

/// <summary>
/// Controller for the car garage page.
/// It listens to the car switch button click events;
/// It controls activeness and usage status of car garage items.
/// </summary>
public class CarGaragePageController : MonoBehaviour
{
    // Refresh the page when enter the car garage page.
    private void OnEnable()
    {
        RefreshPage();
    }

    private void RefreshPage()
    {
        ToggleCarActivenessBasedOnOwnership();
        SetCarUsageStatus();
    }

    // Check if player owns the car.
    private void ToggleCarActivenessBasedOnOwnership()
    {
        // TODO: make the unavailable car in gray color.
        foreach (var car in CarList.List)
        {
            var isCarOwned = GameDataController.GetGameData().CheckCarOwnership(car);
            car.GarageItemGameObj.SetActive(isCarOwned);
        }
    }

    private void SetCarUsageStatus()
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
        GameDataController.GetGameData().UpdateCarInUse(targetCar);
        SetCarUsageStatus();
    }
}