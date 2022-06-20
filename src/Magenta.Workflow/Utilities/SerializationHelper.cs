using System.Text.Json;

namespace Magenta.Workflow.Utilities;

public static class SerializationHelper
{
    public static string SerializeJson(this object type)
    {
        return JsonSerializer.Serialize(type);
    }

    public static T DeserializeJson<T>([NotNull]this string input)
    {
        return JsonSerializer.Deserialize<T>(input);
    }
}