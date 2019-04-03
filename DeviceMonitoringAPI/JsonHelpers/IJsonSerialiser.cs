namespace JsonHelpers
{
    public interface IJsonSerialiser<T>
    {
        string Serialise(T obj);
    }
}
