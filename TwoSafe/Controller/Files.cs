using System.IO;
namespace TwoSafe.Controller
{
    class Files
    {
        public static void RemoveOnClient(string name, string dir_id)
        {
            Model.File file = Model.File.FindByNameAndParentId(name, dir_id);
            string path = file.GetPath();
            File.Delete(path);
            file.Remove();
        }
    }
}
