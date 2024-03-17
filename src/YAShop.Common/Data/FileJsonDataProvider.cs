using Newtonsoft.Json;
using YAShop.Common.Domain;

namespace YAShop.Common.Data;

public class FileJsonDataProvider<T> : IDataProvider<T> where T : DomainObject
{
    private readonly string? _dataPath; 

    public FileJsonDataProvider(string? dataPath = null)
    {
        _dataPath = dataPath;
    }

    public T[] SelectAll()
    {
        var res = new List<T>();
        var files = Directory.GetFiles(GetDirectoryPath());
        foreach (var file in files)
        {
            var T = Load(file);
            if (T != null) res.Add(T);
        }
        return res.ToArray();
    }

    public T? Find(Guid id) => Load(GetFilePath(id));
    
    protected T? Load(string path)
    {
        if (!File.Exists(path)) return null;
        return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
    }

    public void Save(T subj)
    {
        if (subj.Id == Guid.Empty) subj.Id = Guid.NewGuid();
        if (!Directory.Exists(GetDirectoryPath())) Directory.CreateDirectory(GetDirectoryPath());
        File.WriteAllText(GetFilePath(subj.Id), JsonConvert.SerializeObject(subj, Formatting.Indented));
    }

    public void Delete(Guid id)
    {
        var path = GetFilePath(id);
        if (File.Exists(path)) File.Delete(path);
    }

    private string GetFilePath(Guid id) => _dataPath + "\\" + typeof(T).Name + "\\" + id + ".json";
    private string GetDirectoryPath() => _dataPath + "\\" + typeof(T).Name + "\\";
}