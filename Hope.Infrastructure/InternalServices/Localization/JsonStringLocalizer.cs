using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;


namespace Hope.Infrastructure.Services.Localization
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly IConfiguration _configuration;

        public JsonStringLocalizer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private readonly JsonSerializer serializer = new JsonSerializer();

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value);

            }

        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {

                var actualValue = this[name];
                return !actualValue.ResourceNotFound
                    ? new LocalizedString(name, string.Format(actualValue.Value, arguments)) : actualValue;

            }

        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read);
            using StreamReader reader = new StreamReader(stream);
            using JsonTextReader textReader = new JsonTextReader(reader);
            while (textReader.Read())
            {
                if (textReader.TokenType != JsonToken.PropertyName) continue;

                var key = textReader.Value as string;
                reader.Read();
                var value = serializer.Deserialize<string>(textReader);
                yield return new LocalizedString(key, value);

            }
        }

        private string GetString(string key)
        {

            var filePath = $"wwwroot/Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            var fullFilePath = Path.GetFullPath(filePath);
            if (File.Exists(fullFilePath))
            {
                
                return GetValueFromJson(key, fullFilePath);

            }
            return string.Empty;

        }
        private string GetValueFromJson(string propertyname, string filePath)
        {
            if (string.IsNullOrEmpty(propertyname) || string.IsNullOrEmpty(filePath))
                return string.Empty;
            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read);
            using StreamReader reader = new StreamReader(stream);
            using JsonTextReader textReader = new JsonTextReader(reader);

            while (textReader.Read())
            {
                if (textReader.TokenType == JsonToken.PropertyName && textReader.Value as string == propertyname)
                {
                    textReader.Read();
                    return serializer.Deserialize<string>(textReader);
                }
            }
            return string.Empty;
        }
    }
}
