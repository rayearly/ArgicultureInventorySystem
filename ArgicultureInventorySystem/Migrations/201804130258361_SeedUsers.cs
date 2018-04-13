namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5cdb8149-33bd-448a-bdfa-2e3d81c66bed', N'guest@guest.com', 0, N'AA4DfTSckyDIaEaDvELwgiySfeWCctdySKIv8rYuratRxlWAlNq9cM+tINyzi4svTA==', N'e2e371ea-e826-4ce7-9d62-9b6c1da06342', NULL, 0, 0, NULL, 1, 0, N'guest@guest.com')
            
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6c0c0868-f998-4bc2-85ae-62ee8d1a664c', N'admin@admin.com', 0, N'AEcZr+e5UbPhu+RxJIWZ8RTylKMZK5bPyzAiT9QSad9itrqBRGpvBX+BKwDbMu4Atg==', N'fc97c5bc-0aed-4537-8686-99eaed6f4863', NULL, 0, 0, NULL, 1, 0, N'admin@admin.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e36dce00-5b0d-442d-bf52-1532e5b4d42d', N'raysocialemail@gmail.com', 0, N'AOTM0HK5+1mjwuLRiPmB5UMampUL+10ivQPBjqO3aBDQk2eN1E3TeAR8FlQqmvB21A==', N'46b426f5-fabd-4a9b-af18-e00731e56633', NULL, 0, 0, NULL, 1, 0, N'raysocialemail@gmail.com')

            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c6a45bed-6ac4-4b68-86bc-67d321c6fedc', N'CanManageBookings')

            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6c0c0868-f998-4bc2-85ae-62ee8d1a664c', N'c6a45bed-6ac4-4b68-86bc-67d321c6fedc')

            ");
        }
        
        public override void Down()
        {
        }
    }
}
