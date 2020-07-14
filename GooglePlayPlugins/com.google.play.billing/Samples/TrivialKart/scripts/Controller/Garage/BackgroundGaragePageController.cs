using System;
using UnityEngine;

// Controller for background garage page.
public class BackgroundGaragePageController : MonoBehaviour
{
    public GameObject backGroundImages;

    private void OnEnable()
    {
        RefreshPages();
    }
    

    public void OnItemBlueGrassBackgroundClicked()
    {
        SwitchToTargetBackground(BackgroundList.BlueGrassBackground);
    }

    public void OnItemMushroomBackGroundClicked()
    {
        SwitchToTargetBackground(BackgroundList.MushroomBackground);
    }


    private void SwitchToTargetBackground(BackgroundList.Background targetBackground)
    {
        GameDataController.GetGameData().ChangeBackground(targetBackground);
        RefreshPages();
    }

    private void RefreshPages()
    {
        CheckCarOwnership();
        CheckUsingStatus();
    }

    // Check if player owns the car.
    private void CheckCarOwnership()
    {
        foreach (var background in BackgroundList.List)
        {
            var isBackgroundOwned = GameDataController.GetGameData().CheckBackgroundOwnership(background);
            background.GarageItemGameObj.SetActive(isBackgroundOwned);
        }
    }

    private void CheckUsingStatus()
    {
        foreach (var background in BackgroundList.List)
        {
            background.GarageItemGameObj.transform.Find("statusText").gameObject.SetActive(false);
        }

        GameDataController.GetGameData().BackgroundInUseObj.GarageItemGameObj.transform.Find("statusText").gameObject
            .SetActive(true);
    }
}