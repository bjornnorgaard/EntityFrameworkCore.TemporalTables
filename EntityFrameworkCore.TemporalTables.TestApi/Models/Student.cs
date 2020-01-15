using System;

namespace EntityFrameworkCore.TemporalTables.TestApi.Models
{
    public class Student : TemporalEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string School { get; set; }
    }

    public class TemporalEntity
    {
        public DateTime SysStartTime { get; set; }
        public DateTime SysEndTime { get; set; }
    }
}
