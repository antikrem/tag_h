
namespace tagh.Client.Model
{
    public class HImageClientData
    {
        public byte[] Data { get; }
        public string DataBase64 => System.Convert.ToBase64String(Data);
        public string Extension { get; }

        internal HImageClientData(byte[] data, string extension)
        {
            Data = data;
            Extension = extension;
        }


    }
}
