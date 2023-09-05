using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.PC.Root.Network;

namespace SeaStrike.PC.Root.Widgets.BattleGrid.Multiplayer;

public class MultiplayerOpponnentBattleGridPanel : OpponentBattleGridPanel
{
    private NetPlayer player;

    public MultiplayerOpponnentBattleGridPanel(
        NetPlayer player,
        SeaStrikeGame seaStrikeGame,
        Board opponentBoard)
        : base(player.game, seaStrikeGame, opponentBoard) =>
            this.player = player;

    protected override void ShootTile(object sender)
    {
        System.Console.WriteLine("+");
    }
}