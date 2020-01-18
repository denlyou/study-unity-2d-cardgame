using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R
{
    public static class Sprites
    {
        public static Sprite[] Background = {
            Resources.Load<Sprite>("Sprites/CardBg1"),
            Resources.Load<Sprite>("Sprites/CardBg2"),
            Resources.Load<Sprite>("Sprites/CardBg3"),
            Resources.Load<Sprite>("Sprites/CardBg4"),
            Resources.Load<Sprite>("Sprites/CardBg5")
        };
        public static Sprite[] Shapes = {
            Resources.Load<Sprite>("Sprites/Shape1"),
            Resources.Load<Sprite>("Sprites/Shape2"),
            Resources.Load<Sprite>("Sprites/Shape3"),
            Resources.Load<Sprite>("Sprites/Shape4"),
            Resources.Load<Sprite>("Sprites/Shape5")
        };
    }
}