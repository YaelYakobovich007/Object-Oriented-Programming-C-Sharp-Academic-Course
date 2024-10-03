<h1 align="center">
  <img src="https://user-images.githubusercontent.com/45564632/71564538-fa761300-2a6f-11ea-8328-aa99d9b6b854.gif" width="30%" alt="logo"/>
  <br/>
  Welcome to Memory Card Game
  <img src="https://media.giphy.com/media/hvRJCLFzcasrR4ia7z/giphy.gif" width="5%" alt="waveEmoji"/>
</h1>

<h2 align="center">
   A console-based memory card game developed using C#, featuring Human vs Human and Human vs AI gameplay, built with Object-Oriented Programming (OOP) principles.
    <br/>    <br/> 
</h2>

<br/>

# Table of Contents
* [Overview](#overview)
* [Features](#features)
* [Technical Stack](#technical-stack)
* [Class Structure](#class-structure)
* [How to Play](#how-to-play)
* [Installation](#installation)
* [Run](#run)
* [Credits](#credits)

<br/>

# :heavy_check_mark: **Overview** <a name="overview"/>

This project is part of the **Object-Oriented Programming in C# Academic Course**, focusing on building a **Memory Card Game**. The game implements core OOP principles and provides multiple gameplay modes, including Human vs Human and Human vs AI, with adjustable board sizes and AI difficulty levels for enhanced gameplay experience.

<br/>

# :heavy_check_mark: **Features** <a name="features"/>

1. **Gameplay Modes**:
   - **Human vs Human**: Two players take turns flipping cards to find matching pairs.
   - **Human vs AI**: Play against a computer opponent with customizable difficulty levels implemented in the `ComputerAi` class.
   
2. **Customizable Game Board**:
   - Players can select board sizes: 4x4, 4x6, or 6x6, offering varying levels of complexity and challenge.
   
3. **Score Tracking**:
   - The game keeps track of each player's score based on the number of matched pairs found, managed by the `Player` class.
   
4. **Real-Time Feedback**:
   - Immediate feedback is provided when cards are flipped, showing whether a pair was matched. The `SingleGameManager` class handles real-time updates and game flow.
   
5. **End Game Summary**:
   - At the end of the game, a summary is displayed, announcing the winner and showing the final scores, processed by the `GameManagerUI` class.

<br/>

# :gear: **Technical Stack** <a name="technical-stack"/>

- **Language**: C#  
- **Framework**: .NET  
- **IDE**: Visual Studio  
- **Project Type**: Console-based application

<br/>

# :clipboard: **Class Structure** <a name="class-structure"/>

1. **Player.cs**: Represents a player in the game, storing their name and current score. The class tracks each player’s successful matches and total score.
2. **ComputerAi.cs**: Implements AI logic for the computer opponent, using a decision-making engine that varies based on difficulty levels.
3. **Card.cs**: Represents each card on the game board, including its symbol and state (flipped or unflipped). This class manages the state of individual cards.
4. **Board.cs**: Manages the grid layout of the cards and handles logic for card placement, matching, and shuffling. It ensures that cards are correctly arranged and flipped.
5. **SingleGameManager.cs**: Oversees the overall game flow, managing player turns, updating scores, and determining when the game ends. It acts as the controller for game operations.
6. **GameManagerUI.cs**: Handles user input and output via the console, providing instructions, game status updates, and displaying the end game summary.
7. **GameInputHandler.cs**: Deals with gathering player input and processing moves within the game. This class ensures that inputs are valid and appropriately handled.
8. **PrintHandler.cs**: Manages the formatting and display of game information on the console, ensuring a clear and consistent user experience.
9. **UICard.cs**: Responsible for the visual representation of cards on the console, translating the card state into a user-friendly format.

<br/>

# :video_game: **How to Play** <a name="how-to-play"/>

1. **Start the Game**: Run the program, and you'll be prompted to select the board size.
2. **Player Selection**: Choose to play against another player or against the computer (AI).
3. **Game Rounds**: Players take turns flipping two cards. If a match is found, the cards stay face-up, and the player earns points. If no match is found, the cards flip back over.
4. **Winning**: The player with the most matched pairs wins. The end-game screen displays scores and announces the winner, or declares a tie if the scores are equal.

<br/>

# :wrench: **Installation** <a name="installation"/>

1. Download the project files from the repository.
2. Set up your development environment with **C#** and **.NET** support (Visual Studio recommended).
3. Ensure that all dependencies are installed and the project is configured for a console-based application.

<br/>

# :arrow_forward: **Run** <a name="run"/>

1. Open the project in **Visual Studio**.
2. Run the `SingleGameManager` class to start the game.
3. Follow the on-screen prompts to set up the game (board size, player vs player or player vs AI).

<br/>

# :trophy: **Credits** <a name="credits"/>
> Created by: Yael Yakobovich

<br/>
