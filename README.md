# Team Cook 
<p><b>By:</b> Lim Junxue and Max Ng</p>
<p><b>Project:</b> DinoMight - A 2D platformer with an interactive storyline which aims to promote effective habits that reduces stress.</p>

![poster](docs/dinomight_poster_v3.1.png)
<p><b>Try our game here! --></b> https://maxjx.github.io/dino-might/</p>
<p><b>Watch the game trailer! --></b> https://youtu.be/bUDn65XtAFs</p>
<p><b>Documentation:</b> https://docs.google.com/document/d/1mt6SgyHExIwvO-I2PbNucnTOGOtYRP6EHN-Ybl8bovU/edit?usp=sharing</p>
<p><b>Feature walkthrough:</b> https://drive.google.com/file/d/1SkLr3tsnfdUyzKQEqfHlY21flEVr7oSm/view?usp=sharing</p>
<p><b>More details on the build:</b> </p>

## Project Overview
### Motivation:
<p>
Stress in Singapore is a significant issue considering that 92% of Singaporeans report feelings of stress, with some even indicating unmanageable levels of stress.
</p>
<p>
A persistent high level of stress will narrow a person’s ability to think, cope and to function effectively. Oftentimes, poor management of stress can be attributed to poor stress management techniques or simply being unaware of good stress relief habits. There is a need to raise awareness on the various ways that can effectively combat stress.
</p>
<p>
Interested in the mechanics of game design and raising awareness through story-telling, we seek to create an interactive fantasy world where the user controls a dino as the main character. This is a 2D adventure/educational game with an interactive storyline which changes based on the player’s decisions that brings the player through a journey of mental self-discovery.
</p>

### Core features: 
<ol><dl>
  <li>Game account creation:
    <dd>- Players can save their data within the game and continue from where they left off by logging back in to their accounts.</dd>
    <dd>- The user’s accounts will have to be saved to a database.</dd>
  </li>
  <li>Menu: 
    <dd> - Change movement keys and volume.</dd>
    <dd>- Players can pause the game and access the options menu. The player can then resume the game through the pause menu.</dd>
  </li>
  <li>Gameplay:
    <dd>- Multiple endings based on the user’s decisions.</dd>
    <dd>- There will be multiple levels which will be loaded as different ‘scenes’ in unity.</dd>
    <dd>- The character moves around using the keyboard and at certain points will trigger a popup that displays a continuation of the storyline.</dd>
  </li>
  <li>Enemy Artificial Intelligence:
    <dd>- Basic enemy mobs will path find to the player using the A* algorithm.</dd>
    <dd>- Enemies respawn after a set amount of time.</dd>
    <dd>- Employs the state machine to switch between chasing and attacking, in line with its animations.</dd>
  </li>
  <li>Visual novel style character conversations:
    <dd>- The NPC(non playable characters) will provide options to the players upon which decisions made will alter the storyline.</dd>
    <dd>- The NPC remembers the state of the storyline.</dd>
    <dd>- Choices made while interacting with NPCs gives insight into the players performance in stress related activities.</dd>
  </li>
  <li>Boss fights:
    <dd>- Employs state machine and factory pattern to switch between attacks for boss fight.</dd>
    <dd>- Advanced fight patterns and different algorithms such as teleportation around the game map.</dd>
    <dd>- Difficulty will be moderate to invoke a sense of achievement which is crucial for stress relief while not being too difficult as to cause unease.</dd>
  </li>
  <li>Variations on mobs, attacks and platform:
    <dd>- Create different kinds of enemy sprite, enemy attacks, enemy behaviours.</dd>
    <dd>- Create different kinds of platforms such as double jump, moving, 2-way, etc.</dd>
    <dd>- Variations should help keep the player engaged and to have a better experience.</dd>
  </li>
  <li>Character Controls:
    <dd>- The character movements and attacks can be controlled by the keyboard, attacks can also be used on the mouse.</dd>
    <dd>- The character has a different collision box height when crouching to enable the player to sneak past obstacles</dd>
    <dd>- Different material textures has been added to the character which keep changing during runtime to ensure smooth movement up slopes, when stationary, when jumping and when landing diagonally.</dd>
    <dd>- The movement through the story can be triggered via both keys and mouse clicks.</dd>
  </li>
  <li>Music:
    <dd>- The volume of the music and the sound effects can be adjusted separately throughout the game. The adjustment is also linear instead of by decibels.</dd>
    <dd>- Player volume preference can be stored and loaded when logging back in.</dd>
    <dd>- The game music added helps with the immersive and relaxing game experience.</dd>
  </li>
  <li>Mini Stress relief activities:
    <dd>- Short simple stress relief exercises will be implemented within the game play which engages the users.</dd>
    <dd>- At the same time, the users get to experience stress relief while taking a “break” from the game.</dd>
    <dd>- Examples include a short 5 min guided meditation session with voice guide and background music, drawing as art therapy and a guide for massaging acupuncture points.</dd>
  </li>
  <li>Questing System
    <dd>- Able to view current task at hand so that players have an aim to work towards.</dd>
    <dd>- Also used to determine at which stage of the game the player is at, thus unlocking certain levels and advances NPC conversations.</dd>
  </li>
  <li>Parallax background
    <dd>- Parallax backgrounds help to add a sense of depth to a 2D game and this helps make the game more immersive in general.</dd>
    <dd>- The rate of parallax scrolling can be easily changed on the developer’s end.</dd>
  </li>
  <li>Summary page for Stress relief tips:
    <dd>- Collates all choices made in the game and presents it in a concise and digestible format at the end of the game.</dd>
    <dd>- Highlights key takeaways for best practises and tips for stress relief and a simple analysis of game performance.</dd>
  </li>
  <li>Collectibles and inventory system:
    <dd>- Basic enumeration system to keep track of the items, such as keys to a locked door, are recorded intra-scene.</dd>
    <dd>- Global system to keep track to inter-scene item states.</dd>
  </li>
 </dl></ol>

### User stories:
  <u>High priority:</u>
  <ul>
    <li>As a busy user, I want to be able to save my progress so that I can save time.</li>
    <li>As a user who wants to destress, I want to be able to have smooth and frictionless player controls so that I enjoy the game and can focus on the storyline.</li>
    <li>As a user who wants to learn from the game, I want learning points to be delivered in a concise and memorable manner so that my time is spent efficiently.</li>
    <li>As a user, I want to be able to resume a paused game from where i left off.</li>
    <li>As a user, I want to be able to pause the game.</li>
    <li>As a user seeking a novel and thrilling method to destress, I want to have variations in fighting mobs.</li>
    <li>As a busy user, I want the game to load quickly.</li>
    <li>As a stressed user, I want to relax while playing the game.</li>
  </ul>
<u>Low Priority:</u>
  <ul>
    <li>As a user, I want to be able to adjust the game volume.</li>
    <li>As a user, i want to be able to adjust the effects volume.</li>
    <li>As a user, I want my character to get stronger over time.</li>
    <li>As a user who wants to destress, I want to play the game with interesting and enjoyable music.</li>
    <li>As a user who wants to feel fulfilled, the game should not end too fast.</li>
  </ul>
<p><b>Game:</b> https://maxjx.github.io/dino-might/</p>
