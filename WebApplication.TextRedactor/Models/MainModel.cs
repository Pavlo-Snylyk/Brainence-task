using System.Collections.Generic;
using System.Web;

namespace WebApplication.TextRedactor.Models
{
    public class MainModel
    {
        public string InputText { get; set; }
        public List<Sentences> SavedSentences { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}