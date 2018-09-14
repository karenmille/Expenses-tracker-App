using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ExpensesTrackerAplication.Models.Extended
{
    [MetadataType(typeof(ExpensMetadata))]
    public partial class Expens
    {

    }

    public class ExpensMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide first")]
        public string ProjectID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide first")]
        public string Name { get; set; }



    }
}