namespace SDIKit.Common.UserInterfaceType
{
    public class SelectItem
    {
        public SelectItem(string id, string text)
        {
            Id = id;
            Text = text;
        }

        private SelectItem()
        {
        }

        public string Id { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
    }
}