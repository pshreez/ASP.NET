using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace TaskManagementSystem.Models
{
    
    public class TaskManagementSystemDB: DbContext
    {
   
            public DbSet<User> Users { get; set; }
            public DbSet<Project> Projects { get; set; }
            public DbSet<Task> Tasks { get; set; }      
            public DbSet<Issue> Issues { get; set; }
            public DbSet<File> FileUploads { get; set; }
            public DbSet<Discuss> Discussions { get; set; }
            public DbSet<DiscussionThread> DiscussionThreads { get; set; }

            public DbSet<Organisations> Organisations { get; set; }
            public DbSet<Taskassign> TaskAssigned { get; set; }

    }
}