# Neocities.NET

## Interact with the Neocities API!

While there are several Neocities command line programs and libraries out there, I wanted one that would be able to support users who were Neocities supporters and had multiple websites available.

Some recommendations:

- Use this command line utility alongside whichever shell scripting language is your favorite to encase calls to the CLI in easy-to-call keywords
- If you're a Windows user and need a shell scripting language, try out PowerShell!
- While `username:password@neocities.org/api` authentication method is supported, I personally prefer the API key method of authentication since it has a far lower chance of being a credential associated with something other than Neocities, and an API key is easier to change if your key is somehow compromised

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
