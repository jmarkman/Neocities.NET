using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NeocitiesApi
{
    public static class Extensions
    {
        /// <summary>
        /// Given a valid path, determine if the path is a directory
        /// </summary>
        /// <param name="path">The path to check</param>
        /// <returns><see cref="true"/> if the path is a directory, <see cref="false"/> otherwise</returns>
        public static bool IsDirectory(this string path)
        {
            FileAttributes attr = File.GetAttributes(path);

            if (attr.HasFlag(FileAttributes.Directory))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Given a valid path, convert the Windows directory separators to Unix-like directory
        /// separators, as Neocities uses Unix-style directory separators for its filepaths
        /// </summary>
        /// <param name="path">The path to convert</param>
        /// <returns>The path, with Windows backslashes replaced with Unix forward-slashes</returns>
        public static string ConvertWindowsDirectorySeparatorToUnixSeparator(this string path)
        {
            return path.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }
    }
}
