# AI_Ex8

In this homework I took the original game from Erel github and change it to make it a result for the tasks.

For task 8: [here](https://github.com/LeveI-Up/AI_Ex8/blob/main/%D7%9E%D7%98%D7%9C%D7%94%208.pdf)

This is a part of week 8 weekly task.
In this game i added 3 levels - in each level: the size of the map is bigger & the number of enemies is increasing.
The player need to reach to the gate in order to get to the next level.
### Changes:
  * Added jump force (by clicking space) [here](https://github.com/LeveI-Up/AI_Ex8/blob/main/Assets/Scripts/1-player/CharacterKeyboardMover.cs)
  * Added 2 weapons
  * Added shot option (by pressing the left mouse button) and a red scope [here](https://github.com/LeveI-Up/AI_Ex8/blob/main/Assets/Gun.cs)
  * Option the switch between the 2 weapons - by clicking 1 or 2 on the keyboard [here](https://github.com/LeveI-Up/AI_Ex8/blob/main/Assets/WeaponSwitching.cs)
  * Scripts & effects for the guns - for switching & lights effects when the player shots
  * Added life to the enemies - when the enemy's health reaches 0, he dies
  * Added HitEngineState: in this state the enemy cought the player and can go and destroy the engine [here](https://github.com/LeveI-Up/AI_Ex8/blob/main/Assets/Scripts/2-npc/HitEngineState.cs)
  * Brave & Coward script
  * The coward enemy: when there are 2 enemies -> the enemy's target is the farthest from the player
  * The brave enemy: when one of the enemies dead -> the enemy's target is the closest from player  
 
  For playing the game click [here](https://almogre.itch.io/ai-ex8) 
  
  Kill the enemies to defend the engine GOOD LUCK!
