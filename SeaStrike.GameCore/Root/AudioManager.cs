using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SeaStrike.GameCore.Root;

public class AudioManager
{
    public bool audioLoaded;
    public SoundEffect buttonClickSFX;

    private readonly SeaStrikeGame game;
    private Song mainOST;
    private Song battleOST;
    private Song victoryOST;
    private Song lostOST;

    public AudioManager(SeaStrikeGame game) => this.game = game;

    public void LoadAudio()
    {
        mainOST = game.Content.Load<Song>("SFX/main_ost");
        battleOST = game.Content.Load<Song>("SFX/battle_ost");
        victoryOST = game.Content.Load<Song>("SFX/victory_ost");
        lostOST = game.Content.Load<Song>("SFX/lost_ost");
        buttonClickSFX = game.Content.Load<SoundEffect>("SFX/button_click");

        audioLoaded = true;
    }

    public void PlayMainOST() => PlaySong(mainOST, true);

    public void PlayBattleOST() => PlaySong(battleOST, true);

    public void PlayVictoryOST() => PlaySong(victoryOST, false);

    public void PlayLostOST() => PlaySong(lostOST, false);

    private void PlaySong(Song song, bool loop)
    {
        if (MediaPlayer.Queue.ActiveSong != song)
        {
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = loop;
        }
    }
}