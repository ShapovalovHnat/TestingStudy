using System.Collections.Generic;

namespace TestingTask.Core.Models
{
    public class Room
    {
        public string Name { get; set; }

        public List<Room> Rooms { get; set; }

        public bool AllowPets { get; set; }
    }
}