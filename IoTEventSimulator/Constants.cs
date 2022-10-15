using System.Collections.Generic;

namespace IoTEventSimulator
{
    public static class Constants
    {
        public static string connString = "Server=mvrcmhfeumysql.mysql.database.azure.com;UserID=rcmmvadmin;Password=Password$123;Database=foodtracker"; //ConfigurationManager.AppSettings["sqlConnString"];

        public static string RawMaterial = "raw_material";

        public static string ProductPickup = "product_pickup";

        public static string Fleet_ID = "fleet_id";

        public static string FleetID = "fleetid";

        public const string Supplier = "supplier";
        
        public const string Manufacturer = "manufacturer";


        public static List<string> supplierFleetLocations = new List<string>
            {
                "chitradurga",
                "Aimangala",
                "Hiriyur ",
                "Javagondnahalli",
                "kallembella",
                "vasanthanarasapura",
                "tumakuru ",
                "dobbspet ",
                "nelamangala",
                "bangalore",
            };

        public static List<string> manufacturerFleetLocation = new List<string>
            {
            "bangalore", 
            "hosur", 
            "krishnagiri", 
            "dharmapuri", 
            "salem", 
            "namakkal", 
            "kulithalai",
            "trichy",
            "thanjavur",
            "mannarkudi"
            };
    }
}
