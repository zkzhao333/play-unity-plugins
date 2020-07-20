using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Constant data for backgrounds.
/// </summary>
public class BackgroundList
{
    public class Background
    {
        public Background(BackgroundName name)
        {
            Name = name;
        }

        public BackgroundName Name { get; }

        public GameObject GarageItemGameObj { get; set; }

        public Sprite ImageSprite { get; set; }
    }

    public static readonly Background BlueGrassBackground =
        new Background(BackgroundName.BlueGrass);

    public static readonly Background MushroomBackground =
        new Background(BackgroundName.Mushroom);

    public static readonly List<Background> List = new List<Background>()
        {BlueGrassBackground, MushroomBackground};
}