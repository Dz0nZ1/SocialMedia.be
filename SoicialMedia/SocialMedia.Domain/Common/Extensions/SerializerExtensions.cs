using System.Text.Json;

namespace SocialMedia.Domain.Common.Extensions;

public static class SerializerExtensions
{

    #region Serialization/Deserialization Options

    
    public static readonly JsonSerializerOptions DefaultOptions = new();
    
    public static readonly JsonSerializerOptions SettingsGeneralOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    public static readonly JsonSerializerOptions SettingsWebOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    public static readonly JsonSerializerOptions SettingsHardwareOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public static readonly JsonSerializerOptions SettingsDepthOptions = new()
    {

        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        MaxDepth = 10
    };

    #endregion
    
    
    public static string Serialize(this object? json, JsonSerializerOptions settings) => JsonSerializer.Serialize(json, settings);
    
    public static T? Deserialize<T>(this string json, JsonSerializerOptions settings) => JsonSerializer.Deserialize<T>(json, settings);

    public static bool TryDeserializeJson<T>(this string obj, out T? result, JsonSerializerOptions setting)
    {
        try
        {
            result = Deserialize<T>(obj, setting);
            return true;

        }
        catch (Exception)
        {
            result = default;
            return false;
        }
    }
   
}