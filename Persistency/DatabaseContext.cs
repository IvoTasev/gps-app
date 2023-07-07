using CsvHelper;
using gps_app.Models;
using gps_app.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;

namespace gps_app.Persistency
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=gpsdb;Trusted_Connection=true;TrustServerCertificate=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            using (var reader = new StreamReader("gps_data.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {

                var devices = new List<Device>();
                var devicesData = new List<DeviceData>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var deviceId = csv.GetField<string>("Device ID");

                    var device = new Device
                    {
                        Id = deviceId,
                        DeviceType = ParseEnum(csv.GetField<string>("Device Type"))
                    };

                    if (devices.Any(d => d.Id == deviceId) == false)
                    {
                        devices.Add(device);
                    }

                    var deviceData = new DeviceData
                    {
                        Id = (devicesData.Count + 1).ToString(),
                        Date = FormatDate(csv.GetField<string>("Timestamp")),
                        Location = csv.GetField<string>("Location"),
                        DeviceId = device.Id,

                    };

                    devicesData.Add(deviceData);
                }

                modelBuilder.Entity<Device>().HasData(devices);
                modelBuilder.Entity<DeviceData>().HasData(devicesData);
            }

            modelBuilder.Entity<Device>()
               .HasMany(e => e.DeviceData)
               .WithOne(e => e.Device)
               .HasForeignKey(e => e.DeviceId)
               .IsRequired();

            modelBuilder
                .Entity<Device>()
                .Property(d => d.DeviceType)
                .HasConversion(new EnumToStringConverter<DeviceType>());


            modelBuilder.Entity<DeviceData>()
               .HasOne(e => e.Device)
               .WithMany(e => e.DeviceData)
               .HasForeignKey(e => e.DeviceId)
               .IsRequired();
        }

        private DeviceType ParseEnum(string value)
        {
            return (DeviceType)Enum.Parse(typeof(DeviceType), value, true);
        }
        private DateTime FormatDate(string dateString)
        {
            DateTime date = DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            return date;
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceData> DeviceData { get; set; }
    }
}
