﻿/*  Manages storing styles
 *  Styles relate to colors of elements 
 */
using System;
using static System.Windows.Media.Color;

public class ColorStyling
{
    // Gets background colour, used for main window
    public static System.Windows.Media.Color getBackgroundColour()
    {
        return System.Windows.Media.Color.FromArgb(255, 34, 34, 34);
    }

    // Gets transparent colour, used for border
    public static System.Windows.Media.Color getBorderColour()
    {
        return System.Windows.Media.Color.FromArgb(100, 52, 52, 52);
    }

}
