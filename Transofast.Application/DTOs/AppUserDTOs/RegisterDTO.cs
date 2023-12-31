﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Transofast.Domain.Entities.Concrete;

namespace Transofast.Application.DTOs.AppUserDTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Kullanıcı Adını Boş Geçemezsiniz.")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parolayı Boş Geçemezsiniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Parola Tekrarı Boş Geçemezsiniz.")]
        [Compare("Password", ErrorMessage = "Parola ile Parola Tekrar Aynı Değil")]
        [DataType(DataType.Password)]
        [Display(Name = "ParolaTekrar")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email Boş Geçemezsiniz.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? CompanyID { get; set; }
        public Guid? AppUserID { get; set; }
        public Company? Company { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
