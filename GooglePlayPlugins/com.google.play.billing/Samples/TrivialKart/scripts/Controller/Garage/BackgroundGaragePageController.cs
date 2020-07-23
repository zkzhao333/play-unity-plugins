using UnityEngine;

/// <summary>
/// Controller for background garage page.
/// It listens to the background switch button click events;
/// It controls activeness and usage status of background garage items.
/// </summary>
public class BackgroundGaragePageController : MonoBehaviour
{
    private void OnEnable()
    {
        RefreshPage();
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
        GameDataController.GetGameData().UpdateBackgroundInUse(targetBackground);
        SetBackgroundUsageStatus();
    }

    private void RefreshPage()
    {
        ToggleBackgroundActivenessBasedOnOwnership();
        SetBackgroundUsageStatus();
    }

    // Check if player owns the background, and set the item activeness accordingly.
    private void ToggleBackgroundActivenessBasedOnOwnership()
    {
        foreach (var background in BackgroundList.List)
        {
            var isBackgroundOwned = GameDataController.GetGameData().CheckBackgroundOwnership(background);
            background.GarageItemGameObj.SetActive(isBackgroundOwned);
        }
    }

    private void SetBackgroundUsageStatus()
    {
        foreach (var background in BackgroundList.List)
        {
            background.GarageItemGameObj.transform.Find("statusText").gameObject.SetActive(false);
        }

        GameDataController.GetGameData().BackgroundInUseObj.GarageItemGameObj.transform.Find("statusText").gameObject
            .SetActive(true);
    }
}