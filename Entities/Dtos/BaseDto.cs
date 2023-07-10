namespace gps_app.Entities.Dtos
{
    public abstract class BaseDto
    {
        public required string Id { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
