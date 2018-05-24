namespace VSProjectManager.Model
{
    public enum ProjectTypes
    {
        CSharp=0,
        CplusPlus=1
    }

    public class Project
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string DirectoryPath
        {
            get
            {
                for (int i = Path.Length - 7; i > 0; i--)
                {
                    if (Path[i] == '\\')
                    {
                        return Path.Substring(0, i);
                        
                    }
                }
                return null;
            }
        }
        public string DebugPath
        {
            get
            {
                switch (Type)
                {
                    case ProjectTypes.CSharp: return DirectoryPath + @"\bin\Debug";
                    case ProjectTypes.CplusPlus: return DirectoryPath + @"\Debug";
                    default: return DirectoryPath;
                }
               
            }
        }
        public string ReleasePath
        {
            get
            {
                switch (Type)
                {
                    case ProjectTypes.CSharp: return DirectoryPath + @"\bin\Release";
                    case ProjectTypes.CplusPlus: return DirectoryPath + @"\Release";
                    default: return DirectoryPath;
                }
            }
        }
        public ProjectTypes Type { get; set; }

        public Project(string path,string name)
        {
            Path = path;
            Name = name;

            switch (path.Substring(path.LastIndexOf(".")))
            {
                case ".csproj": Type = ProjectTypes.CSharp; break;
                case ".vcxproj": Type = ProjectTypes.CplusPlus; break;
                default: 
                    break;
            }

        }
    }
}
