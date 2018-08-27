# SFM Music Player

![](https://i.imgur.com/qcfUxYl.png "SFM Music Player Preview")

This is a minimalistic MP3 player coded in C#. It originally started as a personal idea due to me searching for a small music player that would be in the bottom right corner of my desktop, which I never managed to find.

# How to use:

Clicking on the browse button (or using the Ctrl + O hotkey), you can select either an MP3 (or multiple MP3 files) or a CSV file that would represent your playlist / song database, containing details or a full path for each song (the CSV must have only one of the two variants). The layout for the CSV with details goes as follows: `Artist,Title,Album`. These three (3) columns are required for each row of the CSV.

When loading a CSV file, the player will look for MP3 files based on the content of the CSV by following the filename template: `Artist - Title.mp3`. If it doesn't find a file that follows the template, an error will be provided. Alternatively, the CSV can contain full paths for each song; e.g `C:\Music\Starset - My Demons.mp3` (note that in this variant, the filename doesn't need to follow the `Artist - Title.mp3` template).

## Settings

![](https://i.imgur.com/vx45Fpb.png "Settings Form")


The "Default Search Path" represents the default directory that you will be redirected to when browsing for a song / playlist to play.

To "Edit ID3 Tags", first click on the Browse button, select an MP3 of your choice and then click on the "Right Arrow" button (next to the Browse button), fill the form with the info such as Artist, Title, Album; the Browse button on the "Edit ID3 tags" pop-up will allow you choose an image (album art) that will be displayed when playing the song. Clicking on the Arrow button here will save your info.

![](https://i.imgur.com/ieTSZRL.png "ID3 Tags")

From the Settings (which can be accessed directly via the Settings button), you can create your own CSV playlist (whose content will be based on the ID3 tags of the file - if those aren't present, the content of the CSV will be derived from the filename, which SHOULD follow the `Artist - Title.mp3` pattern). Alternatively, you can check the "Base CSV on filenames" checkbox to get the other CSV variant containing the full path for each song of the playlist.

![](https://i.imgur.com/Y0bzkpf.png "CSV Creation")

## Hotkeys

- Ctrl + O - Opens the file dialog to search for a song / playlist
- Ctrl + I - Displays base info about the player
- Play / Pause (keyboard special media key) - Plays or pauses the current song
- Backward (keyboard special media key) - Switches to the previous song (if any)
- Forward (keyboard special media key) - Switches to the next song (if any)

# Credits:

- Crispy 
- Fabrice Lacharme ([Volume Slider](https://github.com/fabricelacharme/ColorSlider))
- Icons8 (Graphics)
- Mono Project (TagLib Sharp)
- Pixel Perfect (Icon)
- Van Cruz (Inspirational Design)

# Thanks:

- Alberto "Cianez" Zanella (Testing, Suggestions)
- Harun Hodžić (Suggestions)
- [Yogensia](https://github.com/yogensia) (Testing, Suggestions)