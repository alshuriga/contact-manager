namespace ContactManagerTest.Services;

public interface ICsvDeserializerService<T>
{
    List<T> Deserialize(string csv);
}
