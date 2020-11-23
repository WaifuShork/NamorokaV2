namespace NamorokaV2
{
    public class UserModel
    {
        public int UserId { get; set; }
        
        public string Reason { get; set; }
        
        public int InfractionId { get; set; }

        public string FullUser => $"{UserId.ToString()}";
    }
}