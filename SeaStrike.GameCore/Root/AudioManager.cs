using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using SeaStrike.Core.Entity.GameLogic.Utility;

namespace SeaStrike.GameCore.Root;

public class AudioManager
{
    private readonly SeaStrikeGame game;
    private Song mainOST;
    private Song battleOST;
    private Song victoryOST;
    private Song lostOST;
    private SoundEffect buttonClickSFX;
    private SoundEffect comboBoxSFX;
    private SoundEffect missSFX;
    private SoundEffect hitSFX;
    private SoundEffect sunkSFX;

    public AudioManager(SeaStrikeGame game) => this.game = game;

    public void LoadAudio()
    {
        mainOST = game.Content.Load<Song>("OST/main_ost");
        battleOST = game.Content.Load<Song>("OST/battle_ost");
        victoryOST = game.Content.Load<Song>("OST/victory_ost");
        lostOST = game.Content.Load<Song>("OST/lost_ost");

        buttonClickSFX = game.Content.Load<SoundEffect>("SFX/button_click");
        comboBoxSFX = game.Content.Load<SoundEffect>("SFX/combo_box_select");
        missSFX = game.Content.Load<SoundEffect>("SFX/miss");
        hitSFX = game.Content.Load<SoundEffect>("SFX/hit");
        sunkSFX = game.Content.Load<SoundEffect>("SFX/sunk");
    }

    public void PlayMainOST() => PlaySong(mainOST, true);

    public void PlayBattleOST() => PlaySong(battleOST, true);

    public void PlayVictoryOST() => PlaySong(victoryOST, false);

    public void PlayLostOST() => PlaySong(lostOST, false);

    public void PlayButtonClickSFX() => PlaySFX(buttonClickSFX);

    public void PlayComboBoxSFX() => PlaySFX(comboBoxSFX);

    public void PlayShotResultSFX(ShotResult shotResult)
    {
        if (!shotResult.hit)
            PlaySFX(missSFX);
        else if ((bool)shotResult.sunk)
            PlaySFX(sunkSFX);
        else
            PlaySFX(hitSFX);
    }

    private void PlaySong(Song song, bool loop)
    {
        if (MediaPlayer.State != MediaState.Playing ||
            MediaPlayer.Queue.ActiveSong != song)
        {
            MediaPlayer.IsRepeating = loop;
            MediaPlayer.Play(song);
        }
    }

    private void PlaySFX(SoundEffect sfx)
    {
        SoundEffectInstance instance = sfx.CreateInstance();
        if (instance.State == SoundState.Stopped)
            instance.Play();
    }
}