namespace PostcodeServices.Models
{
    /// <summary>
    /// Single instance of a postcode, stores Id, postcode and status.
    /// </summary>
    public class Postcode
    {
        public Postcode(int id, string value)
        {
            Id = id;
            Value = value;
        }

        public Postcode(string id, string value)
        {
            Id = int.Parse(id);
            Value = value;
        }

        public int Id { get; set; }
        public string Value { get; set; }

        public bool IsValid { get; set; }
    }
}


