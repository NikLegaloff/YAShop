using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ImagesStoreHost.Domain;

namespace ImagesStoreHost
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        void AddFolder(string name, string path);

        [OperationContract]
        Folder GetFolder(string name, Guid? parentId);

        [OperationContract]
        List<Image> GetImagesInFolder(Guid? folderId);

        [OperationContract]
        List<Folder> GetSubFolders(Guid? folderId);

        [OperationContract]
        Guid UploadImage(byte[] data, string fileNameOrigin, string folder);

        [OperationContract]
        string GetImageUrl(Guid id);

        [OperationContract]
        string GetTmbUrl(Guid id);
    }


    //// Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    //[DataContract]
    //public class Folder
    //{
    //    [DataMember]
    //    public Guid Id { get; set; }
    //    [DataMember]
    //    public Guid? ParentId { get; set; }
    //    [DataMember]
    //    public string Name { get; set; }
    //}

    //[DataContract]
    //public class Image
    //{
    //    [DataMember]
    //    public Guid Id { get; set; }
    //    [DataMember]
    //    public Guid? Folder { get; set; }
    //    [DataMember]
    //    public string Name { get; set; }
    //}

}
