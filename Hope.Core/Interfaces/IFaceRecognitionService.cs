namespace Hope.Core.Interfaces
{
    public interface IFaceRecognitionService
    {
        public Task<string> predict(string path);

    }
}
