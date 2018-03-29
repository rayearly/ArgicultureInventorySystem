namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBookingDate : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO BookingDates (BookingDateTime, ReturnDateTime) VALUES ('02-02-2018' , '03-03-2018')");
            Sql("INSERT INTO BookingDates (BookingDateTime, ReturnDateTime) VALUES ('04-04-2018' , '05-05-2018')");
            Sql("INSERT INTO BookingDates (BookingDateTime, ReturnDateTime) VALUES ('06-06-2018' , '07-07-2018')");
            Sql("INSERT INTO BookingDates (BookingDateTime, ReturnDateTime) VALUES ('08-08-2018' , '09-09-2018')");
            Sql("INSERT INTO BookingDates (BookingDateTime, ReturnDateTime) VALUES ('10-10-2018' , '11-11-2018')");
        }
        
        public override void Down()
        {
        }
    }
}
