using System;
using System.Collections.Generic;
using System.Data.Common;
using Kiosk.Business.Helpers;
using Kiosk.Business.Model.Amenities;
using Kiosk.Business.Model.Search;
using Kiosk.Domain.Models;
using Kiosk.Domain.Models.SPModel;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Data;

public partial class KioskContext : DbContext
{
    public KioskContext()
    {
    }

    public KioskContext(DbContextOptions<KioskContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; }

    public virtual DbSet<Agprospect> Agprospects { get; set; }

    public virtual DbSet<BirthDayLeadEntry> BirthDayLeadEntries { get; set; }

    public virtual DbSet<CheckIn> CheckIns { get; set; }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<DataTrakCheckin> DataTrakCheckins { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<FreePassLeadEntry> FreePassLeadEntries { get; set; }

    public virtual DbSet<Hash> Hashes { get; set; }

    public virtual DbSet<InitialSignatureImage> InitialSignatureImages { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobParameter> JobParameters { get; set; }

    public virtual DbSet<JobQueue> JobQueues { get; set; }

    public virtual DbSet<KioskPlansBackup06032023> KioskPlansBackup06032023s { get; set; }

    public virtual DbSet<Lead> Leads { get; set; }

    public virtual DbSet<Lead1> Leads1 { get; set; }

    public virtual DbSet<LeadEntry> LeadEntries { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberInfo> MemberInfos { get; set; }

    public virtual DbSet<MemberPayment> MemberPayments { get; set; }

    public virtual DbSet<MemberPlan> MemberPlans { get; set; }

    public virtual DbSet<MemberSignupSource> MemberSignupSources { get; set; }

    public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

    public virtual DbSet<Minor> Minors { get; set; }

    public virtual DbSet<NewMember> NewMembers { get; set; }

    public virtual DbSet<NewMemberForReport> NewMemberForReports { get; set; }

    public virtual DbSet<NewPtmember> NewPtmembers { get; set; }

    public virtual DbSet<NewPtmemberForReport> NewPtmemberForReports { get; set; }

    public virtual DbSet<NonActiveMember> NonActiveMembers { get; set; }

    public virtual DbSet<NonBogomember> NonBogomembers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PromocodeMember> PromocodeMembers { get; set; }

    public virtual DbSet<ProspectBarcode> ProspectBarcodes { get; set; }

    public virtual DbSet<ProspectSignature> ProspectSignatures { get; set; }

    public virtual DbSet<Ptplan> Ptplans { get; set; }

    public virtual DbSet<RecurringMember> RecurringMembers { get; set; }

    public virtual DbSet<RecurringMember1> RecurringMembers1 { get; set; }

    public virtual DbSet<RecurringMember2> RecurringMembers2 { get; set; }

    public virtual DbSet<RecurringMemberSignature> RecurringMemberSignatures { get; set; }

    public virtual DbSet<RecurringMemberSignature1> RecurringMemberSignatures1 { get; set; }

    public virtual DbSet<RenewMember> RenewMembers { get; set; }

    public virtual DbSet<Schema> Schemas { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<SignatureImage> SignatureImages { get; set; }

    public virtual DbSet<SilverSneaker> SilverSneakers { get; set; }

    public virtual DbSet<Source> Sources { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<SurveyOption> SurveyOptions { get; set; }

    public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }

    public virtual DbSet<SurveyQuestionsType> SurveyQuestionsTypes { get; set; }

    public virtual DbSet<SurveySubmitAnswer> SurveySubmitAnswers { get; set; }

    public virtual DbSet<TempMember> TempMembers { get; set; }

    public virtual DbSet<TempMember1> TempMembers1 { get; set; }

    public virtual DbSet<TempRecurringMember> TempRecurringMembers { get; set; }

    public virtual DbSet<TempRecurringMember1> TempRecurringMembers1 { get; set; }

    public virtual DbSet<Totspot> Totspots { get; set; }

    public virtual DbSet<Url> Urls { get; set; }

    public virtual DbSet<UtbAbcEmployeeDepartment> UtbAbcEmployeeDepartments { get; set; }

    public virtual DbSet<UtbAbcSalesPerson> UtbAbcSalesPeople { get; set; }

    public virtual DbSet<UtbAbcemployee> UtbAbcemployees { get; set; }

    public virtual DbSet<UtbCourt> UtbCourts { get; set; }

    public virtual DbSet<UtbInventory> UtbInventories { get; set; }

    public virtual DbSet<UtbItem> UtbItems { get; set; }

    public virtual DbSet<UtbMemberChildCarePlanDetail> UtbMemberChildCarePlanDetails { get; set; }

    public virtual DbSet<UtbReferralsAndFriend> UtbReferralsAndFriends { get; set; }

    public virtual DbSet<YouFitCheckoutPtplan> YouFitCheckoutPtplans { get; set; }

    public virtual DbSet<YoufitCheckoutTempMember> YoufitCheckoutTempMembers { get; set; }
    public virtual DbSet<usp_GetLocationsByEmail_Result> GetClubs { get; set; }
    public virtual DbSet<usp_GetClubStationsByClub_Result> ClubStations { get; set; }
    public virtual DbSet<usp_GetClubStateBYClubNumber_Result> GetStateByClub { get; set; }

    public virtual DbSet<ClubUserModel> GetClubUsers { get; set; }
    public virtual DbSet<ClubUserModel> GetClubsByUser { get; set; }
    public virtual DbSet<TotspotpickelballFlag> GetAmenitiesFlag { get; set; }
    public virtual DbSet<usp_GetMemberDetails_Result> MemberList { get; set; }
    public virtual DbSet<usp_GetEmployeeDetail_Result> SalesPersonList { get; set; }
    public virtual DbSet<GetABCPaymentPlansResultModels> getSilverSneakersPlans { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(AppSettings.KioskConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AggregatedCounter>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("PK_HangFire_CounterAggregated");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_AggregatedCounter_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");
        });

        modelBuilder.Entity<CheckIn>(entity =>
        {
            entity.HasKey(e => e.CheckInId).HasName("PK_dbo.CheckIns");
        });

        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.ClubId).HasName("PK_dbo.Club");
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity.HasIndex(e => e.Key, "CX_HangFire_Counter").IsClustered();
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.Property(e => e.MemberType).IsFixedLength();
        });

        modelBuilder.Entity<Hash>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Field }).HasName("PK_HangFire_Hash");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Hash_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");
        });

        modelBuilder.Entity<InitialSignatureImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.InitialSignatureImages");

            entity.HasOne(d => d.Parent).WithMany(p => p.InitialSignatureImages).HasConstraintName("FK_dbo.InitialSignatureImages_dbo.ProspectSignatures_ParentId");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HangFire_Job");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Job_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.HasIndex(e => e.StateName, "IX_HangFire_Job_StateName").HasFilter("([StateName] IS NOT NULL)");
        });

        modelBuilder.Entity<JobParameter>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.Name }).HasName("PK_HangFire_JobParameter");

            entity.HasOne(d => d.Job).WithMany(p => p.JobParameters).HasConstraintName("FK_HangFire_JobParameter_Job");
        });

        modelBuilder.Entity<JobQueue>(entity =>
        {
            entity.HasKey(e => new { e.Queue, e.Id }).HasName("PK_HangFire_JobQueue");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<KioskPlansBackup06032023>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Lead>(entity =>
        {
            entity.HasKey(e => e.LeadId).HasName("PK_dbo.Lead");

            entity.ToTable("Lead", tb =>
                {
                    tb.HasTrigger("tgr_Lead_Insert");
                    tb.HasTrigger("tgr_Lead_update");
                });

            entity.Property(e => e.IsRfc).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsRfcviewed).HasDefaultValueSql("((0))");
            entity.Property(e => e.TotalProspectValidateCount).HasDefaultValueSql("((0))");
            entity.Property(e => e.TotalValidateCount).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<Lead1>(entity =>
        {
            entity.HasKey(e => e.LeadId).HasName("PK_Pickleball.Lead");
        });

        modelBuilder.Entity<LeadEntry>(entity =>
        {
            entity.Property(e => e.AbccheckIn).HasDefaultValueSql("((0))");
            entity.Property(e => e.TotalProspectValidateCount).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Id }).HasName("PK_HangFire_List");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_List_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Members");
        });

        modelBuilder.Entity<MemberPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MemberPayment_1");
        });

        modelBuilder.Entity<MigrationHistory>(entity =>
        {
            entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");
        });

        modelBuilder.Entity<Minor>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<NewMember>(entity =>
        {
            entity.Property(e => e.AbccheckIn).HasDefaultValueSql("((0))");
            entity.Property(e => e.DwhInserted).HasDefaultValueSql("((0))");
            entity.Property(e => e.HasAppointment).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsPrepaidCard).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsUserChangedPrepaidCard).HasDefaultValueSql("((0))");
            entity.Property(e => e.TotalValidateCount).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<NewMemberForReport>(entity =>
        {
            entity.Property(e => e.IsContractUpdated).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<NewPtmember>(entity =>
        {
            entity.ToTable("NewPTMember", tb => tb.HasTrigger("Tr_NewPTMember_Insert"));

            entity.Property(e => e.IsPrepaidCard).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsUserChangedPrepaidCard).HasDefaultValueSql("((0))");
            entity.Property(e => e.TotalValidateCount).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<NonActiveMember>(entity =>
        {
            entity.HasKey(e => e.NonActiveMemberId).HasName("PK_dbo.NonActiveMember");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6EDB01BF04D");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
        });

        modelBuilder.Entity<PromocodeMember>(entity =>
        {
            entity.ToView("PromocodeMembers");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ProspectBarcode>(entity =>
        {
            entity.HasKey(e => e.ProspectBarcodeId).HasName("PK_dbo.ProspectBarcodes");
        });

        modelBuilder.Entity<ProspectSignature>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ProspectSignatures");
        });

        modelBuilder.Entity<Ptplan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_YouFitCheckout.PTPlan");
        });

        modelBuilder.Entity<RecurringMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_RecurringMembers_1");
        });

        modelBuilder.Entity<RecurringMemberSignature>(entity =>
        {
            entity.Property(e => e.RecurringMemberSignatureId).ValueGeneratedNever();
        });

        modelBuilder.Entity<RecurringMemberSignature1>(entity =>
        {
            entity.Property(e => e.RecurringMemberSignatureId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Schema>(entity =>
        {
            entity.HasKey(e => e.Version).HasName("PK_HangFire_Schema");

            entity.Property(e => e.Version).ValueGeneratedNever();
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HangFire_Server");
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Value }).HasName("PK_HangFire_Set");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Set_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");
        });

        modelBuilder.Entity<SignatureImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.SignatureImages");

            entity.HasOne(d => d.Parent).WithMany(p => p.SignatureImages).HasConstraintName("FK_dbo.SignatureImages_dbo.ProspectSignatures_ParentId");
        });

        modelBuilder.Entity<Source>(entity =>
        {
            entity.HasKey(e => e.SourceId).HasName("PK_dbo.Source");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.Id }).HasName("PK_HangFire_State");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Job).WithMany(p => p.States).HasConstraintName("FK_HangFire_State_Job");
        });

        modelBuilder.Entity<SurveySubmitAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.SurveySubmitAnswers");
        });

        modelBuilder.Entity<TempMember>(entity =>
        {
            entity.HasKey(e => e.TempMemberId).HasName("PK_PTMembers");
        });

        modelBuilder.Entity<TempMember1>(entity =>
        {
            entity.HasKey(e => e.TempMemberId).HasName("PK_PTMembers");
        });

        modelBuilder.Entity<Totspot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_YouFitCheckout.Totspot");
        });

        modelBuilder.Entity<Url>(entity =>
        {
            entity.HasKey(e => e.Urlid).HasName("PK_ShortenURL");

            entity.Property(e => e.IsActiveMember).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<UtbInventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Inventory");
        });

        modelBuilder.Entity<UtbItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Item");
        });

        modelBuilder.Entity<UtbMemberChildCarePlanDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.utb_Member_ChildCarePlanDetails");
        });

        modelBuilder.Entity<UtbReferralsAndFriend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Referrals_And_Friends");
        });

        modelBuilder.Entity<YouFitCheckoutPtplan>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<usp_GetLocationsByEmail_Result>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<usp_GetClubStationsByClub_Result>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<usp_GetMemberDetails_Result>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<usp_GetEmployeeDetail_Result>(entity =>
        {
            entity.HasNoKey();
        });


        modelBuilder.Entity<ClubUserModel>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<GetABCPaymentPlansResultModels>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<usp_GetClubStateBYClubNumber_Result>(entity =>
        {
            entity.HasNoKey();
        });
        

        modelBuilder.Entity<TotspotpickelballFlag>(entity =>
        {
            entity.HasNoKey();
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
