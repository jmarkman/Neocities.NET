# Neocities.NET

## Interact with the Neocities API!

While there are several Neocities command line programs and libraries out there, I wanted one that would be able to support users who were Neocities supporters and had multiple websites available.

Some recommendations:

- Use this command line utility alongside whichever shell scripting language is your favorite to encase calls to the CLI in easy-to-call keywords
- If you're a Windows user and need a shell scripting language, try out PowerShell!
- While `username:password@neocities.org/api` authentication method is supported, I personally prefer the API key method of authentication since it has a far lower chance of being a credential associated with something other than Neocities, and an API key is easier to change if your key is somehow compromised

## Installation

### From Source

Neocities.NET was made using .NET Core 3.1 and Visual Studio 2019, so ensure you have the most recent .NET Core SDK installed as well as VS 2019

1. Clone this repository, i.e., `git clone https://github.com/jmarkman/Neocities.NET.git`
2. Build with the Release configuration in VS 2019 *or* navigate to the project directory from the command line and type `dotnet build --configuration Release`
3. Copy the contents of the `./Neocities.NET/bin/Release` folder to a reasonable location on your computer
4. Add the NeocitiesNet executable to your PATH variable

### From Releases

1. Download the release
2. Extract the contents to a reasonable location on your computer
3. Add the NeocitiesNet executable to your PATH variable

## CLI Quick-Start

### Inline Help

`neocitiesnet --help`

### Add your website's API key

`neocitiesnet account --addkey [Username/Website Name]:[API key]`

### See all the files currently on your website

`neocitiesnet get --allfiles`

### View the metadata about your website

`neocitiesnet get --mysitedata`

## More Help

Please consult the [wiki](https://github.com/jmarkman/Neocities.NET/wiki) for more in-depth information about using this tool.
