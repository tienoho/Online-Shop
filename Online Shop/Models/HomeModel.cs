using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Online_Shop.Languages;

namespace Online_Shop.Models
{
    public class HomeModel
    {
        [Display(Name = "LearnMore", ResourceType = typeof(Resource))]
        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FirstNameRequired")]
        public string LearnMore
        {
            get;
            set;
        }
    }
}