﻿namespace TaleLearnCode.TwitchCommander.Settings
{


	/// <summary>
	/// Represents the settings for connecting to the Azure Storage account.
	/// </summary>
	public class AzureStorageSettings
	{

		// TODO: Switch the Uri property to a URI data type.

		/// <summary>
		/// Gets or sets the Azure Storage account URI.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the URI for connecting to the Azure Storage account.
		/// </value>
		public string Uri { get; set; }

		/// <summary>
		/// Gets or sets the name of the Azure Storage account to connect to.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the Azure Storage account name.
		/// </value>
		public string AccountName { get; set; }

		/// <summary>
		/// Gets or sets the key for connecting to the Azure Storage account.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the Azure Storage key.
		/// </value>
		public string AccountKey { get; set; }

		/// <summary>
		/// Gets or sets the name of the chat command table.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the name of the chat command table.
		/// </value>
		public string ChatCommandTableName { get; set; }

	}

}