﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBugs2020.Models
{
   
        public class ProjectUser
        {
            public string UserId { get; set; }
            public BTUser User { get; set; }
            public int ProjectId { get; set; }
            public Project Project { get; set; }
        }
    }

