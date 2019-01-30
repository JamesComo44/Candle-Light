

Jutebox and relative sounds are inside "Resources" fold specifically for automatic load on play.
--DO NOT-- remove from folder



---Default Level Music:---

JuteBox music has been automated to play without additional scripting. Music can be preselected for each level in the Main Menu scene.
Simply select a sound clip and insert it into the corresponding level slot in the JuteBox inspector.
This feature is for the default music for each level. 



---Event Driven Music:---

The ability to manually script music is retained for event driven music.
- First add desired music to:
	Assets/Resources/Sound/Game Music

- Then add to your script:
	private JuteBox jutebox;

- In your Start() add:
	jutebox = GameObject.FindObjectWithTag("JuteBox").GetComponent<JuteBox>();

- Play music with:
	jutebox.PlayMusic(string nameOfMusic);

- Pause music with:
	jutebox.PauseMusic();

- Set music volume with: volume range from 0-1
	jutebox.MusicVolume(flaot volume);



---Sound Effects---

Follow these steps to que sound effects:
- First add desired sound clips to:
	Assets/Resources/Sound/Candle Light Sound Effects

- Then add to your script:
	private JuteBox jutebox;

- In your Start() add:
	jutebox = GameObject.FindObjectWithTag("JuteBox").GetComponent<JuteBox>();

- Finally call play function with:
	jutebox.PlaySound(string nameOfSound);	and/or	jutebox.SoundVolume(float volume);



---Random Sounds---

Similar to music, you can predetermine which soundclips are randomly played.
Simply drag and drop into the JuteBox inspector, under "Random Clips", the audioClips you want played.

Note: Right now random sounds are hardcoded to play on level scenes 1-4.
		Main Menu and Ending scenes will not play random sounds.



