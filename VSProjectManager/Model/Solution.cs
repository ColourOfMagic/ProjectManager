using System;
using System.Collections.Generic;

namespace VSProjectManager.Model
{
    public class Solution
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime AccessTime { get; set; }
        public List<Project> Projects { get; set; }  //Изменить потом названия, это проекты, а сам класс это решения

        public double TimeAgo
        {
            get; set;
        }

        public Solution(string name, string path, DateTime accessTime, List<Project> projects)
        {
            Name = name;
            Path = path;
            AccessTime = accessTime;
            TimeAgo = (DateTime.Now - AccessTime).TotalDays;
            Projects = projects;
        }
    }
}
