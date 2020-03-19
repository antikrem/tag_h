/*  Manages storing styles
 *  Styles relate to colors of elements 
 */
using System;
using System.Windows.Media.Color;

public class ColorStyling
{
	// Gets background colour, used for main window
    Color getBackgroundColour()
    {
        return Color.FromArgb(255, 52, 52, 52);
    }

}
