# Rocket League Replay Parser CLI

This project has been created in order to allow for future Data Analysis of Rocket League Replays. Currently it is very bare bones but does extract every bit of information from a Replay, you may just need to Program extracting certain parts yourself.


## Installation
Use the following command to Install the tool on your device. Make sure you have proper dependencies

```
dotnet tool install --global RocketLeagueReplayParserCLI
```

## Usage
To use the Command Line tool use the following command

```
rlparse path/to/replay/file/or/folder
```

Here is an Example

```
rlparse 0BF39A3B420B6ED23A054EAE349517E9.replay
```

This will display barebones information about the Replay



## Commands
Commands can be Chained onto the default Command, it will overide the displayed information from the regular usage for more specific info. Multiple Commands can be chained along to display more info per usage.

Commands can be used using the Following method

```
rlparse path/to/file/or/folder --CommandName1 commandArg1 commandArg2 ... --CommandName2 commandArg1 commandArg2
```

### Goals
The Goals Command will display the number of Goals each team has scored, or you can isolate the goals of a certain Player by specifying their name.

#### Usage
To display the Goals from each team use
```
rlparse path/to/file/or/folder --Goals
```

To display an individual Players goals use **Make sure enter the PlayerName with no Spaces (Ex: My Tyranosaur --> MyTyranosaur)**
```
rlparse path/to/file/or/folder --Goals PlayerName
```


### Rename
Creates a Copy of the Replay File Renamed to what the Player Named it in game.

#### Usage
```
rlparse path/to/file/or/folder --Rename
```
