﻿using System;
using System.Collections.Generic;
using TaleLearnCode.TwitchCommander.Models;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TaleLearnCode.TwitchCommander.Events
{
	public class OnCommandReceivedArgs : EventArgs
	{

		public List<string> ArgumentsAsList { get; }
		public string ArgumentsAsString { get; }
		public ChatMessage ChatMessage { get; }
		public string CommandText { get; }
		public ChatCommandSettings ChatCommand { get; }
		public string ReturnedMessage { get; }

		public OnCommandReceivedArgs(OnChatCommandReceivedArgs onChatCommandReceivedArgs, ChatCommandSettings chatCommand, string returnedMessage)
		{
			ArgumentsAsList = onChatCommandReceivedArgs.Command.ArgumentsAsList;
			ArgumentsAsString = onChatCommandReceivedArgs.Command.ArgumentsAsString;
			ChatMessage = onChatCommandReceivedArgs.Command.ChatMessage;
			CommandText = onChatCommandReceivedArgs.Command.CommandText;
			ChatCommand = chatCommand;
			ReturnedMessage = returnedMessage;
		}
	}

}