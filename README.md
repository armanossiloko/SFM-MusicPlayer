# SFM Music Player

![](https://i.imgur.com/qcfUxYl.png "SFM Music Player Preview")

This is a minimalistic MP3 player coded in C#. It originally started as a personal idea due to me searching for a small music player that would be in the bottom right corner of my desktop, which I never managed to find.

# How to use:

Clicking on the browse button, you can select either an MP3 (or multiple MP3 files) or a CSV file that would represent your playlist / song database, containing details for each song you want. The layout for the CSV goes as follows: `Artist,Title,Album`. These three (3) columns are required for each row of the CSV.

When loading a CSV file, the player will look for MP3 files based on the content of the CSV by following the filename template: `Artist - Title.mp3`. If it doesn't find a file that follows the template, an error will be provided.

## Settings

![](https://i.imgur.com/n9xUjhG.png "Settings Form")


The "Default Search Path" represents the default directory that you will be redirected to when you browse for a song to play.

To "Edit ID3 Tags", first click on the Browse button, select an MP3 of your choice, click on the "Right Arrow" button (next to the Browse button) and fill the form with the info such as Artist, Title, Album. Beside that, the Browse button on that form will allow you choose an image (album art) that will be displayed when playing the song. Clicking on the Arrow button on this form will save your info.

![](https://i.imgur.com/aWRMdGy.png "ID3 Tags")

From the Settings (which can be accessed directly via the Settings button), you can create your own CSV playlist (whose content will be based on the ID3 tags of the file - if those aren't present, the content of the CSV will be derived from the filename, which SHOULD follow the `Artist - Title.mp3` pattern).

![](https://i.imgur.com/Y0bzkpf.png "CSV Creation")

For now, clicking on the Info icon will open up some base info about the player (on which the top area is empty - which is a placeholder for the logo).

# Credits:

- Crispy 
- Fabrice Lacharme ([Volume Slider](https://github.com/fabricelacharme/ColorSlider))
- Icons8 (Graphics)
- Mono Project (TagLib Sharp)
- Pixel Perfect (Icon)
- Van Cruz 

# Thanks go to:

- Alberto "Cianez" Zanella (Testing)
- Harun Hodžić (Suggestions)
- [Yogensia](https://github.com/yogensia) (Testing, Suggestions)
