using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace JobPortal.Service.Dtos
{
    public class MultipleSheetsDto
    {
        public DataTable dt { get; set; }
        public string WorkSheetName { get; set; }
        public string ReportHeading { get; set; }
    }
}
