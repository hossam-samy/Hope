using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Quartz;

namespace Hope.Infrastructure.Jobs
{
    [DisallowConcurrentExecution]
    public class DeletingPostsJob : IJob
    {
        private readonly IUnitofWork work;

        public DeletingPostsJob(IUnitofWork work)
        {
            this.work = work;
        }

        public Task Execute(IJobExecutionContext context)
        {
            work.Repository<PostOfLostPeople>().Get(i => i.IsFound).Result.ToList().ForEach(i => i.IsDeleted = true);
            work.Repository<PostOfLostThings>().Get(i => i.IsFound).Result.ToList().ForEach(i => i.IsDeleted = true);

            return Task.CompletedTask;

        }
    }
}
