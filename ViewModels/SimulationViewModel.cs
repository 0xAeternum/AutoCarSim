using AutoCarSim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCarSim.ViewModels
{
    public class SimulationViewModel : Simulation
    {
        public SimulationViewModel()
        {
            map = new Map();
            milisecondsPerSpawn = getSpawns();
            amountOfThreads = getThreads();
            amountOfCars = 1;

            start();
        }

        //get amount of threads from local settings
        private int getThreads()
        {
            return (int)Windows.Storage.ApplicationData.Current.LocalSettings.Values["threads"];
        }

        //get spawn time for enemy cars from local settings
        private int getSpawns()
        {
            return (int)Windows.Storage.ApplicationData.Current.LocalSettings.Values["spawns"];
        }

        public SimulationViewModel(int milisecondsPerSpawn, int threads, int amountOfCars)
        {
            map = new Map();
            this.milisecondsPerSpawn = milisecondsPerSpawn;
            amountOfThreads = threads;
            this.amountOfCars = amountOfCars;

            start();
        }

        public void stopSimulation()
        {
            stopAsync();
        }
    }
}
