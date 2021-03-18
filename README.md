# Battleship game



### Building the project

In order to build a project one has to run `dotnet build` command on a solution file.
Assuming the command is run from the root repository folder:
```bash
dotnet build
```
If the command is run outside the root folder:
```bash
dotnet build <folder containing solution>/Battleship.sln
```

### Running the project
To run a project `dotnet run` command needs to be executed. Path to `ConsoleBattleships`
project file should be passed as a parameter:
```bash
dotnet run --project <folder containing solution>/ConsoleBattleships/ConsoleBattleships.csproj 
```
### Symbols used in the game 

* Clear (cell have not been shot at) => `' '`
* Hit ship => `H`
* Sunk ship => `S`
* Missed shot => `M`

### Known issues
Sometimes to make the board appear in the console any key has to be pressed so that board is refreshed.

### Test coverage and maintainability
[![Coverage Status](https://coveralls.io/repos/github/L-Sypniewski/Battleship/badge.svg?branch=master)](https://coveralls.io/github/L-Sypniewski/Battleship?branch=master)
[![Maintainability](https://api.codeclimate.com/v1/badges/36304294bd82f78a6880/maintainability)](https://codeclimate.com/github/L-Sypniewski/Battleship/maintainability)
