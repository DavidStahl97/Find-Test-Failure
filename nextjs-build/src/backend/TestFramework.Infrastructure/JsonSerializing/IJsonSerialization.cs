namespace TestFramework.Infrastructure
{
    public interface IJsonSerialization
    {
        T Deserialize<T>(string s);
        string Serialize<T>(T entity);
    }
}