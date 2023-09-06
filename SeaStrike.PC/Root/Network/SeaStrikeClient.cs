using System;
using LiteNetLib;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeClient : SeaStrikeNetManager
{
    public SeaStrikeClient(NetPlayer player) : base(player)
    {
        player.client = this;

        EventBasedNetListener listener = new EventBasedNetListener();

        listener.NetworkReceiveEvent +=
            (fromPeer, dataReader, deliveryMethod, channel) =>
                HandleReceivedMessage(dataReader);
        listener.PeerDisconnectedEvent += (peer, info) =>
            player.RedirectToMainMenu();

        netManager = new NetManager(listener);
    }

    public void Start()
    {
        netManager.Start();
        netManager.Connect(NetUtils.address, 9050, NetUtils.connectionKey);
    }

    public void Disconnect() => netManager.DisconnectAll();

    public void PollEvents() => netManager.PollEvents();

    public void Send(string message) =>
        netManager.SendToAll(
            FormMessage(message), DeliveryMethod.ReliableOrdered);

    private void HandleReceivedMessage(NetPacketReader dataReader)
    {
        string message = dataReader.GetString();

        Console.WriteLine("From server: {0}", message);

        if (message == NetUtils.deploymentPhaseStartMessage)
            player.RedirectToDeploymentScreen();
        else if (message == NetUtils.startBattlePhaseMessage)
            player.RedirectToBattleScreen();
        else if (MessageIsBoardData(message))
            player.ReceiveOpponentBoardData(message);
        else
            player.HandleOpponentShot(message);

        dataReader.Recycle();
    }
}