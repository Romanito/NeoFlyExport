using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NeoFlyExport
{
    /// <summary>
    /// Command line utilities
    /// </summary>
    public static class CommandLine
    {
        private static Dictionary<string, string> _args;

        /// <summary>
        /// Simple command line arugments parser
        /// Supports --key=value and /key=value syntaxes
        /// </summary>
        public static Dictionary<string, string> Arguments
        {
            get
            {
                if (_args == null)
                {
                    _args = new Dictionary<string, string>();
                    Regex reKeyValue = new Regex(@"^(--|/)(\w*)=(.*)$", RegexOptions.IgnoreCase);
                    foreach (string arg in Environment.GetCommandLineArgs())
                    {
                        Match keyValueMatch = reKeyValue.Match(arg);
                        if (keyValueMatch.Success && keyValueMatch.Groups.Count == 4)
                        {
                            _args.Add(keyValueMatch.Groups[2].Value.ToLower(), keyValueMatch.Groups[3].Value);
                        }
                    }
                }
                return _args;
            }
        }
    }
}
