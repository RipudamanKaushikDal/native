using MangaScrapeLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reardo.Models
{
    public class ChapterList
    {
        public string ChapterName { get; set; }

        public string UpdatedDate { get; set; }

        public IChapter ChapterModel { get; set; }
    }
}
