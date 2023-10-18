using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SeaStrike.GameCore.Root;

public class AudioManager
{
    public SoundEffect buttonClickSFX;

    private readonly SeaStrikeGame game;
    private Song mainOST;
    private Song battleOST;
    private Song victoryOST;
    private Song lostOST;

    public AudioManager(SeaStrikeGame game) => this.game = game;

    public void LoadAudio()
    {
        mainOST = game.Content.Load<Song>("OST/main_ost");
        battleOST = game.Content.Load<Song>("OST/battle_ost");
        victoryOST = game.Content.Load<Song>("OST/victory_ost");
        lostOST = game.Content.Load<Song>("OST/lost_ost");

        buttonClickSFX = game.Content.Load<SoundEffect>("SFX/button_click");
    }

    public void PlayMainOST() => PlaySong(mainOST, true);

    public void PlayBattleOST() => PlaySong(battleOST, true);

    public void PlayVictoryOST() => PlaySong(victoryOST, false);

    public void PlayLostOST() => PlaySong(lostOST, false);

    private void PlaySong(Song song, bool loop)
    {
        if (MediaPlayer.State != MediaState.Playing ||
            MediaPlayer.Queue.ActiveSong != song)
        {
            MediaPlayer.IsRepeating = loop;
            MediaPlayer.Play(song);
        }
    }
}