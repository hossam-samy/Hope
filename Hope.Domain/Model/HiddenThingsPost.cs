﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Domain.Model
{
    public class HiddenThingsPost
    {
        public virtual User User { get; set; }
        public virtual PostOfLostPeople postOfLostPeople { get; set; }
    }
}
