using Hope.Core.Common;

namespace Hope.Core.Interfaces
{
    public interface IFaceRecognitionService
    {
        public  Task<Response> predict(string path);

    }
}
