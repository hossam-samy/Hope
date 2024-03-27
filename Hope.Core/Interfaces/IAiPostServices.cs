using Hope.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Interfaces
{
    public interface IAiPostServices
    {
        Task<Response> GetPostOfPeopleByTown(string town);
        Task<Response> GetPostOfPeopleByAge(int age);
        Task<Response> GetPostOfThingsByTown(string town);
        Task<Response> GetPostOfPeopleByTownandAge(int age, string town);


    }
}
