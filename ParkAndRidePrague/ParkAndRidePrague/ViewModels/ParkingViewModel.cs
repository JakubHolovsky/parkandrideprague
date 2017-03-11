using System;
using System.ComponentModel;
using ParkAndRidePrague.Core.Enums;
using ParkAndRidePrague.Core.Interfaces;

namespace ParkAndRidePrague.ViewModels
{
	public class ParkingViewModel : INotifyPropertyChanged
	{
		public ParkingViewModel() { }

		public ParkingViewModel(IParking parking)
		{
			Update(parking, false);
		}

		public void Update(IParking parking, bool updateDifference = true)
		{
			if (updateDifference)
				Difference = parking.FreePlacesCount - FreePlacesCount;

			Name = parking.Name;
			LastUpdateDate = parking.LastUpdateDate;
			TotalPlacesCount = parking.TotalPlacesCount;
			FreePlacesCount = parking.FreePlacesCount;
			TakenPlacesCount = parking.TakenPlacesCount;
			ParkingAvailability = parking.ParkingAvailability;
		}

		private string name;
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
				OnPropertyChanged("Name");
			}
		}

		public DateTime lastUpdateDate;
		public DateTime LastUpdateDate 
		{
			get
			{
				return lastUpdateDate;
			}
			set
			{
				lastUpdateDate = value;
				OnPropertyChanged("LastUpdateDate");
			}
		}

		private int totalPlacesCount;
		public int TotalPlacesCount
		{
			get
			{
				return totalPlacesCount;
			}
			set
			{
				totalPlacesCount = value;
				OnPropertyChanged("TotalPlacesCount");
			}
		}

		private int freePlacesCount;
		public int FreePlacesCount 
		{ 
			get
			{
				return freePlacesCount; 
			} 
			set 
			{
				freePlacesCount = value;
				OnPropertyChanged("FreePlacesCount");
			} 
		}

		private int takenPlacesCount;
		public int TakenPlacesCount 
		{
			get
			{
				return takenPlacesCount;
			}
			set
			{
				takenPlacesCount = value;
				OnPropertyChanged("TakenPlacesCount");
			}
		}

		private ParkingAvailability parkingAvailability;
		public ParkingAvailability ParkingAvailability
		{
			get
			{
				return parkingAvailability;
			}
			set
			{
				parkingAvailability = value;
				OnPropertyChanged("ParkingAvailability");
			}
		}

		private int difference;
		public int Difference
		{
			get
			{
				return difference;
			}
			set
			{
				difference = value;
				OnPropertyChanged("Difference");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
