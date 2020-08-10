# Neocities.NET

## Interact with the Neocities API!

While there are several Neocities command line programs and libraries out there, I wanted one that would be able to support users who were Neocities supporters and had multiple websites available. I also provide a few underlying changes to how site files are listed:

- I don't pump out the raw json
- Creation/Updated dates are formatted according to the current culture and are displayed in a slightly more natural format (I use the [full date short time format](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#the-full-date-short-time-f-format-specifier))
- Site tags are listed
- File size is specified in kilobytes (base 10) instead of just bytes

Some recommendations:

- Use this command line utility alongside whichever shell scripting language is your favorite to encase calls to the CLI in easy-to-call keywords
- If you're a Windows user and need a shell scripting language, try out PowerShell!
- While `username:password@neocities.org/api` authentication method is supported, the API key method is much more user friendly, and an API key is easier to change if your key is somehow compromised

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