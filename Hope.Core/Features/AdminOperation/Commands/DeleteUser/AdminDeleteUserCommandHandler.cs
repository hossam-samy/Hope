using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Hope.Core.Features.AdminOperation.Commands.DeleteUser
{
    internal class AdminDeleteUserCommandHandler : IRequestHandler<AdminDeleteUserCommand, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<AdminDeleteUserCommandHandler> localizer;
        private readonly IUnitofWork work;


        public AdminDeleteUserCommandHandler(UserManager<User> userManager, IStringLocalizer<AdminDeleteUserCommandHandler> localizer, IUnitofWork work)
        {
            this.userManager = userManager;
            this.localizer = localizer;
            this.work = work;
        }

        public async Task<Response> Handle(AdminDeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(command.UserId!);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"].Value);
            }

            await userManager.RemoveFromRolesAsync(user, new List<string> { "User", "Admin" });
            user.Comments?.Clear();
            user.HiddingPeoples?.Clear();   
            user.HiddingThings?.Clear();    
            user.PinningPeoples?.Clear();
            user.PinningThings?.Clear();
            user?.lostPeople?.ForEach(i =>
            {
                i?.PinnedPeoples?.Clear();
                i?.Comments?.Clear();
                i?.HiddenPeoples?.Clear();
                i?.PinnedPeoples?.Clear();
            });

            user?.lostThings?.ForEach(i =>
            {
                i?.HiddenThings?.Clear();    
                i?.Comments?.Clear();
                i?.PinnedThings?.Clear();   
            });
            user?.Notifications?.Clear();    
            user?.RecievedMessages?.Clear(); 
            user?.SentMessages?.Clear(); 
           
            await work.SaveAsync();

            await userManager.DeleteAsync(user);

            
            return await Response.SuccessAsync(localizer["Success"].Value);

        }
    }
}
