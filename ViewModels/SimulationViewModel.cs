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
            milisecondsPerSpawn = 1000;
            amountOfThreads = 1;
            amountOfCars = 1;

            start();
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
            stop();
        }
    }
}
