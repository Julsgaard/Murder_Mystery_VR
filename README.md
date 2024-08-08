## Setup
The project was developed and tested for Unity version 2022.3.9f1

The game is meant to be played using a VR headset connected to Unity. While developing and testing, we used the Oculus Quest 2 headset.

While playing the game, the player can interact with NPCs through two ways:
1. Talking to them through speech recognition. This is done by being within proximity of an NPC and holding down the X button on the VR controller while talking, and releasing the button upon finishing talking.
2. Interacting with them through dialogue options that appear when you enter the proximity of an NPC. These dialogue options can be navigated by aiming with the right controller, and pressing the right trigger to select an option.

These two NPC interaction methods can be toggled between by ticking of the bool "Enable Dialogue Options" located on the GameManager gameobject. (ON for dialogue options, OFF for speech recognition). 
Also located on the Gamemanager is the microphones. Make sure the desired microphone is located in index 0 of the list.

The NPCs use GPT4 to generate responses. This requires an API key to work, which should be placed in line 13 of the "ChatGPTManager" script.

Finally, the NPCs use the LMNT package for text to speech. This also requires an API key which can be generated and placed within the LMNT tab located in the top of the Unity window. 

### ISSUE:
Due to a change in the Unity package we use to integrate OpenAI in our project, you might need to change line 177 in the OpenAIApi scripts (Packages/OpenAI Unity/Runtime) from this:
data = JsonConvert.DeserializeObject<T>(request.downloadHandler.text, jsonSerializerSettings);
To this:
data = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);


## Game Guide:
Upon starting the game, the player is placed in a tutorial room that explanis the setup of the murder mystery, as well as the controls of the game.
The room can be excited by pressing the button located beneath the intro text. Upon exiting, the player is placed in a mansion and now has 15 minutes to figure out the culprit behind the murder (Remaining time can be seen by looking at the watch attached to the players right wrist.) 
The player can gather information mainly through two ways:
- Interacting with NPCs and asking them questions (Either through dialogue options or speech recognition)
- Gathering clues located through the mansion. These clues can be distinguised from other objects as they are highlighted with particle effects and can be picked up. Upon having interacted with a clue, the player can now ask questions about this clue to NPCs.

The game does not offer a way to win and/or guess the culprit and simply ends after the 15 minutes have passed.

## Spoilers for the game:
The true culprit of the game is Jens and his motive was jealousy. 

There are in total 5 clues to be found throughout the mansion:
- Doll (Located where Chris' body is found): Used by Jens to frame Leonard for the murder.
- Letter (Located in the drawer found in Quinn's room): A love letter written by Jens adressed to Quinn.
- Vase (Located in Ashley's room): Tells the player of the argument Chris and Ashley had.
- Knife (Located in the drawer found in Jens' room): The weapon Jens used to kill Chris.
- Pot (Located in the kitchen): By turning this pot around, a bloody handprint from Jens hand can be found. 

