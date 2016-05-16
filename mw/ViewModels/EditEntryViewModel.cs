using System.Web.Mvc;

namespace mw.ViewModels
{
    public class EditEntryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Body { get; set; }
    }
}