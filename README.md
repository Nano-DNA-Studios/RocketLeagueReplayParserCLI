# Rocket League Parser CLI

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
rlparse GoldenGoose.replay
```

This will display barebones information about the Replay

![image](https://github.com/Nano-DNA-Studios/RocketLeagueReplayParserCLI/assets/93613553/1fd47db7-4364-4f1c-a673-7d639d7c546a)



## Commands
Commands can be Chained onto the default Command, it will overide the displayed information from the regular usage for more specific info. Multiple Commands can be chained along to display more info per usage.

Commands can be used using the Following method

```
rlparse path/to/file/or/folder --CommandName1 commandArg1 commandArg2 ... --CommandName2 commandArg1 commandArg2
```

---

## Stats Commands
All Stats Commands function extremely similarly. You can use the Stat Name as a Command to display the Teams Stat or can Isolate a single players Stat. The Stat name is Capitalized and has no Spaces

### Usage
To display the Goals from each team use
```
rlparse path/to/file/or/folder --Statname
```

To display an individual Players goals use **Make sure enter the PlayerName with no Spaces (Ex: My Tyranosaur --> MyTyranosaur)**
```
rlparse path/to/file/or/folder --Statname PlayerName
```

### Example

##### Team
```
rlparse path/to/file/or/folder --Goals 
```

##### Player
```
rlparse path/to/file/or/folder --Goals MyTyranosaur
```

---

### Stat Command Options

#### Goals
The Goals Command will display the number of Goals each team has scored, or you can isolate the goals of a certain Player by specifying their name.


#### Assists
The Assists Command will display the number of Assists each team has, or you can isolate the Assists of a certain Player by specifying their name. 


#### Saves
Displays the Team or Players Saves


#### Score
Displays the Team or Players Score


#### Shots
Displays the Team or Players Shots

---

### Other Commands

#### Rename
Creates a Copy of the Replay File Renamed to what the Player Named it in game.

##### Usage
```
rlparse path/to/file/or/folder --Rename
```

#### Save Replay
Saves the Class object Replay created from the Extraction process as a JSON File. Can Specify the Name and the Save Path aswell

##### Usage

**Save Replay at Current Location (Name of file is same as Replay Name**
```
rlparse path/to/file/or/folder --SaveReplay
```

**Save Replay With New Name (Save Same Location)**
```
rlparse path/to/file/or/folder --SaveReplay NewName
```

**Save Replay with new Name and New Location**
```
rlparse path/to/file/or/folder --SaveReplay NewName /new/path/to/save
```

#### Save Psyonix Replay
Saves the Raw Extracted Replay file as a JSON File. Can Specify the Name and the Save Path aswell

##### Usage

**Save Replay at Current Location (Name of file is same as Replay Name**
```
rlparse path/to/file/or/folder --SavePsyonixReplay
```

**Save Replay With New Name (Save Same Location)**
```
rlparse path/to/file/or/folder --SavePsyonixReplay NewName
```

**Save Replay with new Name and New Location**
```
rlparse path/to/file/or/folder --SavePsyonixReplay NewName /new/path/to/save
```

## Creator
Created by [MrDNAlex](https://github.com/MrDNAlex)
