﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WorkOrderSystem.Models
{
    public partial class Employee
    {
        public int employee_id { get; set; }
        public string employee_name { get; set; }
        public string gender { get; set; }
        public DateTime? hire_date { get; set; }
        public double? hourly_rate { get; set; }
    }
}