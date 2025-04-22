namespace TravelAgency.Models
{
    public class Hotel
    {
        public int HotelId { get; private set; }
        public string Name { get; private set; } = null!;
        public string PhoneNumber { get; private set; } = null!;
        public string Address { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public int stars { get; private set; }
        public decimal PricePerNight { get; private set; }
        public bool HasWifi { get;private set; }
        public bool HasParking { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;


        public static Hotel Create(string name, string phoneNumber, string address, string city, string description, int stars, decimal pricePerNight, bool hasWifi, bool hasParking)
        {
          
            return new Hotel
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Address = address,
                City = city,
                Description = description,
                stars = stars,
                PricePerNight = pricePerNight,
                HasWifi = hasWifi,
                HasParking = hasParking
            };
        }

      public void UpdatePrice(decimal pricePerNight)
        {
           if (pricePerNight <0)
                throw new ArgumentException("Price must be grater than zero");

            PricePerNight = pricePerNight;
        }

    }
}
