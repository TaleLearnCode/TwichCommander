﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaleLearnCode.TwitchCommander.Models;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Games;
using TwitchLib.Api.Services;
using TwitchLib.Api.V5.Models.Channels;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TaleLearnCode.TwitchCommander
{

	public partial class WOPR
	{

		private readonly AppSettings _appSettings;
		private readonly AzureStorageSettings _azureStorageSettings;
		private readonly TableNames _tableNames;
		private readonly TwitchSettings _twitchSettings;

		private readonly TwitchClient _twitchClient = new();
		private ConnectionCredentials _credentials;
		private TwitchAPI _twitchAPI;
		private LiveStreamMonitorService _twitchMonitor;

		private bool _IsOnline;
		private bool _fakeOnline;

		public string ChannelName { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="WOPR"/> class.
		/// </summary>
		/// <param name="appSettings">The application settings.</param>
		/// <param name="azureStorageSetttings">The azure storage setttings.</param>
		/// <param name="tableNames">The table names.</param>
		/// <param name="twitchSettings">The Twitch settings.</param>
		/// <param name="viewLogs"></param>
		public WOPR(
			string channelName,
			AppSettings appSettings,
			AzureStorageSettings azureStorageSetttings,
			TableNames tableNames,
			TwitchSettings twitchSettings,
			bool viewLogs)
		{

			ChannelName = channelName;
			_appSettings = appSettings;
			_azureStorageSettings = azureStorageSetttings;
			_tableNames = tableNames;
			_twitchSettings = twitchSettings;

			ConfigureTwitchClient(viewLogs);
			ConfigureTwitchAPI();
			ConfigureTwitchMonitor();

		}

		/// <summary>
		/// Starts monitoring Twitch in order to perform the necessary actions.
		/// </summary>
		public void Connect(bool fakeOnline = false)
		{
			_fakeOnline = fakeOnline;
			_twitchClient.Connect();
			_twitchMonitor.Start();
			ConfigureTimers();
		}

		/// <summary>
		/// Configures the <see cref="TwitchClient"/> instance.
		/// </summary>
		/// <param name="viewLogs">If set to <c>true</c> then the logs will be sent to the consumer.</param>
		private void ConfigureTwitchClient(bool viewLogs)
		{
			if (viewLogs) _twitchClient.OnLog += TwitchClient_OnLog;
			_twitchClient.OnConnected += TwitchClient_OnConnected;
			_twitchClient.OnDisconnected += TwitchClient_OnDisconnected;
			_twitchClient.OnChatCommandReceived += TwitchClient_OnChatCommandReceived;
			_twitchClient.OnNewSubscriber += TwitchClient_OnNewSubscriber;
			_twitchClient.OnGiftedSubscription += TwitchClient_OnGiftedSubscription;
			_twitchClient.OnReSubscriber += TwitchClient_OnResubscriber;
			_twitchClient.OnMessageReceived += TwitchClient_OnMessageReceived;
			_twitchClient.OnRaidNotification += TwitchClient_OnRaidNotification;
			_twitchClient.OnBeingHosted += TwitchClient_OnBeingHosted;

			_credentials = new ConnectionCredentials(_twitchSettings.BotName, _twitchSettings.AccessToken);
			_twitchClient.Initialize(_credentials, _twitchSettings.ChannelName);
		}

		private void TwitchClient_OnBeingHosted(object sender, OnBeingHostedArgs e)
		{
			OnBeingHosted?.Invoke(this, e);
		}

		public EventHandler<OnBeingHostedArgs> OnBeingHosted;

		private void TwitchClient_OnRaidNotification(object sender, OnRaidNotificationArgs e)
		{
			OnRaidNotification?.Invoke(this, e);
		}

		public EventHandler<OnRaidNotificationArgs> OnRaidNotification;

		/// <summary>
		/// Configures the <see cref="TwitchAPI"/> instance.
		/// </summary>
		private void ConfigureTwitchAPI()
		{
			_twitchAPI = new();
			_twitchAPI.Settings.ClientId = _twitchSettings.ClientId;
			_twitchAPI.Settings.AccessToken = _twitchSettings.AccessToken;
		}

		/// <summary>
		/// Configures the <see cref="LiveStreamMonitorService"/> instance.
		/// </summary>
		private void ConfigureTwitchMonitor()
		{

			_twitchMonitor = new(_twitchAPI, _appSettings.StreamMonitorCheckInterval);
			_twitchMonitor.SetChannelsByName(new List<string> { _twitchSettings.ChannelName });

			_twitchMonitor.OnStreamOffline += TwitchMonitor_OnStreamOffline;
			_twitchMonitor.OnStreamOnline += TwitchMonitor_OnStreamOnline;
			_twitchMonitor.OnStreamUpdate += TwitchMonitor_OnStreamUpdate;
		}

		/// <summary>
		/// Sends the message to the Twitch channel.
		/// </summary>
		/// <param name="message">The message to be sent to the Twitch channel's chat..</param>
		/// <returns></returns>
		private void SendMessage(string message)
		{
			_twitchClient.SendMessage(_twitchSettings.ChannelName, message);
		}

		public async Task<int> GetSubscriberCountAsync()
		{
			ChannelSubscribers channelSubscribers = await _twitchAPI.V5.Channels.GetChannelSubscribersAsync(_twitchSettings.ChannelId);
			return channelSubscribers.Total;
		}

		public async Task<int> GetFollowerCountAsync()
		{
			ChannelFollowers channelFollowers = await _twitchAPI.V5.Channels.GetChannelFollowersAsync(_twitchSettings.ChannelId);
			return channelFollowers.Total;
		}

		public async Task<string> GetGameNameAsync()
		{
			GetGamesResponse getGamesResponse = await _twitchAPI.Helix.Games.GetGamesAsync(new List<string>() { _stream.GameId });
			if (getGamesResponse.Games.Any())
			{
				string gameName = getGamesResponse.Games[0].Name;
				gameName = gameName.Replace("&", "&&");
				return gameName;
			}
			else
			{
				return string.Empty;
			}
		}


	}

}