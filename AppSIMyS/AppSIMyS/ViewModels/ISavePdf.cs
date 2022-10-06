using System.IO;
using System.Threading.Tasks;

namespace AppSIMyS.ViewModels
{
    public interface ISavePdf
    {
        //Method to save document as a file and view the saved document
       Task SaveAndView(string filename, string contentType, MemoryStream stream, string root);
    }
}