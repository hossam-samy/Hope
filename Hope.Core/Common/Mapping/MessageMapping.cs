using Hope.Core.Features.MessageOperation.Queries.GetLatiestMessages;
using Hope.Domain.Model;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Common.Mapping
{
    public class MessageMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Message,GetLatiestMessagesQueryResponse>().Map(i=>i,i=>i.Recipient);
         }
    }
}
