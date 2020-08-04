using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeocitiesNET.Options
{
    [Verb("get", HelpText = "Retrieve information about Neocities websites")]
    public class ApiGet
    {
        [Option(HelpText = "Retrieves a list of all the files on the website")]
        public bool ListAllFiles { get; set; }

        [Option(HelpText = "Get all files from a specified directory")]
        public string ListFilesFromDirectory { get; set; }

        [Option(HelpText = "Get your own website's metadata")]
        public bool GetPersonalMetadata { get; set; }

        [Option(HelpText = "Get a specified site's metadata")]
        public string GetSiteMetadata { get; set; }

        [Option(HelpText = "If you're using account:password auth, this will get your site's API key, or generate one for you")]
        public bool GetApiKey { get; set; }

    }

    [Verb("modify", HelpText = "Modify the files your Neocities website")]
    public class ApiModify
    {

        [Option("upload", HelpText = "Provide a complete path to a file to upload it to your website")]
        public string UploadFile { get; set; }

        [Option("delete", Separator = ':', HelpText = "Delete a file or number of files from your website")]
        public IEnumerable<string> DeleteFiles { get; set; }
    }
}
