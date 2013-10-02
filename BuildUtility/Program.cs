namespace BuildUtility
{
    using System;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var buildUtilityOpions = new BuildUtilityOption();

            var argumentIndex = 0;
            var parameter = string.Empty;
            var command = string.Empty;
            while (argumentIndex < args.Length)
            {
                parameter = args[argumentIndex];
                if (parameter.StartsWith("/"))
                {
                    command = parameter.ToLower();
                    switch (command)
                    {
                        case "/sf":
                            buildUtilityOpions.ScanSubFolders = true;
                            command = string.Empty;
                            break;
                        case "/m":
                            buildUtilityOpions.MinificationRequired = true;
                            command = string.Empty;
                            break;
                        case "/vo":
                            buildUtilityOpions.VerboseOutput = true;
                            command = string.Empty;
                            break;
                        case "/u":
                            buildUtilityOpions.UpdateVersion = true;
                            command = string.Empty;
                            break;
                        case "/?":
                            //TO DO: Display Help
                            break;
                    }
                }
                else if (!string.IsNullOrEmpty(command))
                {
                    switch (command)
                    {
                        case "/rf":
                            buildUtilityOpions.RootFolder = parameter;
                            break;
                        case "/v":
                            buildUtilityOpions.Version = parameter;
                            break;
                        case "/ct":
                            buildUtilityOpions.ContentFileTypes = parameter;
                            break;
                        case "/tt":
                            buildUtilityOpions.TartgetFileTypes = parameter;
                            break;
                    }
                    command = string.Empty;
                }
                argumentIndex++;
            }

            if (string.IsNullOrEmpty(buildUtilityOpions.ContentFileTypes) ||
                    string.IsNullOrEmpty(buildUtilityOpions.RootFolder) ||
                    string.IsNullOrEmpty(buildUtilityOpions.TartgetFileTypes) ||
                    string.IsNullOrEmpty(buildUtilityOpions.Version))
            {
                Console.WriteLine("BuildUtility -> Please pass all mandatory parameters.");
                System.Environment.Exit(1);
            }
            else
            {
                using (var buildUtility = new BuildUtility())
                {
                    if (buildUtility.VersionStaticContentUrl(buildUtilityOpions))
                    {
                        Console.WriteLine("BuildUtility -> Task completed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("BuildUtility -> Task completed with errors! Please check the log file for more deails.");
                    }
                }
                System.Environment.Exit(0);
            }
        }
    }
}
