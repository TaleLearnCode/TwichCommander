﻿using System.Collections.Generic;

namespace TaleLearnCode.TwitchCommander.Models
{

	/// <summary>
	/// Represents the settings for a chat command.
	/// </summary>
	/// <seealso cref="IChatCommand" />
	public class ChatCommandSettings
	{

		/// <summary>
		/// Gets or sets the name of the Twitch channel the command is associated with.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the Twitch channel name.
		/// </value>
		public string ChannelName { get; set; }

		/// <summary>
		/// Gets or sets the text used to initiate the command.
		/// </summary>
		/// <value>
		/// A <c>string</c> the command initialization text.
		/// </value>
		public string Command { get; set; }

		/// <summary>
		/// Gets or sets the name of the chat command.
		/// </summary>
		/// <value>
		/// A <see cref="string"/> representing the name of the chat command.
		/// </value>
		public string CommandName { get; set; }

		/// <summary>
		/// Gets or sets the user permission.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the minimum permission level required to execute the command.
		/// </value>
		public UserPermission UserPermission { get; set; }

		/// <summary>
		/// Gets or sets the template used for the formatted responses of the command.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the template to use when the command responds.
		/// </value>
		public string Response { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this command is enabled when streaming.
		/// </summary>
		/// <value>
		///   <c>true</c> if this command is enabled when streaming; otherwise, <c>false</c>.
		/// </value>
		public bool IsEnabledWhenStreaming { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this command is enabled when not streaming.
		/// </summary>
		/// <value>
		///   <c>true</c> if this command is enabled when not streaming; otherwise, <c>false</c>.
		/// </value>
		public bool IsEnabledWhenNotStreaming { get; set; }

		/// <summary>
		/// Gets or sets the type of the response the command uses.
		/// </summary>
		/// <value>
		/// A <see cref="CommandResponseType"/> representing the response type used by the command.
		/// </value>
		public CommandResponseType CommandResponseType { get; set; }

		/// <summary>
		/// Gets or sets the number of seconds to wait before allowing a user to use the command again.
		/// </summary>
		/// <value>
		/// A <c>int</c> representing the cooldown period for the user in seconds.
		/// </value>
		public int UserCooldown { get; set; }

		/// <summary>
		/// Gets or sets the number of seconds to wait before allowing anyone to use the command again.
		/// </summary>
		/// <value>
		/// A <c>int</c> representing the cooldown for everyone in seconds.
		/// </value>
		public int GlobalCooldown { get; set; }

		/// <summary>
		/// Gets or sets the aliases for the command text.
		/// </summary>
		/// <value>
		/// A <see cref="List{string}"/>representing the list of command aliases.
		/// </value>
		public List<string> CommandAliases { get; set; } = new();

	}

}