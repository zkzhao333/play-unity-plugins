using System.Collections.Generic;
using UnityEngine;

public static class BackgroundList
{
    public class Background
    {
        private GameObject _backGroundGarageItemGameObject;

        public Background(BackgroundName name, GameObject backGroundGarageItemGameObject, Sprite imageSprite)
        {
            Name = name;
            // TODO: change this game object to real game object
            BackGroundGarageItemGameObject = backGroundGarageItemGameObject;
            ImageSprite = imageSprite;
        }

        public BackgroundName Name { get; }

        public GameObject BackGroundGarageItemGameObject
        {
            get => _backGroundGarageItemGameObject;
            set => _backGroundGarageItemGameObject = value;
        }

        public Sprite ImageSprite { get; }
    }


    public static readonly Background BlueGrassBackground =
        new Background(BackgroundName.BlueGrass, new GameObject(), Resources.Load<Sprite>("background/blueGrass"));

    public static readonly Background MushroomBackground =
        new Background(BackgroundName.Mushroom, new GameObject(), Resources.Load<Sprite>("background/coloredShroom"));

    public static readonly List<Background> List = new List<Background>()
        {BlueGrassBackground, MushroomBackground};
}