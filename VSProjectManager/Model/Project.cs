using System;

namespace VSProjectManager.Model
{
    public class Project
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsProject { get; set; }
        public DateTime AccessTime { get; set; }

        public double TimeAgo
        {
            get; set;
        }

        public Project(string name, string path, DateTime accessTime, bool isProj = true)
        {
            Name = name;
            Path = path;
            IsProject = isProj;
            AccessTime = accessTime;
            TimeAgo = (DateTime.Now - AccessTime).TotalDays;
        }
    }
}
