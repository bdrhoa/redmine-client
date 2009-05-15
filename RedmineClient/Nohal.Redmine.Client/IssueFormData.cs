using System;
using System.Collections.Generic;
using System.Text;

namespace Nohal.Redmine.Client
{
    internal class IssueFormData
    {
        public List<Tracker> Trackers { get; set; }
        public List<Status> Statuses { get; set; }
        public List<Priority> Priorities { get; set; }
        public List<Version> Versions { get; set; }
        public List<User> Watchers { get; set; }
        public List<User> Assignees { get; set; }
    }
}
