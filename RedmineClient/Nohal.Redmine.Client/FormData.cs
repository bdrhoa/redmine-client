using System.Collections.Generic;

namespace Nohal.Redmine.Client
{
    internal class FormData
    {
        public List<Project> Projects { get; set; }
        public List<Activity> Activities { get; set; }
        public List<Issue> Issues { get; set; }
    }
}
