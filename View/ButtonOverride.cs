using System;
using System.Windows.Controls;
using System.Windows;

namespace TwitchBot.View
{
    public class ButtonOverride : RadioButton
    {
        static ButtonOverride()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonOverride), new FrameworkPropertyMetadata(typeof(ButtonOverride)));
        }
    }
}
