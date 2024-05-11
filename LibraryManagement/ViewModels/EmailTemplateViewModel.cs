namespace LibraryManagement.ViewModels
{
    public class EmailTemplateViewModel
    {
        public Guid Id { get; set; }    
        public string TemplateName { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
