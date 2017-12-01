using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Sprut;
using ImageStoreConsole.ImageService;

namespace ImageStoreConsole
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ServiceClient _imageStoreRepository = new ServiceClient();

            string select = null;
            do
            {
                //Console.Clear();
                Console.WriteLine("Main menu");
                Console.WriteLine("1. Add new root folder");
                Console.WriteLine("2. Add new folder 2 level");
                Console.WriteLine("3. Add new folder 3 level");
                Console.WriteLine("4. Upload JPG file to root");
                Console.WriteLine("5. GetImageUrl");
                Console.WriteLine("0. Exit");
                select = Console.ReadLine();

                switch (select)
                {
                    case "1":
                        _imageStoreRepository.AddFolder("Folder num.1","");
                        break;
                    case "2":
                        _imageStoreRepository.AddFolder("Folder 2 level", "Folder num.1");
                        break;
                    case "3":
                        _imageStoreRepository.AddFolder("Folder 3 level", "Folder num.1\\Folder 2 level");
                        break;
                    case "4":
                        OpenFileDialog openFileDialog=new OpenFileDialog();
                        openFileDialog.Filter = "jpg files (*.jpg)|*.jpg";
                        openFileDialog.ShowDialog();
                        byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                        _imageStoreRepository.UploadImage(imageBytes, openFileDialog.FileName, "");
                        break;
                    case "5":
                        Console.WriteLine(_imageStoreRepository.GetImageUrl(Guid.Parse("A9EA5375-FCE0-4E90-A5B3-408B5F53C412")));
                        break;
                }
            } while (select != "0");

        }
    }
}
