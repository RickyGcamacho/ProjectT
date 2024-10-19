using UnityEngine;

public class MenuView : ActorView
{
    public override void SetReferences()
    {
        base.SetReferences();
        _dicReferences = FMODEvents.instance.EventReferencesUI;
    }
    public void HighlihghtButton()
    {

        _sounds[Sounds.UI_MOVECURSOR].start();
    }
    public void PlayButton()
    {
        _sounds[Sounds.UI_PLAYBUTTON].start();
    }
    public void AmbientSound()
    {
        _sounds[Sounds.UI_PLAYBUTTON].start();
    }
}
