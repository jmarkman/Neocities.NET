﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeocitiesNET.Options
{
    [Verb("get", HelpText = "Retrieve information about Neocities websites")]
    public class ApiGet
    {
        [Option('f', "allfiles", HelpText = "Retrieves a list of all the files on the website")]
        public bool ListAllFiles { get; set; }

        [Option('F', "filesin", HelpText = "Get all files from a specified directory")]
        public string ListFilesFromDirectory { get; set; }

        [Option('a', "mysitedata", HelpText = "Get your own website's metadata")]
        public bool GetPersonalMetadata { get; set; }

        [Option('A', "sitedatafor", HelpText = "Get a specified site's metadata")]
        public string GetSiteMetadata { get; set; }

        [Option('k', "apikey", HelpText = "If you're using account:password auth, this will get your site's API key, or generate one for you")]
        public bool GetApiKey { get; set; }

    }

    [Verb("modify", HelpText = "Modify the files on your Neocities website")]
    public class ApiModify
    {

        [Option('u', "upload", HelpText = "Provide a complete path to a file to upload it to your website")]
        public string UploadFile { get; set; }

        [Option('d', "delete", Separator = ':', HelpText = "Delete a file or number of files from your website")]
        public IEnumerable<string> DeleteFiles { get; set; }
    }
}
