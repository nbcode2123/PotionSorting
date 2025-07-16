using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ColorType
{
    Green,
    Yellow,
    Blue,
    Red,
    Black,
    Purple,
    MintGreen,
    NavyBlue,
    Violet,
    Rainbow
}
public enum ShapeType
{
    Big,
    Small
}
public class Potion : MonoBehaviour
{
    [SerializeField]
    public ColorType ColorSelected = ColorType.Green; // Chọn mặc định là Green
    [SerializeField]

    public ShapeType ShapeSelected = ShapeType.Big; // Chọn mặc định là Green


    public ColorType GetColor()
    {
        return ColorSelected;
    }
    public ShapeType GetShape()
    {
        return ShapeSelected;
    }

}
