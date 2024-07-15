using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OutOfOffice_web.Models;
using System.Reflection.Emit;

namespace OutOfOffice_web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>()
                .HasNoKey();
            builder.Entity<IdentityUserLogin<string>>()
                .HasNoKey();
            builder.Entity<IdentityUserToken<string>>()
                .HasNoKey();

            builder.Entity<Employee>()
                .HasKey(e => e.Id);
            builder.Entity<Employee>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Entity<Employee>()
                .Property(e => e.FullName)
                .IsRequired();
            builder.Entity<Employee>()
                .Property(e => e.Subdivision)
                .IsRequired();
            builder.Entity<Employee>()
                .Property(e => e.Position)
                .IsRequired();
            builder.Entity<Employee>()
                .Property(e => e.Status)
                .IsRequired();
            builder.Entity<Employee>()
                .Property(e => e.PeoplePartner)
                .IsRequired();
            builder.Entity<Employee>()
                .Property(e => e.OutOfOfficeBalance)
                .IsRequired();

            builder.Entity<LeaveRequest>()
                .HasKey(e => e.Id);
            builder.Entity<LeaveRequest>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Entity<LeaveRequest>()
                .Property(e => e.AbsenceReason)
                .IsRequired();
            builder.Entity<LeaveRequest>()
                .Property(e => e.StartDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Entity<LeaveRequest>()
                .Property(e => e.EndDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Entity<LeaveRequest>()
                .Property(e => e.Comment)
                .HasMaxLength(100);
            builder.Entity<LeaveRequest>()
                .Property(e => e.Status)
                .IsRequired();

            builder.Entity<LeaveRequest>()
                .HasOne(request => request.Employee)
                .WithMany(employee => employee.LeaveRequests)
                .HasForeignKey(request=>request.EmployeeId)
                .HasPrincipalKey(employee=>employee.Id);

            builder.Entity<ApprovalRequest>()
                .HasKey(e=>e.Id);
            builder.Entity<ApprovalRequest>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Entity<ApprovalRequest>()
                .Property(e => e.Status)
                .IsRequired();
            builder.Entity<ApprovalRequest>()
                .Property(e => e.Comment)
                .HasMaxLength(100);

            builder.Entity <ApprovalRequest>()
                .HasOne(approval=>approval.LeaveRequest)
                .WithOne(leave=>leave.ApprovalRequest)
                .HasForeignKey<ApprovalRequest>(req=>req.LeaveRequestId)
                .IsRequired();
            builder.Entity<ApprovalRequest>()
                .HasOne(approval => approval.Approver)
                .WithOne(employee => employee.ApprovalRequest)
                .HasForeignKey<ApprovalRequest>(req => req.ApproverId)
                .IsRequired();

            builder.Entity<Project>()
                .HasKey(e => e.Id);
            builder.Entity<Project>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Entity<Project>()
                .Property(e => e.ProjectType)
                .IsRequired();
            builder.Entity<Project>()
                .Property(e => e.StartDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Entity<Project>()
                .Property(e => e.EndDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Entity<Project>()
                .Property(e => e.Comment)
                .HasMaxLength(100);
            builder.Entity<Project>()
                .Property(e => e.Status)
                .IsRequired();

            builder.Entity<Project>()
                .HasOne(project => project.ProjectManager)
                .WithOne(employee => employee.Project)
                .HasForeignKey<Employee>(employee => employee.Id)
                .IsRequired();

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<ApprovalRequest> ApprovalRequests { get; set; }
        public DbSet<Project> Projects { get; set; }

    }
}
