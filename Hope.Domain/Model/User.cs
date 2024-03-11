﻿using Microsoft.AspNetCore.Identity;

namespace Hope.Domain.Model
{
    public class User:IdentityUser
    {

        public string City { get; set; }
        public string? Name { get; set; }

        public string? UserImage { get; set; }
        public virtual List<PostOfLostThings>? lostThings { get; set; } 

        public virtual List<PostOfLostPeople>? lostPeople { get; set; } 

        public virtual List<PostOfLostPeople> HiddingPeoples { get; set; }  
        public virtual List<PostOfLostThings> HiddingThings { get; set; }

        public virtual List<PostOfLostPeople> PinningPeoples { get; set; } 
        public virtual List<PostOfLostThings> PinningThings { get; set; } 
        public virtual List<Message> Messages { get; set; } 
        public virtual List<Notification> Notifications { get; set; }
        public virtual List<Comment> Comments { get; set; } 

    }
}
