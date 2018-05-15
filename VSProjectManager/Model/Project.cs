using System;
using System.Collections.Generic;
using Microsoft.Build.Construction;

namespace VSProjectManager.Model
{
    public class Project
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime AccessTime { get; set; }
        public List<ProjectInSolution> NestedProjects { get; set; }  //Изменить потом названия, это проекты, а сам класс это решения
        public List<string> NameNestedProjects
        {
            get
            {
                var Coll = new List<string>();
                foreach (var item in NestedProjects)
                {
                    if (item.ProjectType != SolutionProjectType.SolutionFolder)  //Чтобы в список не попали просто папки, потом исправить
                        Coll.Add(item.ProjectName);
                }
                return Coll;
            }
        }

        public double TimeAgo
        {
            get; set;
        }

        public Project(string name, string path, DateTime accessTime, List<ProjectInSolution> projects)
        {
            Name = name;
            Path = path;
            AccessTime = accessTime;
            TimeAgo = (DateTime.Now - AccessTime).TotalDays;
            NestedProjects = projects;
        }
    }
}
