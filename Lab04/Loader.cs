public class Loader<T>
{
    public List<T> LoadList(string path, Func<string[], T> generate)
    {
        var list = new List<T>();
        foreach (var line in File.ReadLines(path).Skip(1))
        {
            var fields = line.Split(',');
            list.Add(generate(fields));
        }
        return list;
    }
}
