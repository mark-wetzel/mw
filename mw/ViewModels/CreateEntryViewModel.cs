using System.Web.Mvc;

namespace mw.ViewModels
{
    public class CreateEntryViewModel
    {
        public string Title { get; set; }
        [AllowHtml]
        public string Body { get; set; }
    }
}