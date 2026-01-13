namespace FitnessSimulation.Helpers
{
    public static class FileHelpers
    {
        public static bool CheckSize(this IFormFile file,int mb)
        {
            return file.Length < 1024 * 1024 * mb;
        }
        public static bool CheckType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
        public static async Task<string> FileUploadAsync(this IFormFile file,string folderPath)
        {
            string uniqueFileName = Guid.NewGuid().ToString()+file.FileName;
            string path  = Path.Combine(folderPath, uniqueFileName);
            using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return uniqueFileName; 
        }
        public static void FileDelete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
