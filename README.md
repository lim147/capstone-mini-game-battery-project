# About
This repository houses the files used for the G-ScalE Mini-Game Battery Project, a capstone project under development by Team Ludus.

The implementation of `Balloons`,  `Squares`, `Catch The Thief`, `Image Hit`, and `Ball` (including `Catch The Ball`, `Save One Ball`, and `Judge The Ball`) mini-games have been completed.


# Releases
- **The latest build of the game can be found [here](https://ludus-mini-game-battery.surge.sh/)** (public, make sure your browser supports WebGL and has this functionality enabled)
- **The latest code documentation can be found [here](https://ludus-mini-game-battery.surge.sh/docs)** (public)
- **The latest data storage can be found [here](https://app.bonsai.io/clusters/ludusminigamebattery-1683149612/console)** (login required; how to nevigate the UI can be found in this [section](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Testable-Artifact#back-end))
     - username: **bucklj4@mcmaster.ca**
     - password: **LudusCapstone**
     - Type **/battery-session/_search** in the input field


# Related information
- **The Internal Testing document of this project can be found [here](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Tests-Guide-Document)** 
- **The Testable Artifact(for External Testing) can be found [here](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Testable-Artifact)** 
- **The Progress Roadmap can be found [here](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Progress-Roadmap)** 
- **The Combined Coverage of Testing can be found [here](https://ludus-mini-game-battery.surge.sh/coverage/)** 
- **The Test Report can be found [here](https://ludus-mini-game-battery.surge.sh/tests/)** 
- **The Setup Instructions of this project can be found [here](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Setup-Instructions)**
- **For the back-end of this project, [Elasticsearch](https://app.bonsai.io/clusters/battery-session-test-682900662/console) is used to collect user data, which are in JSON-like format.**
- **The Architecture And Module Design document (Design document) can be found [here](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design)**
- **The SRS document can be found [here](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification)**



# Files
- *.unity: the Unity scenes
- *.cs: C# script files, where our modules are implemented
- *.meta: config files to store information such as links between scripts and the scenes. They are auto-generated by Unity and cannot be removed




# Game Instructions
## Squares
The `Squares` game consists of a number of game rounds that lasts for a total of 55 seconds. In each round, a sequence of squares will light up on the screen. After the sequence is complete, the player must recreate the sequence that was shown, by clicking in order, the positions that the squares were at, on the screen. 

In the first round, there will be 3 squares highlighted on the screen. The number of highlighted squres will increase by one for a new round. Within the time period, the player is expected to finish the round where the number of highlighted squares is up to 8 or 9.

## Balloons

In the `Balloons` game, click on the circle at its centre point as fast as you can. In each round, there will be two balloons that appear: the first balloon always appears at the origin on the screen for the purpose of the calibration; the second balloon appears at a random position on the screen, which will be used as measurement.


## Catch The Thief

The `Catch The Thief` game consists of a grid of nine squares. Over a total of 50 seconds, the thief image may appear on a square, and the person image may appear, on a different square. Either image can disappear or reappear at any time. If the thief image appears on a square, and it is the only image on the screen, the player must press the spacebar key, as fast as possible, to attack the thief. Do not press the spacebar key if there is a person image on any square to avoid hurting the innocent person.

## Image Hit

In the `Image Hit` game, ten different images will be shown on the screen one by one and 4-6 images will belong to the same image theme. There will be obvious features in the images for players to comfirm the theme. The theme of the game will be shown at the top of the page. Players will need to press SPACEBAR if the image's theme is the same with the game theme. Do not press the SPACEBAR if they are different.

## Ball Mini-Games
### Catch The Ball

In the `Catch The Ball`, a soccer ball will be kicked towards the player. The player will need to press any key at the moment that they believe they are able to catch the ball.


### Save One Ball

In the `Save One Ball`, there will be two balls and players can only catch one of them, which should be the first one to arrive. Players will need to find out which ball will come first and then press A key to catch the left ball, and L key to catch the right ball.

### Judge The Ball 

In the `Judge The Ball`, there will be three soccer balls which will each start out a random distance from the player's perspective. The ball in the middle is the player's ball while those to the left and right are reference balls. All balls will then be kicked towards the player at the same time with each ball having a random velocity. After a random duration of time, each ball will disappear at once. The user is expected to move a slider to indicate when they believe their ball would have arrived (had it not disappeared) in relation to the two reference balls. 


# Scene Navigation
- Start from the `Start Scene`: the name of the project.

- Next scene the `Info Scene`: the purpose of this mini-game battery. Check on "Acknowledge" box to go to the nest scene.

- Next scene is the `Questionnaire Scene`: fill in user name, age, keyboard availability, mouse availability. Please enter syntatically correct information for these components. Press "Submit" button to go to next scene.

- Next scene is the `Menu Scene`: using this menu, the player can freely choose which game(s)
 to play. Each game will be triggered by pressing the corresponding icon. When the player is finished playing the games they are interested 
 in, they can press the `Finish` button to go the `Result Scene`.

- Final scene is the `Result Scene`: it shows the score and the competency level for the abilities tested by the battery.
   - Click each ability name to go to the single ablity result scene. From there you can check the definition of the ability,
      level, score of the ability, as well as the game(s) that measures this ability.

- Game Scenes:
   - `Balloons` starts:
      - `Balloons Instruction Scene`: after reading, press the "Next" button to play the game.
      - `Balloons Scene`: try to click on the circle at its centre point as fast as you can. 
         When time's up, go back to the menu.

   - `Squares` starts:
      - `Squares Instruction Scene`: after reading, press the "Next" button to play the game.
      - `Squares Scene`: try to repeat the order and position of the highlighted circles in each round.
         Select the `Done` button to go to the next round. When time's up, go back to menu.

   - `Catch The Thief` starts:
      - `Catch The Thief Instruction Scene`: after reading, press the "Next" button to play the game.
      - `Catch The Thief Scene`: press the button when only a thief image appears on one of 9 squares on 
         the screen. When time's up, go back to menu.

   - `Image Hit` starts:
      - `Image Hit Instruction Scene`: after reading, press the "Next" button to play the game.
      - `Image Hit Scene`: press the SPACEBAR as fast as possible when you think the image displayed on the screen is of the specified theme. When all images are traversed, go back to menu.

   - `Ball Mini-Games` starts:
      - `Catch The Ball Instruction Scene`: after reading, press the "Next" button to play the game.
      - `Catch The Ball Scene`: press any key at the moment you believe it is time to catch the ball
with your gloves. When time's up, go back to menu.
      - `Save One Ball Instruction Scene`: after reading, press the "Next" button to play the game.
      - `Save One Ball Scene`: press the "A" key to catch the right ball, press the "L" key to catch
the right ball. Only to catch the first ball to arrive. When time's up, go back to menu.
      - `Judge The Ball Instruction Scene`: after reading, press the "Next" button to play the game.
      - `Judge The Ball Scene`: move a slider to indicate when the middle ball would have arrived, 
among three balls to arrive. When time's up, go back to menu.


# Recorded Data

## General
- Player information


## Squares
- Gameplay data for each round:
   - Highlighted squares sequence: index and local positions of the centre of the highlighted squares 
   in the order that those squares were shown to the player
   - Recalled squares sequence: index and local positions of the centre of the squares that the player selected
   to recreate the sequence in the order that those squares were selected
   - Recall time: total time duration that player recalls the sequence
   - Square highlight interval: time duration between when one square ends being highlighted and the next square is highlighted
   - Square highlight duration: time duration that a highlighted square is lit up

## Balloons
- Gameplay data for each round:
   - Circle size
   - Destination point: position of the centre of the destination balloons (2nd circle of the round)
   - Clicks information: time taken and position for each unsuccessful click
   - Destination click time: elapsed time between when the destination balloons appears and when the player successfully clicks on it.
   - Successful click position: position of the click on the destination balloons


## Catch The Thief
- Gameplay data for each round:
   - Whether the key is pressed
   - Key press time: amount of time player takes to react to images when the image appears on screen
   - Whether a thief image appeared
   - Whether a person image appeared

## Image Hit
- Gameplay data for each round:
   - Whether the key is pressed
   - Key press time: amount of time player takes to react to images when the image appears on screen
   - Whether the recognition of the player is correct for the current image
   - Whether the space bar is pressed by the player for the current image
   - Whether the pressed bar is space bar
   - Other unidentified keys pressed during the round
   - The theme of the current image
   - The name of the current image
   - The specified theme of the this round of game


## Ball Mini-Games
### Catch The Ball
- Gameplay data for each round:
   - The radius of the initial ball
   - The actual time to contact
   - The predicted time to the contact


### Save One Ball
- Gameplay data for each round:
   - The radius of the initial left ball 
   - The radius of the initial right ball 
   - The actual time to the contact for the left ball
   - The actual time to the contact for the right ball
   - Whether the player make a correct prediction for the first-arriving ball
   - The predicted time of the contact


### Judge The Ball
- Gameplay data for each round:
   - The radius of the initial slow ball
   - The radius of the initial fast ball
   - The radius of the initial player ball
   - The actual time to the contact for the slow ball
   - The actual time to the contact for the fast ball
   - The actual time to the contact for the player ball
   - The time before the ball disappears
   - The predicted time of the contact


# Measurement results:
   - Sub-score sequence: a sequence of measured scores for each ability in each game (multiple games could measure the same ability; that's why we call the measured score from one game `sub-score`).
   - Overall score requence: a sequence of measured scores for each ability from all games that measure the ability









# Scoring Mechanism
This scoring of each ability is designed such that a **normal player** would get a score of approximately **50**.

## Squares
The score will be calculated based on:
- Sufficient rounds completed
   - A normal player should complete 5 rounds.
- Length of sequences of highligted squares in each round
   - The longer the length is, the more difficult the current level is, thus the higher potential mark a player will get in the current round
- Time spent to recreate the sequence in each round
   - The shorter time spent, the higher the mark that will be given
- If the player recreates the sequence successfully in each round
   - There will be mark cutdown for missing a square or selecting a wrong square
   - If the player successfully recreates a sequence that is long enough (its length >= 5), they will get bonus marks



## Balloons
A **Mouse** is required for a player to get an accurate score that correctly reflects their competency. Players using the touchpad tend to get a lower score.

Situations that would result in a cut down in score:
- Slow click on the destination balloon in each round;
- Unsuccessful click on the destination balloon in each round;
- Successful click but not close to the centre of the destination balloon;
- Not enough rounds completed



## Catch The Thief
The score will be calculated based on:
- If the player correctly reacts to the images for each round.
- The amount of time player takes to react to images.

## Image Hit
The score will be calculated based on:
- If the player correct regonized the current image
- If the player recognized the image of specified theme correctly, the reaction time of him/her
- If all the keypressing actions during the game are intentional

## Ball Mini-Games
### Catch The Ball
The score will be calculated based on:
- The player's reaction time to the disappearance of the soccer ball compared to the estimated time


### Save One Ball
The score will be calculated based on:
- The player's accuracy in judging the first soccer to arrive


### Judge The Ball
The score will be calculated based on:
- The time when the player moves a slider, compared with the estimated time when the player’s ball arrives


