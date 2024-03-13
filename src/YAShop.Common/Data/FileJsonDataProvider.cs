using Newtonsoft.Json;
using YAShop.Common.Domain;

namespace YAShop.Common.Data;

public class FileJsonDataProvider<T> : IDataProvider<T> where T : DomainObject
{
    private readonly string? _dataPath; // D:\Work\YAShop\src\YAShop.Web.Storefront\wwwroot\data\

    public FileJsonDataProvider(string? dataPath = null)
    {
        _dataPath = dataPath;
    }


    public T[] SelectAll()
    {
        var Ts = new List<T>();
        var enumerable = Directory.GetFiles(_dataPath + "\\" + typeof(T).Name + "\\");
        foreach (var file in enumerable)
        {
            var T = Load(file);
            if (T != null) Ts.Add(T);
        }
        return Ts.ToArray();
    }

    public T? Find(Guid id)
    {
        return Load(GetFilePath(id));
    }

    protected T? Load(string path)
    {
        if (!File.Exists(path)) return null;
        var readAllText = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(readAllText);
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

    private string GetFilePath(Guid id)
    {
        return _dataPath + "\\" + typeof(T).Name + "\\" + id + ".json";
    }
    private string GetDirectoryPath()
    {
        return _dataPath + "\\" + typeof(T).Name + "\\";
    }
}