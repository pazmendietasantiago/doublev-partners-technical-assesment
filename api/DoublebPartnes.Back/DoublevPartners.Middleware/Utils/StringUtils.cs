using System.Text;

namespace DoublebPartnes.Middleware.Utils;

public static class StringUtils
{
    public static string ConvertToBase64FromString(string resource)
    {
        try
        {
            var bytes = Encoding.UTF8.GetBytes(resource);
            var base64 = Convert.ToBase64String(bytes);

            return base64;
        }
        catch (Exception e)
        {
            return string.Empty;
        }
    }

    public static string ConvertToStringFromBase64(string resource)
    {
        try
        {
            var bytes = Convert.FromBase64String(resource);
            var result = Encoding.UTF8.GetString(bytes);

            return result;
        }
        catch (Exception e)
        {
            return string.Empty;
        }
    }
}