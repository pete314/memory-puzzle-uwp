# Introduction
**Memory puzzle game - C# Universal Windows Platform application**
Memory puzzle is a game where the player has to find image pairs by "flipping" pictures one at the time. The goal of the game is to find all image pairs in the shortest possible time. The player earns scores for each pair revield, which are calculated based on the difficulty, board size of the actual gameplay.

##Technical background
The application is written in C# programming language targeting the latest .net version, 4.6. The application is designed with the use of the MVVM pattern, Model-View-ViewModel and tries to follow the best practeses for a application development to reach a fast application with maintainable codebase. Dynamic code was a key part of the development process, to avoid as many hard coded values as possible. This resulted in easy extendabe game (eg.: simply copy more folders into Images/ and it will be visible as colection).
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
![Class relations](https://raw.githubusercontent.com/pete314/memory-puzzle-uwp/master/docs/img/memory_puzzle_class__relations.PNG?token=AIYB_GW-GssaUb-R7Fh3lKMPMrWg4EhGks5YTBuQwA%3D%3D "Memory puzzle class relations")


##Setting upo the project
**Dependacies:**
- Golang
- MongoDb
- .NET 4.6+
- Code editor (VS Code, VS 2015+, VS Community, ReSharp etc.)

**UWP:**
- Clone the repository
- Open the memory-puzzle-uwp/memory-puzzle-uwp.sln file
- Open Nuget package manager and install/update the dependacies(System.Data.SQLite, SQLite.Net-PCL, Newtonsoft.Json, Microsoft.NETCore.UniversalWindowsPlatform, Microsoft.Azure.Mobile.Client.SQLiteStore [optional])
- Compile and run

**REST API:**
- Open cmd or any cli emulator
- Navigate to the repository root and memory-puzzle-api/
- Set go-path to some location on disk (can be default) and double check it with 'go env' command
- Start mongodb is not running, on windows I would suggest to set the path for mongod and then execute 'mongod --dbpath c:\YOUR\mongodb\data'
- Start the API with 'go run runner.go' or compile with 'go install runner.go' and run the executable

## Disclaimer

THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.

