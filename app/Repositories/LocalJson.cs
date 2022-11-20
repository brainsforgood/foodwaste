using System.Text.Json;

namespace Foodwaste.Repositories
{
    public class LocalJson<T>
    {
        private readonly string filePath;
        public LocalJson(string filePath) => this.filePath = filePath;

        public T Read()
        {
            var jsonString = File.ReadAllText(this.filePath);
            return JsonSerializer.Deserialize<T>(jsonString);
        }

        public void Write(T obj)
        {
            var jsonString = JsonSerializer.Serialize(obj);
            File.WriteAllText(this.filePath, jsonString);
        }
    }
}
