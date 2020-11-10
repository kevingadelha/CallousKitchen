using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Apis;
using Capstone.Classes;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.ServiceModel;

namespace Capstone
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccountService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select AccountService.svc or AccountService.svc.cs at the Solution Explorer and start debugging.
	public class AccountService : AccountServiceParent, IAccountService
	{
	}
}
