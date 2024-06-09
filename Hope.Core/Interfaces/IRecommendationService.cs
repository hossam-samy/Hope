using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Interfaces
{
    public interface IRecommendationService
    {
        public Task<int> predict(double longitude, double latitude);
    }
}
