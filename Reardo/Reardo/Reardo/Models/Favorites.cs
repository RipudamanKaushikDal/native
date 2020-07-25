using MangaScrapeLib;
using SQLite;
using System;
using System.Collections.Generic;

namespace Reardo.Models
{
    public class Favorites
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }


        public string SeriesTitle { get; set; }
        public  Uri CoverImage { get; set; }

        public Uri SeriesUri { get; set; }

        public string Progress { get; set; }
    }
}
