using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication;

namespace WebApplication.Models
{
    public class Event : Entity<long>
    {
        public long Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public Room Room
        {
            get;
            set;
        }

        public long RoomId
        {
            get;
            set;
        }

        public ApplicationUser ApplicationUser
        {
            get;
            set;
        }

        public string ApplicationUserId
        {
            get;
            set;
        }

        public Event()
        {
        }

        public static Event Create(CreateEvent value, string userId)
        {
            return new Event()
            {Id = value.Id, Name = value.Name, RoomId = value.Room.Id, ApplicationUserId = userId, };
        }

        public void Update(UpdateEvent value, string userId)
        {
            Id = value.Id;
            Name = value.Name;
            RoomId = value.Room.Id;
            ApplicationUserId = userId;
        }
    }

    public class CreateEvent
    {
        public long Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public CompactRoom Room
        {
            get;
            set;
        }

        public CompactApplicationUser ApplicationUser
        {
            get;
            set;
        }
    }

    public class UpdateEvent
    {
        public long Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public CompactRoom Room
        {
            get;
            set;
        }

        public CompactApplicationUser ApplicationUser
        {
            get;
            set;
        }
    }

    public class GetEvent
    {
        public long Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public CompactRoom Room
        {
            get;
            set;
        }

        public CompactApplicationUser ApplicationUser
        {
            get;
            set;
        }
    }

    public class CompactEvent
    {
        public long Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
