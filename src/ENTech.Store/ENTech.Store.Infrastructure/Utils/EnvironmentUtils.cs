using System;
using System.Configuration;
using System.Text.RegularExpressions;
using ENTech.Store.Infrastructure.Enums;

namespace ENTech.Store.Infrastructure.Utils
{
	public static class EnvironmentUtils
	{
		private static string _bundleTableSalt;

		/// <summary>Retrieve string from application config file</summary>
		/// <param name="key">Key that identifies the value</param>
		/// <remarks>Use for setttings common to all environments (dev, staging, production)</remarks>
		/// <returns>String value</returns>
		public static string GetConfigSettingStr(string key)
		{
			string value = ConfigurationManager.AppSettings[key];

			return value;
		}

		/// <summary>Retreive int from application config file</summary>
		/// <param name="key">Key that identifies the value</param>
		/// <remarks>Use for setttings common to all environments (dev, staging, production)</remarks>
		/// <returns>Integer value</returns>
		public static int? GetConfigSettingInt(string key)
		{
			string value = GetConfigSettingStr(key);

			if (String.IsNullOrEmpty(value))
			{
				return null;
			}
			return Convert.ToInt32(value);
		}

		/// <summary>Retrieves current environment from App.Config or Web.Config</summary>
		/// <returns>EnumEnvironment value that indicates environment of the current application.</returns>
		/// <example>
		/// <code>
		///		if (XEnvironment.GetEnvironmentType() == EnvironmentType.Dev)
		///		{
		///			//Run some code only in development environment.
		///		}
		///		
		/// </code>
		/// </example>
		public static EnvironmentType GetEnvType()
		{
			var environment = GetConfigSettingStr("EnvironmentType");

			switch (environment)
			{
				case "Stable":
					return EnvironmentType.Stable;
				case "QA":
					return EnvironmentType.Qa;
				case "Prod":
					return EnvironmentType.Prod;
				case "Dev":
					return EnvironmentType.Dev;
			}

			return EnvironmentType.Local;
		}


		public static bool UseErrorLog
		{
			get { return GetConfigSettingInt("UseErrorLog") == 1; }
		}

		/// <summary>Retrieves database connection string from application config file.</summary>
		/// <remarks>
		///	Connection string from config file based on enviroment - production, staging or development.
		/// </remarks> 
		/// <returns>Default connection string.</returns>
		public static string GetConnectionString(string key, bool nameOnly)
		{
			//string envKey = GetEnvironmentKey() + "_" + key;

			ConnectionStringSettings css = ConfigurationManager.ConnectionStrings[key];

			string result = null;

			if (css != null)
				result = nameOnly ? css.Name : css.ConnectionString;

			return result;

		}

		/// <summary>Returns name of smtp server used for sending an e-mail.</summary>
		/// <returns>Full name of Smtp Server</returns>
		/// <remarks>Used in SendEmail method.</remarks>
		public static string GetSmtpServer()
		{
			string smtpServer = GetConfigSettingStr("SmtpServer");
			if (smtpServer == "")
				throw new Exception("EnvironmentUtils::GetSmtpServer  Not specified in config file.");

			var vals = smtpServer.Split(',');
			return vals[0];
		}

		/// <summary>Retreive user name and password for SMTP server.</summary>
		/// <returns>Boolena value indicating if Cretentials have been provided</returns>
		/// <remarks>Used in SendEmail method.</remarks>
		public static bool GetSmtpServerCredentials(ref string username, ref string password)
		{
			string smtpServer = GetConfigSettingStr("SmtpServer");
			if (smtpServer == "")
				throw new Exception("EnvironmentUtils::GetSmtpServerrCredentials  'SmtpServer' is not specified in config file.");

			var vals = smtpServer.Split(',');
			if (vals.Length == 1)
				return false;

			username = vals[1];
			password = vals[2];

			return true;
		}

		private static string _machineName; //Cache current machine name. using in GetMachineName

		/// <summary>This returns the name of the machine the application is running on.</summary>
		/// <returns>Name of the machine</returns>
		/// <remarks>Uses Environment.MachineName function, and caches the name for future use.</remarks>
		public static string GetMachineName()
		{
			//Cache machine name to speed up performance
			if(_machineName == null)
				_machineName = Environment.MachineName;

			return _machineName;
		}

		/// <summary>Return e-mail used for From addresses.</summary>
		public static string GetFromEmail(FromEmailType type)
		{
			var settingsKey = "FromEmail_" + type;

			var fromEmail = GetConfigSettingStr(settingsKey);
			if (String.IsNullOrWhiteSpace(fromEmail))
			{
				throw new Exception("EnvironmentUtils::" + settingsKey + " Not specified in config file.");
			}

			return fromEmail;
		}

		public static string GetFrontEndWebRootUrl()
		{
			string webRoot = GetConfigSettingStr("FrontEndWebRoot");
			if (webRoot == "")
				throw new Exception("EnvironmentUtils::GetFrontEndWebRoot  Not specified in config file.");

			return webRoot;
		}

		public static string GetHttpsFrontEndWebRootHost()
		{
			var webroot = new Uri(GetFrontEndWebRootUrl(), UriKind.Absolute);

			if (webroot.Host == "localhost") return GetFrontEndWebRootUrl();

			return String.Format("https://{0}", webroot.Host + (webroot.Port == 80 ? "" : ":" + webroot.Port));
		}

		public static string GetFrontEndWebRootHost()
		{
			var webroot = new Uri(GetFrontEndWebRootUrl(), UriKind.Absolute);
			return webroot.Host;
		}

		public static Uri GetFrontEndWebRootUri()
		{
			var webroot = new Uri(GetFrontEndWebRootUrl(), UriKind.Absolute);
			return webroot;
		}

		public static string GetFrontEndWebRootScheme()
		{
			var webroot = new Uri(GetFrontEndWebRootUrl(), UriKind.Absolute);
			return webroot.Scheme;
		}

		public static string GetMailBeeLicenseKey()
		{
			return GetConfigSettingStr("MailBeeLicenseKey");
		}

		public static string GetAmazonAccessKeyId()
		{
			return GetConfigSettingStr("Amazon_AccessKeyId");
		}

		public static string GetAmazonSecretAccessKey()
		{
			return GetConfigSettingStr("Amazon_SecretAccessKey");
		}

		public static string GetConnectionString()
		{
			return GetConnectionString("ConnString", false);
		}

		public static string GetConnectionStringName()
		{
			return GetConnectionString("ConnString", true);
		}

		public static string GetTestConnectionStringName()
		{
			var connectionString = GetConnectionString("Test_ConnString", true);

			return connectionString;
		}

		public static string GetConnectionStringMasked()
		{
			String connectionString = GetConnectionString();
			String maskedConnectionString = String.Empty;

			// Look for all characters up to the first semicolon.
			const string userIdPattern = @"User Id=.*?;";
			const string userIdReplacement = @"User Id=XXXXX;";

			// Look for all characters, with or without, but not including semicolon
			const string passwordPattern = @"Password=.*[^;]";
			const string passwordReplacement = @"Password=XXXXX";

			// Look for all characters, with or without, but not including semicolon
			const string pwdPattern = @"Pwd=.*[^;]";
			const string pwdReplacement = @"Pwd=XXXXX";

			maskedConnectionString = Regex.Replace(connectionString, userIdPattern, userIdReplacement, RegexOptions.IgnoreCase);

			if (Regex.IsMatch(maskedConnectionString, passwordPattern, RegexOptions.IgnoreCase))
				maskedConnectionString = Regex.Replace(maskedConnectionString, passwordPattern, passwordReplacement,
													   RegexOptions.IgnoreCase);

			else if (Regex.IsMatch(maskedConnectionString, pwdPattern, RegexOptions.IgnoreCase))
				maskedConnectionString = Regex.Replace(maskedConnectionString, pwdPattern, pwdReplacement, RegexOptions.IgnoreCase);

			return maskedConnectionString;

		}

		public static string GetGoogleAnalyticsCode()
		{
			return GetConfigSettingStr("GoogleAnalyticsCode");
		}

		public static string GetAppDirectKey()
		{
			var key = GetConfigSettingStr("AppDirectKey");

			if (key == "")
				throw new Exception("EnvironmentUtils::AppDirectkey  Not specified in config file.");

			return key;
		}

		public static string GetAppDirectSecret()
		{
			var secret = GetConfigSettingStr("AppDirectSecret");

			if (secret == "")
				throw new Exception("EnvironmentUtils::AppDirectSecret  Not specified in config file.");

			return secret;
		}

		public static string GetBusinessAdminWebUrl()
		{
			var url = GetConfigSettingStr("BusinessAdminWebUrl");

			if (url == "")
				throw new Exception("EnvironmentUtils::BusinessAdminWebUrl  Not specified in config file.");

			return url;
		}

		public static string GetStripePublishableApiKey()
		{
			return GetConfigSettingStr("Stripe_PublishableApiKey");
		}

		public static bool GetBundleTableEnableOptimizations()
		{
			var enableOptimization = GetConfigSettingStr("BundleTable_EnableOptimizations");

			var result = false;

			bool.TryParse(enableOptimization, out result);

			return result;
		}

		public static void SetIncludeFileSalt(string salt)
		{
			_bundleTableSalt = salt;
		}

		public static string GetIncludeFileSalt()
		{
			return _bundleTableSalt;
		}

		public static string GetIosPushnotificationPassword()
		{
			var url = GetConfigSettingStr("Ios_PushNotificationPassword");

			if (url == "")
				throw new Exception("EnvironmentUtils::Ios_PushNotificationPassword  Not specified in config file.");

			return url;
		}


		public static int? SonyBusinessId
		{
			get { return GetConfigSettingInt("BusinessId_Sony").GetValueOrDefault(); }
		}

		public static bool UseRaygun
		{
			get { return GetConfigSettingInt("UseRaygun") == 1; }
		}

		public static string GetRaygunKey()
		{
			var key = GetConfigSettingStr("Raygun_Key");

			if (key == "")
				throw new Exception("EnvironmentUtils::Raygun key  Not specified in config file.");

			return key;
		}

		public static bool UseMachineSpecificL3Cache
		{
			get
			{
				{
					return GetConfigSettingInt("L3Cache_UseMachineSpecific") == 1;
				}
			}
		}

		public static bool CmsEnabled
		{
			get { return GetConfigSettingBoolean("CMS_Enabled").GetValueOrDefault(); }
		}

		private static bool? GetConfigSettingBoolean(string key)
		{
			var value = GetConfigSettingStr(key);
			bool booleanValue;
			if (!Boolean.TryParse(value, out booleanValue))
			{
				return null;
			}
			return booleanValue;
		}
	}
}