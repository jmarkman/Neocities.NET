# Neocities.NET

## Interact with the Neocities API!

While there are several Neocities command line programs and libraries out there, I wanted one that would be able to support users who were Neocities supporters and had multiple websites available. I also provide a few underlying changes to how site files are listed:
- I don't pump out the raw json
- Creation/Updated dates are formatted according to the current culture and are displayed in a slightly more natural format (I use the [full date short time format](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#the-full-date-short-time-f-format-specifier))