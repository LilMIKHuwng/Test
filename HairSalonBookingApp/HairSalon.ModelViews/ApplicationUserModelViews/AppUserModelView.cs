﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalon.ModelViews.ApplicationUserModelViews
{
	public class AppUserModelView
	{
		public string Id { get; set; }
		public string UserInfoId { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
	}
}