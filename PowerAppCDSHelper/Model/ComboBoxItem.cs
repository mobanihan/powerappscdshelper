namespace PowerAppCDSHelper.Model
{
    public class ComboBoxItem
    {
        public ComboBoxItem()
        {

        }

        public ComboBoxItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
