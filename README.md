# Introduction
**Memory puzzle game - C# Universal Windows Platform application**
Memory puzzle is a game where the player has to find image pairs by "flipping" pictures one at the time. The goal of the game is to find all image pairs in the shortest possible time. The player earns scores for each pair revield, which are calculated based on the difficulty, board size of the actual gameplay.

##Technical background
The application is written in C# programming language targeting the latest .net version, 4.6. The application is designed with the use of the MVVM pattern, Model-View-ViewModel and tries to follow the best practeses for a application development to reach a fast application with maintainable codebase.
The application is broken into two part, the core game UWP and a RESTful API written in Go. The technolgie breakdown is the following:
**UWP - the game**:
- C# (.NET 4.6)
- SQLite local storage
- XAML

**REST API - remote storage**:
- Go (1.6)
- Macaron framework
- MongoDB

##UWP Architecture
The class relations in the project
[Class relations](https://lh3.googleusercontent.com/qaXYZ3gJrgNtgJmwFbcZ9s1RJVBwJVj_iwmha4VffuyFIvGG17Qe8hvO7niBT8NjpPk_UGrn=w1920-h948)
