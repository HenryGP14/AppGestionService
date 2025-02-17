﻿using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Contexts;
public partial class GestionServicesContext : DbContext
{
    public GestionServicesContext()
    {
    }

    public GestionServicesContext(DbContextOptions<GestionServicesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attention> Attentions { get; set; }

    public virtual DbSet<Attentionstatus> Attentionstatuses { get; set; }

    public virtual DbSet<Attentiontype> Attentiontypes { get; set; }

    public virtual DbSet<Cash> Cashes { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<Error> Errors { get; set; }

    public virtual DbSet<Methodpayment> Methodpayments { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Statuscontract> Statuscontracts { get; set; }

    public virtual DbSet<Turn> Turns { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Usercash> Usercashes { get; set; }

    public virtual DbSet<Userstatus> Userstatuses { get; set; }

    public async Task<int> InsertErrorRecordAsync(Error error)
    {
        var resultParam = new SqlParameter("@result", System.Data.SqlDbType.Int)
        {
            Direction = System.Data.ParameterDirection.Output
        };

        await Database.ExecuteSqlRawAsync(
            "EXEC InsertErrorRecord @nameprocess, @description, @usercreation, @ipcreation, @result OUTPUT",
            new SqlParameter("@nameprocess", error.Nameprocess),
            new SqlParameter("@description", error.Description),
            new SqlParameter("@usercreation", error.Usercreation),
            new SqlParameter("@ipcreation", error.Ipcreation),
            resultParam);

        return (int)resultParam.Value;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attention>(entity =>
        {
            entity.HasKey(e => e.Attentionid).HasName("PK__attentio__99FD35525136529F");

            entity.ToTable("attention");

            entity.Property(e => e.Attentionid).HasColumnName("attentionid");
            entity.Property(e => e.AttentionstatusStatusid).HasColumnName("attentionstatus_statusid");
            entity.Property(e => e.AttentiontypeAttentiontypeid).HasColumnName("attentiontype_attentiontypeid");
            entity.Property(e => e.ClientClientid).HasColumnName("client_clientid");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
            entity.Property(e => e.TurnTurnid).HasColumnName("turn_turnid");

            entity.HasOne(d => d.ClientClient).WithMany(p => p.Attentions)
                .HasForeignKey(d => d.ClientClientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__attention__clien__5BE2A6F2");

            entity.HasOne(d => d.TurnTurn).WithMany(p => p.Attentions)
                .HasForeignKey(d => d.TurnTurnid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__attention__turn___5AEE82B9");
        });

        modelBuilder.Entity<Attentionstatus>(entity =>
        {
            entity.HasKey(e => e.Statusid).HasName("PK__attentio__36247E309718A150");

            entity.ToTable("attentionstatus");

            entity.Property(e => e.Statusid).HasColumnName("statusid");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Attentiontype>(entity =>
        {
            entity.HasKey(e => e.Attentiontypeid).HasName("PK__attentio__9D38AAA39F15F653");

            entity.ToTable("attentiontype");

            entity.Property(e => e.Attentiontypeid).HasColumnName("attentiontypeid");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Cash>(entity =>
        {
            entity.HasKey(e => e.Cashid).HasName("PK__cash__96014CBDB02A0C86");

            entity.ToTable("cash");

            entity.Property(e => e.Cashid).HasColumnName("cashid");
            entity.Property(e => e.Active)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("active");
            entity.Property(e => e.Cashdescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cashdescription");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Clientid).HasName("PK__client__819DC7698ED0BC14");

            entity.ToTable("client");

            entity.Property(e => e.Clientid).HasColumnName("clientid");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Identification)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("identification");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastname");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Referenceaddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("referenceaddress");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Contractid).HasName("PK__contract__138D0599B8812154");

            entity.ToTable("contract");

            entity.Property(e => e.Contractid).HasColumnName("contractid");
            entity.Property(e => e.ClientClientid).HasColumnName("client_clientid");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.MethodpaymentMethodpaymentid).HasColumnName("methodpayment_methodpaymentid");
            entity.Property(e => e.ServiceServiceid).HasColumnName("service_serviceid");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.StatuscontractStatusid).HasColumnName("statuscontract_statusid");

            entity.HasOne(d => d.ClientClient).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.ClientClientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__contract__client__5165187F");

            entity.HasOne(d => d.MethodpaymentMethodpayment).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.MethodpaymentMethodpaymentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__contract__method__52593CB8");

            entity.HasOne(d => d.ServiceService).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.ServiceServiceid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__contract__servic__4F7CD00D");

            entity.HasOne(d => d.StatuscontractStatus).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.StatuscontractStatusid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__contract__status__5070F446");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Deviceid).HasName("PK__device__84B9F7FF230D4211");

            entity.ToTable("device");

            entity.Property(e => e.Deviceid).HasColumnName("deviceid");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
            entity.Property(e => e.Devicename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("devicename");
            entity.Property(e => e.ServiceServiceid).HasColumnName("service_serviceid");

            entity.HasOne(d => d.ServiceService).WithMany(p => p.Devices)
                .HasForeignKey(d => d.ServiceServiceid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__device__service___4CA06362");
        });

        modelBuilder.Entity<Error>(entity =>
        {
            entity.HasKey(e => e.ErrorId).HasName("PK__errors__DA71E16C635BAA17");

            entity.ToTable("errors");

            entity.Property(e => e.ErrorId).HasColumnName("error_id");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Ipcreation)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("ipcreation");
            entity.Property(e => e.Nameprocess)
                .HasColumnType("text")
                .HasColumnName("nameprocess");
            entity.Property(e => e.Usercreation)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("usercreation");
        });

        modelBuilder.Entity<Methodpayment>(entity =>
        {
            entity.HasKey(e => e.Methodpaymentid).HasName("PK__methodpa__633563A44FED98E7");

            entity.ToTable("methodpayment");

            entity.Property(e => e.Methodpaymentid).HasColumnName("methodpaymentid");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("PK__payments__AF26EBEEE86CAA83");

            entity.ToTable("payments");

            entity.Property(e => e.Paymentid).HasColumnName("paymentid");
            entity.Property(e => e.ClientClientid).HasColumnName("client_clientid");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
            entity.Property(e => e.Paymentdate).HasColumnName("paymentdate");

            entity.HasOne(d => d.ClientClient).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ClientClientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__payments__client__5535A963");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Rolid).HasName("PK__rol__5403326CDF840500");

            entity.ToTable("rol");

            entity.Property(e => e.Rolid).HasColumnName("rolid");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
            entity.Property(e => e.Rolname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rolname");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Serviceid).HasName("PK__service__45516CA72B9D61C2");

            entity.ToTable("service");

            entity.Property(e => e.Serviceid).HasColumnName("serviceid");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
            entity.Property(e => e.Price)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("price");
            entity.Property(e => e.Servicedescription)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("servicedescription");
            entity.Property(e => e.Servicename)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("servicename");
        });

        modelBuilder.Entity<Statuscontract>(entity =>
        {
            entity.HasKey(e => e.Statusid).HasName("PK__statusco__36247E30FFBE3143");

            entity.ToTable("statuscontract");

            entity.Property(e => e.Statusid).HasColumnName("statusid");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Turn>(entity =>
        {
            entity.HasKey(e => e.Turnid).HasName("PK__turn__C2FE6222B709E520");

            entity.ToTable("turn");

            entity.Property(e => e.Turnid).HasColumnName("turnid");
            entity.Property(e => e.CashCashid).HasColumnName("cash_cashid");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
            entity.Property(e => e.Description)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Usergestorid).HasColumnName("usergestorid");

            entity.HasOne(d => d.CashCash).WithMany(p => p.Turns)
                .HasForeignKey(d => d.CashCashid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__turn__cash_cashi__5812160E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PK__user__CBA1B257BCEAAFBB");

            entity.ToTable("user");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Creationdate).HasColumnName("creationdate");
            entity.Property(e => e.Dateapproval).HasColumnName("dateapproval");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RolRolid).HasColumnName("rol_rolid");
            entity.Property(e => e.Userapproval).HasColumnName("userapproval");
            entity.Property(e => e.Usercreate).HasColumnName("usercreate");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.UserstatusStatusid).HasColumnName("userstatus_statusid");

            entity.HasOne(d => d.RolRol).WithMany(p => p.Users)
                .HasForeignKey(d => d.RolRolid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user__rol_rolid__48CFD27E");

            entity.HasOne(d => d.UserstatusStatus).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserstatusStatusid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user__userstatus__49C3F6B7");
        });

        modelBuilder.Entity<Usercash>(entity =>
        {
            entity.HasKey(e => new { e.UserUserid, e.CashCashid }).HasName("PK__usercash__1C0BBE18BEB2C92A");

            entity.ToTable("usercash");

            entity.Property(e => e.UserUserid).HasColumnName("user_userid");
            entity.Property(e => e.CashCashid).HasColumnName("cash_cashid");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");

            entity.HasOne(d => d.CashCash).WithMany(p => p.Usercashes)
                .HasForeignKey(d => d.CashCashid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__usercash__cash_c__5FB337D6");

            entity.HasOne(d => d.UserUser).WithMany(p => p.Usercashes)
                .HasForeignKey(d => d.UserUserid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__usercash__user_u__5EBF139D");
        });

        modelBuilder.Entity<Userstatus>(entity =>
        {
            entity.HasKey(e => e.Statusid).HasName("PK__userstat__36247E30F1874291");

            entity.ToTable("userstatus");

            entity.Property(e => e.Statusid).HasColumnName("statusid");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.Datedelete).HasColumnName("datedelete");
            entity.Property(e => e.Dateupdate).HasColumnName("dateupdate");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
