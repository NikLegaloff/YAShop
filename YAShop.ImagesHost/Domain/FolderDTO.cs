using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Dapper;

namespace YAShop.ImagesHost.Domain
{
    public class ImagesHelper
    {
        public void DeleteImageFile(Guid id)
        {
            File.Delete(GetImagePath(id));
            File.Delete(GetTmbPath(id));
        }

        public static string GetImagePath(Guid id)
        {
            return
                $"{Env.Current.ImagesBasePath}{id.ToString().Substring(0, 2)}\\{id.ToString().Substring(2, 4)}\\{id}.jpg";
        }

        public static string GetTmbPath(Guid id)
        {
            return
                $"{Env.Current.ImagesBasePath}{id.ToString().Substring(0, 2)}\\{id.ToString().Substring(2, 4)}\\{id}_tmb.jpg";
        }

        public Guid UploadFile(string name, byte[] data, Guid? folderId)
        {
            var hash = GetHash(data);
            var size = data.Length;
            var existing = DB.ExecuteScalar<Guid?>("select id from image where hash=@hasm and size=@size",
                new {hash, size});
            if (existing != null) return existing.Value;
            var id = Guid.NewGuid();
            Resize(data, GetImagePath(id), 2000);
            Resize(data, GetTmbPath(id), 150);
            return id;
        }

        private void Resize(byte[] data, string path, int maxWidth)
        {
            if (File.Exists(path)) File.Delete(path);
            using (var ms = new MemoryStream(data))
            {
                using (var g = System.Drawing.Image.FromStream(ms))
                {
                    if (g.Width > maxWidth)
                    {
                        var thumbSize = new Size(g.Width, g.Height);

                        if (thumbSize.Width > maxWidth)
                            thumbSize = new Size(maxWidth, thumbSize.Height * maxWidth / thumbSize.Width);
                        using (var imgOutput = new Bitmap(thumbSize.Width, thumbSize.Height))
                        {
                            imgOutput.MakeTransparent();
                            imgOutput.MakeTransparent(Color.Black);
                            using (var newGraphics = Graphics.FromImage(imgOutput))
                            {
                                newGraphics.CompositingQuality = CompositingQuality.HighQuality;
                                newGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                newGraphics.Clear(Color.FromArgb(255, 255, 255, 255));
                                newGraphics.DrawImage(g, 0, 0, thumbSize.Width, thumbSize.Height);
                                newGraphics.Flush();
                            }
                            var parameters = new EncoderParameters(1);
                            var parameter = new EncoderParameter(Encoder.Quality, 81);
                            parameters.Param[0] = parameter;
                            var codec = GetImageCodecInfo(ImageFormat.Jpeg);
                            imgOutput.Save(path, codec, parameters);
                        }
                    }
                    else
                    {
                        File.WriteAllBytes(path, data);
                    }
                }
            }
        }

        private static ImageCodecInfo GetImageCodecInfo(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();

            foreach (var codec in codecs)
                if (codec.FormatID == format.Guid)
                    return codec;

            return null;
        }


        private Guid GetHash(byte[] data)
        {
            using (var md5Hasher = MD5.Create())
            {
                return new Guid(md5Hasher.ComputeHash(data));
            }
        }
    }

    public class FoldersHelper
    {
        private List<FolderDTO> _all;

        public List<Folder> SelectTree()
        {
            using (var db = DB.Open())
            {
                _all = db.Query<FolderDTO>("select * from Folder").ToList();
                var tree = new List<Folder>();
                tree.AddRange(_all.Where(a => a.ParentId == null).Select(GetFolderFor));
                return tree;
            }
        }

        private Folder GetFolderFor(FolderDTO a)
        {
            return new Folder
            {
                Id = a.Id,
                Name = a.Name,
                Childrens = _all.Where(_ => _.ParentId == a.Id).Select(GetFolderFor).ToList()
            };
        }
    }

    public class FolderDTO
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
    }

    public class ImageDTO
    {
        public Guid Id { get; set; }
        public Guid? FolderId { get; set; }
        public string Name { get; set; }
        public Guid Hash { get; set; }
        public int Size { get; set; }
    }
}