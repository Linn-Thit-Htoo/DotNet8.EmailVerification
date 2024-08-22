namespace DotNet8.EmailVerification.Shared;

public static class DevCode
{
    public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);

    public static T ToObject<T>(this string jsonStr) => JsonConvert.DeserializeObject<T>(jsonStr)!;
}
