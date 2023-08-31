using System;

namespace SeaStrike.PC.Root.Widgets;

public class CreateShipButton : GameButton
{
    public CreateShipButton(Action onClick)
    {
        Text = SeaStrike.stringStorage.createShipButtonLabel;

        TouchUp += (s, a) => onClick?.Invoke();
    }
}