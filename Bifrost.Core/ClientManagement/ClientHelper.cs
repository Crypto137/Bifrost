namespace Bifrost.Core.ClientManagement
{
    public static class ClientHelper
    {
        private const string ClientPathOverrideFile = "ClientPathOverride.txt";

        public static string GetClientPath()
        {
            if (File.Exists(ClientPathOverrideFile))
                return File.ReadAllText(ClientPathOverrideFile);

            return Directory.GetCurrentDirectory();
        }
    }
}
