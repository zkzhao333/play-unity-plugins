using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject backGroundImages;

    public void SwitchToDesertBackGround()
    {
        foreach (Transform backGround in backGroundImages.transform)
        {
            backGround.gameObject.GetComponent<SpriteRenderer>().sprite = BackgroundList.MushroomBackground.ImageSprite;
        }
    }
}