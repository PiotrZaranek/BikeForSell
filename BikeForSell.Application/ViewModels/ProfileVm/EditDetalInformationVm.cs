﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Application.ViewModels.ProfileVm
{
    public class EditDetalInformationVm
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Musisz podać swoje imię!")]
        public string FirsName { get; set; }

        [Required(ErrorMessage = "Musisz podać swoje nazwisko!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Musisz podać swój numer telefonu!")]
        [RegularExpression(@"[0-9]{9}", ErrorMessage = "Podaj 9 cyfrowy numer")]
        public string PhoneNumber { get; set; }
    }
}
