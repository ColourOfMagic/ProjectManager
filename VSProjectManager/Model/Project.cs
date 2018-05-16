namespace VSProjectManager.Model
{
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
                return DirectoryPath + @"\bin\Debug";
            }
        }
        public string ReleasePath
        {
            get
            {
                return DirectoryPath + @"\bin\Release";
            }
        }

        public Project(string path,string name)
        {
            Path = path;
            Name = name;
        }
    }
}
