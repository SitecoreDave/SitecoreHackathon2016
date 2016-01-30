namespace AppBlocks.Settings
{
    public interface ISettings
    {
        string GetSetting(string name, string defaultValue = null, string namespacePrefix = null);
    }
}