using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ImageStore;

namespace ImageStoreConsole
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ImageStoreRepository imageStoreRepository=new ImageStoreRepository();

            string select = null;
            do
            {
                //Console.Clear();
                Console.WriteLine("Main menu");
                Console.WriteLine("1. Add new root folder");
                Console.WriteLine("2. Add new folder 2 level");
                Console.WriteLine("3. Add new folder 3 level");
                Console.WriteLine("4. Upload JPG file");
                Console.WriteLine("5. ");
                Console.WriteLine("0. Exit");
                select = Console.ReadLine();

                switch (select)
                {
                    case "1":
                        imageStoreRepository.AddFolder("Folder num.1","");
                        break;
                    case "2":
                        imageStoreRepository.AddFolder("Folder 2 level", "Folder num.1");
                        break;
                    case "3":
                        imageStoreRepository.AddFolder("Folder 3 level", "Folder num.1\\Folder 2 level");
                        break;
                    case "4":
                        OpenFileDialog openFileDialog=new OpenFileDialog();
                        openFileDialog.Filter = "jpg files (*.jpg)|*.jpg";
                        openFileDialog.ShowDialog();
                        byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                        imageStoreRepository.UploadImage(imageBytes, "69BAB117-7ED0-47F3-8D6F-01A9593CE5D1", "Folder num.1\\Folder 2 level");
                        break;

                }
            } while (select != "0");

        }
    }
}
