using System.Linq;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.Core.Entity.Game.Utility;
using SeaStrike.PC.Root.Widgets.GridTile;
using Player = SeaStrike.PC.Root.Network.Player;

namespace SeaStrike.PC.Root.Widgets.BattleGrid;

public class MultiplayerOpponnentBattleGridPanel : OpponentBattleGridPanel
{
    private Player player;

    public MultiplayerOpponnentBattleGridPanel(
        Player player,
        SeaStrike game,
        SeaStrikeGame seaStrikeGame,
        Board opponentBoard)
        : base(game, seaStrikeGame, opponentBoard) => this.player = player;

    protected override void ShootTile(object sender)
    {
        System.Console.WriteLine("+");
    }
}