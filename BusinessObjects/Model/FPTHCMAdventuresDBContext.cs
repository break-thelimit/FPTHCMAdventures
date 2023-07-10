using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class FPTHCMAdventuresDBContext : DbContext
    {
        public FPTHCMAdventuresDBContext()
        {
        }

        public FPTHCMAdventuresDBContext(DbContextOptions<FPTHCMAdventuresDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventTask> EventTasks { get; set; }
        public virtual DbSet<ExchangeHistory> ExchangeHistories { get; set; }
        public virtual DbSet<Gift> Gifts { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemIventory> ItemIventories { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<Npc> Npcs { get; set; }
        public virtual DbSet<PlayHistory> PlayHistories { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SchoolEvent> SchoolEvents { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskItem> TaskItems { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CapstonProjectDbConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Answer1).HasColumnName("answer");

                entity.Property(e => e.IsRight).HasColumnName("is_right");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_Answer.question_id");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.RankId).HasColumnName("rank_id");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.RankId)
                    .HasConstraintName("FK_Event.rank_id");
            });

            modelBuilder.Entity<EventTask>(entity =>
            {
                entity.ToTable("Event_tasks");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventTasks)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Event_tasks.event_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.EventTasks)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_Event_tasks.task_id");
            });

            modelBuilder.Entity<ExchangeHistory>(entity =>
            {
                entity.ToTable("ExchangeHistory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ExchangeDate)
                    .HasColumnType("datetime")
                    .HasColumnName("exchange_date");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ExchangeHistories)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_ExchangeHistory.item_id");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.ExchangeHistories)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_ExchangeHistory.player_id");
            });

            modelBuilder.Entity<Gift>(entity =>
            {
                entity.ToTable("Gift");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Decription).HasColumnName("decription");

                entity.Property(e => e.GiftName).HasColumnName("gift_name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.RankId).HasColumnName("rank_id");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.Gifts)
                    .HasForeignKey(d => d.RankId)
                    .HasConstraintName("FK_Gift.rank_id");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_Inventory.player_id");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ImageUrl).HasColumnName("image_url");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<ItemIventory>(entity =>
            {
                entity.ToTable("ItemIventory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.InventoryId).HasColumnName("inventory_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.ItemIventories)
                    .HasForeignKey(d => d.InventoryId)
                    .HasConstraintName("FK_ItemIventory.inventory_id");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemIventories)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_ItemIventory.item_id");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(50)
                    .HasColumnName("location_name");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status ");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.Property(e => e.Z).HasColumnName("z");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.ToTable("Major");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Npc>(entity =>
            {
                entity.ToTable("NPC");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Introduce).HasColumnName("introduce");

                entity.Property(e => e.NpcName)
                    .HasMaxLength(50)
                    .HasColumnName("npc_name");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Npcs)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_NPC_Question");
            });

            modelBuilder.Entity<PlayHistory>(entity =>
            {
                entity.ToTable("PlayHistory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AcceptTime)
                    .HasColumnType("datetime")
                    .HasColumnName("accept_time");

                entity.Property(e => e.CompleteTime).HasColumnName("complete_time");

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.Property(e => e.TaskPoint).HasColumnName("task_point");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayHistories)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_PlayHistory.player_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.PlayHistories)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_PlayHistory.task_id");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(255)
                    .HasColumnName("nickname");

                entity.Property(e => e.TotalPoint).HasColumnName("total_point");

                entity.Property(e => e.TotalTime).HasColumnName("total_time");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Player.user_id");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.MajorId).HasColumnName("major_id");

                entity.Property(e => e.QuestionName).HasColumnName("question_name");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_Question_Major");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.ToTable("Rank");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.Property(e => e.RankNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("rank_number");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Ranks)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_Rank.player_id");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.TokenId);

                entity.ToTable("RefreshToken");

                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiry_date");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__RefreshTo__user___1DB06A4F");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name ");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.ToTable("School");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");

                entity.Property(e => e.SchoolName).HasColumnName("school_name");
            });

            modelBuilder.Entity<SchoolEvent>(entity =>
            {
                entity.ToTable("SchoolEvent");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.SchoolEvents)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_SchoolEvent.event_id");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolEvents)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_SchoolEvent.school_id");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ActivityName).HasColumnName("activity_name");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.IsRequireitem).HasColumnName("is_requireitem");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.MajorId).HasColumnName("major_id");

                entity.Property(e => e.NpcId).HasColumnName("npc_id");

                entity.Property(e => e.Point).HasColumnName("point");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.TimeOutAmount).HasColumnName("time_out_amount");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Task.location_id");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_Task.major_id");

                entity.HasOne(d => d.Npc)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.NpcId)
                    .HasConstraintName("FK_Task.npc_id");
            });

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("TaskItem");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TaskItems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_TaskItem.item_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskItems)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_TaskItem.task_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Fullname).HasColumnName("fullname");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User.role_id");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_User.school_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
