namespace JSONdata
{
    public class ProjectData
    {
        public string[] Genre;
        public string ReleaseBuild;
        public string[] DevelopersID;

        public ProjectData(string[] genre, string releaseBuild, string[] developers)
        {
            Genre = genre;
            ReleaseBuild = releaseBuild;
            DevelopersID = developers;
        }
    }
}