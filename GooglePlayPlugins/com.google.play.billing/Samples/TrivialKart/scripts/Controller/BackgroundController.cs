using UnityEngine;

// Controller for background garage page.
public class BackgroundController : MonoBehaviour
{
    public GameObject backGroundImages;

    // Switch the background of play page to mushroom background.
    public void SwitchToMushroomBackGround()
    {
        foreach (Transform backGround in backGroundImages.transform)
        {
            backGround.gameObject.GetComponent<SpriteRenderer>().sprite = BackgroundList.MushroomBackground.ImageSprite;
        }
    }
    
    // TODO: add other switchers.
}