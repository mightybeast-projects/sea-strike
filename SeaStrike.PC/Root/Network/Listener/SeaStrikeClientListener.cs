using System;
using LiteNetLib;

namespace SeaStrike.PC.Root.Network.Listener;

public class SeaStrikeClientListener : SeaStrikeListener
{
    public SeaStrikeClientListener(NetPlayer player) : base(player) { }

    public override void OnNetworkReceive(
        NetPeer peer,
        NetPacketReader reader,
        byte channelNumber,
        DeliveryMethod deliveryMethod)
    {
        string message = reader.GetString();

        Console.WriteLine("From server: {0}", message);

        if (message == NetUtils.deploymentPhaseStartMessage)
            player.RedirectToDeploymentScreen();
        else if (message == NetUtils.startBattlePhaseMessage)
            player.RedirectToBattleScreen();
        else if (MessageIsBoardData(message))
            player.ReceiveOpponentBoardData(message);
        else
            player.HandleOpponentShot(message);

        reader.Recycle();
    }
}