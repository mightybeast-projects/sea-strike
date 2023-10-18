using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SeaStrike.GameCore.Root;

public class AudioManager
{
    public SoundEffect buttonClickSFX;

    private readonly SeaStrikeGame game;
    private Song mainOST;

    public AudioManager(SeaStrikeGame game) => this.game = game;

    public void LoadAudio()
    {
        mainOST = game.Content.Load<Song>("SFX/main_ost");
        buttonClickSFX = game.Content.Load<SoundEffect>("SFX/button_click");
    }

    public void PlayOST()
    {
        MediaPlayer.Play(mainOST);
        MediaPlayer.IsRepeating = true;
    }
}