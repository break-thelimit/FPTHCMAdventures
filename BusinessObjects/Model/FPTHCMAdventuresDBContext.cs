using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemIventory> ItemIventories { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<Npc> Npcs { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerHistory> PlayerHistories { get; set; }
        public virtual DbSet<PlayerPrize> PlayerPrizes { get; set; }
        public virtual DbSet<Prize> Prizes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SchoolEvent> SchoolEvents { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);Database=FPTHCMAdventuresDB;uid=sa;pwd=1234567890;");
            }
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

                entity.Property(e => e.AnswerName)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("answer_name");

                entity.Property(e => e.IsRight).HasColumnName("is_right");
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

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<EventTask>(entity =>
            {
                entity.ToTable("EventTask");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventTasks)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventTask.event_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.EventTasks)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventTask.task_id");
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

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ExchangeHistories)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExchangeHistory.item_id");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.ExchangeHistories)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExchangeHistory.player_id");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory.player_id");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.LimitExchange).HasColumnName("limit_exchange");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100)
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemIventory.inventory_id");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemIventories)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemIventory.item_id");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("location_name");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status");

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
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Npc>(entity =>
            {
                entity.ToTable("NPC");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Introduce)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("introduce");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Isplayer).HasColumnName("isplayer");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nickname");

                entity.Property(e => e.Passcode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("passcode");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.TotalPoint).HasColumnName("total_point");

                entity.Property(e => e.TotalTime).HasColumnName("total_time");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Player.event_id");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Player.student_id");
            });

            modelBuilder.Entity<PlayerHistory>(entity =>
            {
                entity.ToTable("PlayerHistory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CompletedTime).HasColumnName("completed_time");

                entity.Property(e => e.EventtaskId).HasColumnName("eventtask_id");

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.Property(e => e.TaskPoint).HasColumnName("task_point");

                entity.HasOne(d => d.Eventtask)
                    .WithMany(p => p.PlayerHistories)
                    .HasForeignKey(d => d.EventtaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayHistory.eventtask_id");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerHistories)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayHistory.player_id");
            });

            modelBuilder.Entity<PlayerPrize>(entity =>
            {
                entity.ToTable("PlayerPrize");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.Property(e => e.PrizeId).HasColumnName("prize_id");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerPrizes)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayerPrize.player_id");

                entity.HasOne(d => d.Prize)
                    .WithMany(p => p.PlayerPrizes)
                    .HasForeignKey(d => d.PrizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayerPrize.prize_id");
            });

            modelBuilder.Entity<Prize>(entity =>
            {
                entity.ToTable("Prize");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Decription)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("decription");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Prizes)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prize.event_id");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AnswerId).HasColumnName("answer_id");

                entity.Property(e => e.MajorId).HasColumnName("major_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question.answer_id");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question.major_id");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.TokenId)
                    .HasName("PK__RefreshT__CB3C9E171E429C02");

                entity.ToTable("RefreshToken");

                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.Expirydate)
                    .HasColumnType("datetime")
                    .HasColumnName("expirydate");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshToken.student_id");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.ToTable("School");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<SchoolEvent>(entity =>
            {
                entity.ToTable("SchoolEvent");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.InvitationLetter)
                    .IsRequired()
                    .HasColumnName("invitation_letter");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.SchoolEvents)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolEvent.event_id");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolEvents)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolEvent.school_id");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Classname)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("classname");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("fullname");

                entity.Property(e => e.GraduateYear)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("graduate_year");

                entity.Property(e => e.Phonenumber).HasColumnName("phonenumber");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student.school_id");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.MajorId).HasColumnName("major_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.NpcId).HasColumnName("npc_id");

                entity.Property(e => e.Point).HasColumnName("point");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task.item_id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task.location_id");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task.major_id");

                entity.HasOne(d => d.Npc)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.NpcId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task.npc_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
