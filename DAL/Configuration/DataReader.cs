using Newtonsoft.Json;

namespace Bookstore.DAL.Configuration;
public class DataReader<T>
{
    protected List<T>? readData(string dataFile)
    {
        string str = File.ReadAllText(dataFile);
        List<T>? elements = JsonConvert.DeserializeObject<List<T>>(str);
        return elements;
    }
}