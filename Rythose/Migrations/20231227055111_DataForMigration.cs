using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raythose.Migrations
{
    /// <inheritdoc />
    public partial class DataForMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"INSERT INTO [dbo].[tbl_aircraft]
               ([ModelName]
               ,[AircraftType]
               ,[Passengers]
               ,[Baggage]
               ,[CabinWidth]
               ,[CabinHeight]
               ,[CabinLength]
               ,[Range]
               ,[Speed]
               ,[Fuel]
               ,[Price]
               ,[Quantity]
               ,[Description]
               ,[FrontImage1]
               ,[FrontImage2]
               ,[FrontImage3]
               ,[InnerImage1]
               ,[InnerImage2]
               ,[InnerImage3]
               ,[SeatingImage]
               ,[Status])
             VALUES
               ('CIRRUS VISION SF50', 'Light Private Jets', 5, 63, 4.8, 4.7, 9.8, 2, 20, '2', 35000000, 2, 'The Citation Mustang is a very light jet with the spirit of a heavier jet. The speed and range of the performance-inspired Mustang will tackle the sky while delivering the efficiency of a lighter aircraft. Its aerodynamic design and advanced technology excel in short-field performance situations. The cozy interior boasts amazing finishes and the highest-quality materials for the upper class luxury you come to expect in a business jet. The Mustang’s cabin has six large oval windows that provide incredible views and abundant natural light. Looking to rent a private jet like the Citation Mustang.', '1.jpg', 't', 't', 't', 't', 't', 't', 'active')"
        );

            migrationBuilder.Sql(
                @"INSERT INTO [dbo].[tbl_customer]
               ([Address]
               ,[City]
               ,[Contact]
               ,[Country]
               ,[DateOfBirth]
               ,[Email]
               ,[FullName]
               ,[IdNumber]
               ,[LicenseCard]
               ,[Password]
               ,[Status]
               ,[Username]
               ,[VerificationCode]
               ,[ZipCode])
         VALUES
               ('321 Elm St', 'Metropolis', '+1122334455', 'Countryland', '1987-03-25', 'alice.johnson@example.com', 'Alice Johnson', 'PASS123456', 'PL123ABC', 'pass123word', 'active', 'alicejohnson', 'ABCDE12345', 54321),
               ('654 Birch Ave', 'Smalltown', '+9876543210', 'Countryland', '1992-09-15', 'david.williams@example.com', 'David Williams', 'NID789012', 'PL456XYZ', 'davidpass', 'active', 'davidwilliams', 'FGHIJ67890', 98765),
               ('876 Oak St', 'Villageville', '+3344556677', 'Countryland', '1980-05-12', 'eva.martinez@example.com', 'Eva Martinez', 'PASS345678', 'PL789LMN', 'eva123', 'active', 'evamartinez', 'KLMNO12345', 11223),
               ('987 Cedar Ave', 'Hamletville', '+9988776655', 'Countryland', '1975-11-08', 'george.smith@example.com', 'George Smith', 'NID234567', 'PL012PQR', 'georgepass', 'active', 'georgesmith', 'PQRST67890', 33445),
               ('543 Pine St', 'Countryside', '+6677889900', 'Countryland', '1989-07-03', 'linda.brown@example.com', 'Linda Brown', 'PASS890123', 'PL345STU', 'linda456', 'active', 'lindabrown', 'UVWXY12345', 55443)"
            );

            migrationBuilder.Sql(
                @"INSERT INTO [dbo].[tbl_essential_items]
               ([EssentialType]
               ,[EssentialName]
               ,[EssentialQuantity]
               ,[EssentialDate]
               ,[EssentialStock])
         VALUES
               ('Airframe', 'Cirrus Vision Frame', 5, '', 5),
               ('Powerplant', 'Engine 241', 8, '2023-10-05', 8),
               ('Powerplant', 'Engine C21', 10, '2023-10-05', 10),
               ('Powerplant', 'Engine MT01', 5, '2023-10-05', 4),
               ('Avionics', 'Cockpit instrument', 10, '2023-10-05', 10),
               ('Avionics', 'Flight control system', 5, '2023-10-05', 5),
               ('Avionics', 'Hydraulic systems', 10, '2023-10-05', 8),
               ('Avionics', 'Electrical systems', 5, '2023-10-05', 5),
               ('Avionics', 'Fuel systems', 10, '2023-10-05', 10),
               ('Miscellaneous', 'Cooling system', 5, '2023-10-05', 5),
               ('Miscellaneous', 'De-icing system', 10, '2023-10-05', 10),
               ('Miscellaneous', 'Flaps and slats', 15, '2023-10-05', 15)"
            );

            migrationBuilder.Sql(
                @"INSERT INTO [dbo].[tbl_items]
               ([ItemName]
               ,[MainId]
               ,[SubId]
               ,[Quantity]
               ,[Price]
               ,[Vendor]
               ,[Date]
               ,[Stock])
         VALUES
               ('Bombardier Seats', 1, 1, 50, 2500, 'ABC, Canada', '2', '50'),
               ('Safran Seats', 1, 1, 20, 2000, 'ABC, UK', '2', '20'),
               ('Super Pro Seats', 1, 1, 20, 2300, 'ABC, Canada', '2', '15'),
               ('Divan Bed', 2, 4, 10, 1000, 'John Doe - 123 Main St, Cityville', '2', '10'),
               ('Wine Shelf', 2, 4, 20, 520, 'John Doe - 123 Main St, Cityville', '2', '15'),
               ('Karaoke Room', 2, 4, 5, 3250, 'test', '2', '5'),
               ('Dulux Fabric', 2, 4, 10, 220, 'test', '2', '10'),
               ('Genlift Fabric', 2, 4, 10, 120, 'test', '2', '8'),
               ('Soft Rugs', 2, 4, 20, 50, 'test', '2', '20'),
               ('Wifi Adapter', 3, 9, 10, 60, 'test', '2', '10'),
               ('Portable Setup', 3, 9, 5, 50, 'test', '2', '4'),
               ('Connection Wires', 3, 9, 20, 35, 'test', '2', '20'),
               ('Adapter', 3, 9, 10, 45, 'test', '2', '10'),
               ('Samsung TV', 4, 11, 5, 1570, 'test', '2', '5'),
               ('Connection Adapter', 4, 14, 15, 250, 'test', '2', '15'),
               ('Portable Screens', 4, 11, 10, 1400, 'test', '2', '10'),
               ('TV Stand Rack', 4, 11, 15, 75, 'test', '2', '14'),
               ('Speakers', 4, 14, 20, 55, 'test', '2', '20')"
            );

            migrationBuilder.Sql(
                @"INSERT INTO [dbo].[tbl_main_category]
               ([MainCategoryName]
               ,[Status])
         VALUES
               ('Seating Options', 'active'),
               ('Interior Design', 'active'),
               ('Connectivity Options', 'active'),
               ('Entertainment Options', 'active')"
            );

            migrationBuilder.Sql(
                @"INSERT INTO [dbo].[tbl_order]
               ([AircraftId]
               ,[CustomerId]
               ,[Seating]
               ,[Interior]
               ,[Connectivity]
               ,[Entertainment]
               ,[OrderStatus]
               ,[PaymentStatus]
               ,[AircraftPrice]
               ,[SeatingPrice]
               ,[InteriorPrice]
               ,[ConnectivityPrice]
               ,[EntertainmentPrice]
               ,[VAT]
               ,[FinalAmount]
               ,[Status])
         VALUES
               (2, -- AircraftId
                2, -- CustomerId
                1, -- Seating
                4, -- Interior
                9, -- Connectivity
                13, -- Entertainment
                'pending', -- OrderStatus
                'paid', -- PaymentStatus
                120.0, -- AircraftPrice
                220.0, -- SeatingPrice
                320.0, -- InteriorPrice
                420.0, -- ConnectivityPrice
                530.0, -- EntertainmentPrice
                0.55, -- VAT
                25000.5, -- FinalAmount
                'active' -- Status
               )"
            );

            migrationBuilder.Sql(
                @"INSERT INTO [dbo].[tbl_sub_category]
               ([MainId]
               ,[SubCategoryName]
               ,[Price]
               ,[Status])
         VALUES
               (1, 'Club Seating', 0, 'active'),
               (1, 'Individual Seats', 0, 'active'),
               (1, 'Divans', 0, 'active'),
               (2, 'Luxury Materials', 0, 'active'),
               (2, 'Semi Luxury Materials', 0, 'active'),
               (2, 'Custom Furnishings', 0, 'active'),
               (3, 'In-Flight Wi-Fi', 0, 'active'),
               (3, 'Satellite Communication Systems', 0, 'active'),
               (3, 'Cabin Management Systems (CMS)', 0, 'active'),
               (3, 'Ethernet Ports and Power Outlets', 0, 'active'),
               (4, 'Multiple Display Screens', 0, 'active'),
               (4, 'In-Flight Movies and TV Shows', 0, 'active'),
               (4, 'Gaming Consoles', 0, 'active'),
               (4, 'Live TV', 0, 'active'),
               (4, 'Customized Entertainment Packages', 0, 'active')"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
