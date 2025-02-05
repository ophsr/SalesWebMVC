﻿using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} is required")]
        [StringLength(60,MinimumLength = 3, ErrorMessage ="{0} size should  be between {2} and {1}")]
        public string Name { get; set; }
        
        [Required(ErrorMessage ="{0} is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0} is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Range(100.00,50000.00, ErrorMessage = "{0} must be from {1} to {2}")]
        public double BaseSalary { get; set; }


        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate ; 
            BaseSalary = baseSalary;
            Department = department; 
        }

        public void AddSales(SalesRecord salesRecord)
        {
            Sales.Add(salesRecord);
        }

        public void RemoveSales(SalesRecord salesRecord)
        {
            Sales.Remove(salesRecord);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(obj => obj.Date >= initial && obj.Date <= final)
                .Sum(obj => obj.Amount);
        }

    }
}
