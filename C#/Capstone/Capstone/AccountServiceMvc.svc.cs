using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Capstone.Apis;
using Capstone.Classes;

namespace Capstone
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccountServiceMvc" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select AccountServiceMvc.svc or AccountServiceMvc.svc.cs at the Solution Explorer and start debugging.
	public class AccountServiceMvc : AccountServiceParent, IAccountServiceMvc
	{
	}
}
