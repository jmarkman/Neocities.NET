using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeocitiesNET.Options
{
    public class ApiOptions
    {
        [Option("list", HelpText = "Retrieves a list of all the files on the website")]
        public bool ListAllFiles { get; set; }

        [Option("listdir", HelpText = "Get all files from a specified directory")]
        public string ListFilesFromDirectory { get; set; }

        [Option("selfmetadata", HelpText = "Get your own website's metadata")]
        public bool GetPersonalMetadata { get; set; }

        [Option("metadata", HelpText = "Get a specified site's metadata")]
        public string GetSiteMetadata { get; set; }

        [Option("getkey", HelpText = "If you're using account:password auth, this will get your site's API key, or generate one for you")]
        public bool GetApiKey { get; set; }

        [Option("upload", HelpText = "Provide a complete path to a file to upload it to your website")]
        public string UploadFile { get; set; }

        [Option("delete", Separator = ':', HelpText = "Delete a file or number of files from your website")]
        public IEnumerable<string> DeleteFiles { get; set; }
    }
}
