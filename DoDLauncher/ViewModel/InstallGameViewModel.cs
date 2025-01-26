﻿using DoDLauncher.Model;
using DoDLauncher.MVVM;
using DoDLauncher.Util;

namespace DoDLauncher.ViewModel
{
    public class InstallGameViewModel : ViewModelBase
    {
		public event Action OnFinishInstall;

		private double _downloadProgress;
		public double DownloadProgress
		{
			get { return _downloadProgress; }
			set { _downloadProgress = value; OnPropertyChanged(); }
		}

		private double _extractProgress;

		public double ExtractProgress
		{
			get { return _extractProgress; }
			set { _extractProgress = value; OnPropertyChanged(); }
		}



		private GameInstance _instance;
		public GameInstance Instance
		{
			get { return _instance; }
			set { _instance = value; OnPropertyChanged(); }
		}

		public async Task StartDownload()
		{
			Progress<double> downloadProgress = new Progress<double>(p => DownloadProgress = p);
			Progress<double> extractProgress = new Progress<double>(p => ExtractProgress = p);

            Instance.ExecutablePath = await GitHubActions.DownloadRelease("Rob-Storm", "DayOfDespair-Public", Instance.Version, Instance.Name, downloadProgress, extractProgress);
			OnFinishInstall?.Invoke();
		}

	}
}
