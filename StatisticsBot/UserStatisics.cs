using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsBot
{
    public class UserStatisics
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long MessageCount { get; set; }
        public DateTime LastMessageDate { get; set; }
        public DateTime FirstMessageDate { get; set; }
        public TimeSpan LongestAbsencePeriod { get; set; }
        public UserStatisics(long id, string name, DateTime lastMessageDate, DateTime firstMessageDate)
        {
            Id = id;
            Name = name;
            MessageCount = 1;
            LastMessageDate = lastMessageDate;
            FirstMessageDate = firstMessageDate;
        }
    }
}
