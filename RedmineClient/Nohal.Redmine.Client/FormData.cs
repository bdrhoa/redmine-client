using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nohal.Redmine.Client
{
    class FormData
    {
        public List<Project> Projects { get; set; }
        public List<Activity> Activities { get; set; }
        public List<Issue> Issues { get; set; }
    }
}
