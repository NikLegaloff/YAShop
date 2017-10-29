using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace YAShop.ImagesHost
{
    [ServiceContract]
    public interface IService
    {

        [OperationContract]
        List<Folder> GetFoldersTree();
        [OperationContract]
        List<Image> GetImages(Guid? folderId);
        [OperationContract]

        Guid AddFolder(string name, Guid? parentId);
        [OperationContract]
        Guid UploadImage(string name, byte[] data, Guid? folderId);
        [OperationContract]

        bool RenameFolder(Guid folderId, string newName);
        [OperationContract]
        bool RenameImage(Guid imageId, string newName);

        [OperationContract]
        void DeleteFolder(Guid folderId, Guid? newFolderId);
        [OperationContract]
        void DeleteImage(Guid imageId);
    }


    [DataContract]
    public class Image
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Size{ get; set; }
    }

    [DataContract]
    public class Folder
    {
        [DataMember]
        public Guid Id{ get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<Folder> Childrens{ get; set; }
    }
}
