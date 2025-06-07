using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace StatisticsBot
{
    public class StatisticsCollector
    {
        public Dictionary<long, UserStatisics> UserStatistics = new Dictionary<long, UserStatisics>();

        public void UpdateUserStatistics(long userId, Message msg)
        {
            var now = DateTime.UtcNow;
            if (UserStatistics.ContainsKey(userId))
            {
                var user = UserStatistics[userId];
                var delta = now - user.LastMessageDate;
                if (delta > user.LongestAbsencePeriod)
                {
                    user.LongestAbsencePeriod = delta;
                }
                user.LastMessageDate = now;
                user.MessageCount += 1;
            }
            else
            {
                var newUser = new UserStatisics(userId, msg.From.Username ?? "UnknownUser", now, now);
                UserStatistics.Add(userId, newUser);
            }
        }

        public string GetStatistics()
        {
            var sb = new StringBuilder();
            foreach(var u in UserStatistics)
            {
                var us = u.Value;
                sb.Append($"--------------------------------");
                sb.AppendLine();
                sb.Append($"User {us.Name}");
                sb.AppendLine();
                sb.Append($"{nameof(us.MessageCount)}:{us.MessageCount}");
                sb.AppendLine();
                sb.Append($"{nameof(us.FirstMessageDate)}:{us.FirstMessageDate}");
                sb.AppendLine();
                sb.Append($"{nameof(us.LastMessageDate)}:{us.LastMessageDate}");
                sb.AppendLine();
                sb.Append($"{nameof(us.LongestAbsencePeriod)}:{us.LongestAbsencePeriod.TotalSeconds} seconds");
                sb.AppendLine();
                sb.Append($"--------------------------------");
            }
            return sb.ToString();
        }
    }
}
