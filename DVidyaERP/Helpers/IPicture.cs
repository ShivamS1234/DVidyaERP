namespace DVidyaERP.Helpers
{
    public interface IPicture
    {
        void SavePictureToDisk(string filename, byte[] imageData);
        string GetPictureFromDisk(string id);
    }
}
